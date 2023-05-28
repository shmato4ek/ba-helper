using AutoMapper;
using BAHelper.BLL.Exceptions;
using BAHelper.BLL.Services.Abstract;
using BAHelper.Common.DTOs.Cluster;
using BAHelper.Common.DTOs.ClusterData;
using BAHelper.Common.DTOs.User;
using BAHelper.Common.Enums;
using BAHelper.DAL.Context;
using BAHelper.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Office.Interop.Word;

namespace BAHelper.BLL.Services
{
    class Point
    {
        public const int NOISE = -1;
        public const int UNCLASSIFIED = 0;
        public double TaskTopic;
        public double Quality;
        public int UserId;
        public int ClusterId;
        public Point(double taskTopic, double quality, int userId)
        {
            TaskTopic = taskTopic;
            Quality = quality;
            UserId = userId;
        }
        public static double DistanceSquared(Point p1, Point p2)
        {
            double diffX = p2.TaskTopic - p1.TaskTopic;
            double diffY = p2.Quality - p1.Quality;
            return diffX * diffX + diffY * diffY;
        }
    }
    public class DbscanClasterization : BaseService
    {
        private readonly ProjectService _projectService;
        public DbscanClasterization(BAHelperDbContext context, IMapper mapper, ProjectService projectService)
            :base(context, mapper)
        {
            _projectService = projectService;
        }
        public async Task<List<ClusterInfoDTO>> Cluster(int projectId, int userId)
        {
            double eps = 0.05;
            int minPts = 1;
            var projectEntity = await _context
                .Projects
                .Include(project => project.Users)
                .ThenInclude(user => user.Statistics)
                .FirstOrDefaultAsync(project => project.Id == projectId);
            if (projectEntity is null)
            {
                throw new NotFoundException(nameof(Project), projectId);
            }
            if (projectEntity.AuthorId != userId)
            {
                throw new NoAccessException(userId);
            }
            var usersStatistic = new List<List<double>>();
            foreach (var user in projectEntity.Users)
            {
                var userEntity = await _context
                    .Users
                    .Include(user => user.Statistics)
                    .FirstOrDefaultAsync(us => us.Id == user.Id);
                foreach (var stat in userEntity.Statistics)
                {
                    if (stat.TaskCount > 0)
                    {
                        var userStat = new List<double>();
                        userStat.Add((double)stat.TaskTopic);
                        userStat.Add(stat.TaskQuality);
                        userStat.Add(stat.UserId);
                        usersStatistic.Add(userStat);
                    }
                }
            }
            List<Point> points = new List<Point>();
            foreach (var s in usersStatistic)
            {
                Point p = new Point(s[0], s[1], (int)s[2]);
                points.Add(p);
            }
            List<Point> rawData = Normalized(points);
            List<List<Point>> clusters = new List<List<Point>>();
            int clusterId = 1;
            for (int i = 0; i < rawData.Count; i++)
            {
                Point p = rawData[i];
                if (p.ClusterId == Point.UNCLASSIFIED)
                {
                    (bool flag, rawData) = ExpandCluster(rawData, p, clusterId, eps, minPts);
                    if (flag) clusterId++;
                }
            }
            // sort out points into their clusters, if any
            int maxClusterId = rawData.OrderBy(p => p.ClusterId).Last().ClusterId;
            for (int i = 0; i < maxClusterId; i++) clusters.Add(new List<Point>());
            foreach (Point p in rawData)
            {
                if (p.ClusterId > 0) clusters[p.ClusterId - 1].Add(p);
            }

            var newClusters = new List<ClusterDTO>();
            for (int i = 0; i < clusters.Count; i++)
            {
                var addedCluster = new ClusterDTO { Data = new List<ClusterDataDTO>(), ProjectId = projectId, Users = new List<UserDTO>() };
                for (int j = 1; j <= 8; j++)
                {
                    addedCluster.Data.Add(new ClusterDataDTO() { Quality = 0, Topic = (TopicTag)j });
                }
                newClusters.Add(addedCluster);
            }
            for (int i = 0; i < clusters.Count; i++)
            {
                foreach (var s in clusters[i])
                {
                    var clusteredUserId = s.UserId;
                    var clusteredUser = await _context
                        .Users
                        .FirstOrDefaultAsync(user => user.Id == clusteredUserId);
                    var foundCluster = newClusters[i].Users.FirstOrDefault(user => user.Id == clusteredUserId);
                    if (foundCluster == null)
                    {
                        newClusters[i].Users.Add(_mapper.Map<UserDTO>(clusteredUser));
                    }
                }
            }
            foreach (var cluster in newClusters)
            {
                for (int i = 0; i < cluster.Data.Count; i++)
                {
                    double sum = 0;
                    if (cluster.Users != null)
                    {
                        foreach (var user in cluster.Users)
                        {
                            sum += user.Statistics.FirstOrDefault(s => s.TaskTopic == cluster.Data[i].Topic).TaskQuality;
                        }
                        double result = sum / cluster.Users.Count;
                        cluster.Data[i].Quality = (int)Math.Round(result);
                    }
                }
            }
            List<ClusterInfoDTO> clustersInfo = new List<ClusterInfoDTO>();
            foreach (var c in newClusters)
            {
                c.Data.Sort(CompareClusterData);
                var clusterInfo = new ClusterInfoDTO
                {
                    ProjectName = projectEntity.ProjectName,
                    Data = new List<ClusterDataDTO>(),
                    Users = _mapper.Map<List<UserInfoDTO>>(c.Users)
                };
                for (int i = 0; i < 3; i++)
                {
                    clusterInfo.Data.Add(c.Data[i]);
                }
                clustersInfo.Add(clusterInfo);
            }
            clustersInfo.Sort(CompareClusterInfo);
            var resultClusters = new List<ClusterInfoDTO>();
            foreach (var c in clustersInfo)
            {
                if (c.Users.Count != 0)
                {
                    resultClusters.Add(c);
                }
            }
            if (resultClusters.Count <= 10)
            {
                return resultClusters;
            }
            else
            {
                return resultClusters.Take(10).ToList();
            }
        }
        private static int CompareClusterData(ClusterDataDTO x, ClusterDataDTO y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (y == null)
                {
                    return 1;
                }
                else
                {
                    int retval = y.Quality.CompareTo(x.Quality);

                    if (retval != 0)
                    {
                        return retval;
                    }
                    else
                    {
                        return y.Quality.CompareTo(x.Quality);
                    }
                }
            }
        }

