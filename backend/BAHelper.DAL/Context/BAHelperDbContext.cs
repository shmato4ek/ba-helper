﻿using BAHelper.Common.Enums;
using BAHelper.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.DAL.Context
{
    public class BAHelperDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Glossary> Glossaries { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<UserStory> UserStories { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> Tasks { get; set; }
        public DbSet<Subtask> Subtasks { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<TaskTopic> TaskTopics { get; set; }
        public DbSet<Cluster> Clusters { get; set; }
        public DbSet<ClusterData> ClustersData { get; set; }
        public DbSet<StatisticData> Statistics { get; set; }

        public BAHelperDbContext() { }
        public BAHelperDbContext(DbContextOptions<BAHelperDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Configure();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=BAHelperTest2;UserName=postgres;Password=0985883147");
    }
}
