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
            double eps = 15;
            int minPts = 3;
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
                    var userStat = new List<double>();
                    userStat.Add((double)stat.TaskTopic);
                    userStat.Add(stat.TaskQuality);
                    userStat.Add(stat.UserId);
                    usersStatistic.Add(userStat);
                }
            }
            List<Point> points = new List<Point>();
            foreach (var s in usersStatistic)
            {
                Point p = new Point(s[0], s[1], (int)s[2]);
            }
            List<List<Point>> clusters = new List<List<Point>>();
            eps *= eps; // square eps
            int clusterId = 1;
            for (int i = 0; i < points.Count; i++)
            {
                Point p = points[i];
                if (p.ClusterId == Point.UNCLASSIFIED)
                {
                    if (ExpandCluster(points, p, clusterId, eps, minPts)) clusterId++;
                }
            }
            // sort out points into their clusters, if any
            int maxClusterId = points.OrderBy(p => p.ClusterId).Last().ClusterId;
            for (int i = 0; i < maxClusterId; i++) clusters.Add(new List<Point>());
            foreach (Point p in points)
            {
                if (p.ClusterId > 0) clusters[p.ClusterId - 1].Add(p);
            }
            List<ClusterInfoDTO> clustersInfo = new List<ClusterInfoDTO>();
            foreach (var c in clusters)
            {
                ClusterInfoDTO newCluster = new ClusterInfoDTO() { ProjectName = projectEntity.ProjectName, Data = new List<ClusterDataDTO>(), Users = new List<UserInfoDTO>()};
                foreach (var d in c)
                {
                    newCluster.Data.Add(new ClusterDataDTO() { Topic = (TopicTag)d.TaskTopic, Quality = (int)Math.Round(d.Quality)});
                }
            }
            return clustersInfo;
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
        static bool ExpandCluster(List<Point> points, Point p, int clusterId, double eps, int minPts)
        {
            List<Point> seeds = GetRegion(points, p, eps);
            if (seeds.Count < minPts) // no core point
            {
                p.ClusterId = Point.NOISE;
                return false;
            }
            else // all points in seeds are density reachable from point 'p'
            {
                for (int i = 0; i < seeds.Count; i++) seeds[i].ClusterId = clusterId;
                seeds.Remove(p);
                while (seeds.Count > 0)
                {
                    Point currentP = seeds[0];
                    List<Point> result = GetRegion(points, currentP, eps);
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
                return true;
            }
        }
    }
}