        private static int CompareClusterInfo(ClusterInfoDTO x, ClusterInfoDTO y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (y == null)
                {
                    return 1;
                }
                else
                {
                    x.Data.Sort(CompareClusterData);
                    y.Data.Sort(CompareClusterData);
                    double xMax = x.Data[0].Quality;
                    double yMax = y.Data[0].Quality;
                    int retval = yMax.CompareTo(xMax);

                    if (retval != 0)
                    {
                        return retval;
                    }
                    else
                    {
                        return yMax.CompareTo(xMax);
                    }
                }
            }
        }
        private static List<Point> Normalized(List<Point> rawData)
        {
            List<Point> result = new List<Point>();
            for (int i = 0; i < rawData.Count; ++i)
            {
                result.Add(rawData[i]);
            }

            for (int j = 0; j < 2; ++j)
            {
                double colSum = 0.0;
                for (int i = 0; i < result.Count; ++i)
                {
                    if (j == 0)
                    {
                        colSum += result[i].TaskTopic;
                    }
                    else
                    {
                        colSum += result[i].Quality;
                    }
                }
                double mean = colSum / result.Count;
                double sum = 0.0;
                for (int i = 0; i < result.Count; ++i)
                {
                    if (j == 0)
                    {
                        sum += (result[i].TaskTopic - mean) * (result[i].TaskTopic - mean);
                    }
                    else
                    {
                        sum += (result[i].Quality - mean) * (result[i].Quality - mean);
                    }
                }
                double sd = sum / result.Count;
                for (int i = 0; i < result.Count; ++i)
                {
                    if (j == 0)
                    {
                        result[i].TaskTopic = (result[i].TaskTopic - mean) / sd;
                    }
                    else
                    {
                        result[i].Quality = (result[i].Quality - mean) / sd;
                    }
                }
            }
            return result;
        }

        static List<Point> GetRegion(List<Point> points, Point p, double eps)
        {
            List<Point> region = new List<Point>();
            for (int i = 0; i < points.Count; i++)
            {
                double distSquared = Point.DistanceSquared(p, points[i]);
                if (distSquared <= eps) region.Add(points[i]);
            }
            return region;
        }
        static (bool, List<Point>) ExpandCluster(List<Point> points, Point p, int clusterId, double eps, int minPts)
        {
            List<Point> seeds = GetRegion(points, p, eps);
            if (seeds.Count < minPts) // no core point
            {
                p.ClusterId = Point.NOISE;
                return (false, points);
            }
            else // all points in seeds are density reachable from point 'p'
            {
                for (int i = 0; i < seeds.Count; i++) seeds[i].ClusterId = clusterId;
                seeds.Remove(p);
                while (seeds.Count > 0)
                {
                    Point currentP = seeds[0];
                    List<Point> result = GetRegion(points, currentP, eps*eps);
                    if (result.Count >= minPts)
                    {
                        for (int i = 0; i < result.Count; i++)
                        {
                            Point resultP = result[i];
                            if (resultP.ClusterId == Point.UNCLASSIFIED || resultP.ClusterId == Point.NOISE)
                            {
                                if (resultP.ClusterId == Point.UNCLASSIFIED) seeds.Add(resultP);
                                resultP.ClusterId = clusterId;
                            }
                        }
                    }
                    seeds.Remove(currentP);
                }
                return (true, points);
            }
        }
    }
}
