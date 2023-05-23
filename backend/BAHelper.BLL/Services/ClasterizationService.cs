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
    public class ClasterizationService : BaseService
    {
        public ClasterizationService(BAHelperDbContext context, IMapper mapper)
            : base(context, mapper)
        {

        }
        public async Task<List<ClusterInfoDTO>> Cluster(int projectId, int userId)
        {
            var projectEntity = await _context
                .Projects
                .Include(project => project.Users)
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
            double[][] rawData = new double[usersStatistic.Count][];
            for (int i = 0; i < usersStatistic.Count; i++)
            {
                double[] raw = new double[2];
                raw[0] = usersStatistic[i][0];
                raw[1] = usersStatistic[i][1];
                rawData[i] = raw;
            }
            int numClusters = (int)Math.Round((double)projectEntity.Users.Count);
            double[][] data = Normalized(rawData);

            bool changed = true;
            bool success = true;

            int[] clustering = InitClustering(data.Length, numClusters, 0);
            double[][] means = Allocate(numClusters, data[0].Length);

            int maxCount = data.Length * 10;
            int ct = 0;
            while (changed == true && success == true && ct < maxCount)
            {
                ++ct;
                success = UpdateMeans(data, clustering, ref means);
                changed = UpdateClustering(data, ref clustering, means);
            }
            var newClusters = new List<ClusterDTO>();
            for (int i = 0; i < numClusters; i++)
            {
                var addedCluster = new ClusterDTO { Data = new List<ClusterDataDTO>(), ProjectId = projectId, Users = new List<UserDTO>() };
                for (int j = 1; j <= 8; j++)
                {
                    addedCluster.Data.Add(new ClusterDataDTO() { Quality = 0, Topic = (TopicTag)j});
                }
                newClusters.Add(addedCluster);
            }
            for (int i = 0; i < clustering.Length; i++)
            {
                var clusteredUserId = usersStatistic[i][2];
                var clusteredUser = await _context
                    .Users
                    .FirstOrDefaultAsync(user => user.Id == clusteredUserId);
                var foundCluster = newClusters[clustering[i]].Users.FirstOrDefault(user => user.Id == clusteredUserId);
                if (foundCluster == null)
                {
                    newClusters[clustering[i]].Users.Add(_mapper.Map<UserDTO>(clusteredUser));
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
                        cluster.Data[i].Quality = sum / cluster.Users.Count;
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
            return clustersInfo;
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


        private static double[][] Normalized(double[][] rawData)
        {
            double[][] result = new double[rawData.Length][];
            for (int i = 0; i < rawData.Length; ++i)
            {
                result[i] = new double[rawData[i].Length];
                Array.Copy(rawData[i], result[i], rawData[i].Length);
            }

            for (int j = 0; j < result[0].Length; ++j)
            {
                double colSum = 0.0;
                for (int i = 0; i < result.Length; ++i)
                    colSum += result[i][j];
                double mean = colSum / result.Length;
                double sum = 0.0;
                for (int i = 0; i < result.Length; ++i)
                    sum += (result[i][j] - mean) * (result[i][j] - mean);
                double sd = sum / result.Length;
                for (int i = 0; i < result.Length; ++i)
                    result[i][j] = (result[i][j] - mean) / sd;
            }
            return result;
        }
        private static int[] InitClustering(int numTuples, int numClusters, int randomSeed)
        {
            Random random = new Random(randomSeed);
            int[] clustering = new int[numTuples];
            for (int i = 0; i < numClusters; ++i)
                clustering[i] = i;
            for (int i = numClusters; i < clustering.Length; ++i)
                clustering[i] = random.Next(0, numClusters);
            return clustering;
        }
        private static double[][] Allocate(int numClusters, int numColumns)
        {
            double[][] result = new double[numClusters][];
            for (int k = 0; k < numClusters; ++k)
                result[k] = new double[numColumns];
            return result;
        }

        private static bool UpdateMeans(double[][] data, int[] clustering, ref double[][] means)
        {
            int numClusters = means.Length;
            int[] clusterCounts = new int[numClusters];
            for (int i = 0; i < data.Length; ++i)
            {
                int cluster = clustering[i];
                ++clusterCounts[cluster];
            }

            for (int k = 0; k < numClusters; ++k)
                if (clusterCounts[k] == 0)
                    return false;

            for (int k = 0; k < means.Length; ++k)
                for (int j = 0; j < means[k].Length; ++j)
                    means[k][j] = 0.0;

            for (int i = 0; i < data.Length; ++i)
            {
                int cluster = clustering[i];
                for (int j = 0; j < data[i].Length; ++j)
                    means[cluster][j] += data[i][j];
            }

            for (int k = 0; k < means.Length; ++k)
                for (int j = 0; j < means[k].Length; ++j)
                    means[k][j] /= clusterCounts[k];
            return true;
        }

        private static bool UpdateClustering(double[][] data, ref int[] clustering, double[][] means)
        {
            int numClusters = means.Length;
            bool changed = false;

            int[] newClustering = new int[clustering.Length];
            Array.Copy(clustering, newClustering, clustering.Length);

            double[] distances = new double[numClusters];

            for (int i = 0; i < data.Length; ++i)
            {
                for (int k = 0; k < numClusters; ++k)
                    distances[k] = Distance(data[i], means[k]);

                int newClusterID = MinIndex(distances);
                if (newClusterID != newClustering[i])
                {
                    changed = true;
                    newClustering[i] = newClusterID;
                }
            }

            if (changed == false)
                return false;

            int[] clusterCounts = new int[numClusters];
            for (int i = 0; i < data.Length; ++i)
            {
                int cluster = newClustering[i];
                ++clusterCounts[cluster];
            }

            for (int k = 0; k < numClusters; ++k)
                if (clusterCounts[k] == 0)
                    return false;

            Array.Copy(newClustering, clustering, newClustering.Length);
            return true;
        }

        private static double Distance(double[] tuple, double[] mean)
        {
            double sumSquaredDiffs = 0.0;
            for (int j = 0; j < tuple.Length; ++j)
                sumSquaredDiffs += Math.Pow((tuple[j] - mean[j]), 2);
            return Math.Sqrt(sumSquaredDiffs);
        }

        private static int MinIndex(double[] distances)
        {
            int indexOfMin = 0;
            double smallDist = distances[0];
            for (int k = 0; k < distances.Length; ++k)
            {
                if (distances[k] < smallDist)
                {
                    smallDist = distances[k];
                    indexOfMin = k;
                }
            }
            return indexOfMin;
        }
    }
}
