﻿// <auto-generated />
using System;
using BAHelper.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BAHelper.DAL.Migrations
{
    [DbContext(typeof(BAHelperDbContext))]
    [Migration("20230417085005_AddedPasswordToUsers")]
    partial class AddedPasswordToUsers
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BAHelper.DAL.Entities.AcceptanceCriteria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserStoryId")
                        .HasColumnType("integer");

                    b.Property<int?>("UserStoryId1")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserStoryId");

                    b.HasIndex("UserStoryId1");

                    b.ToTable("AcceptanceCriterias");
                });

            modelBuilder.Entity("BAHelper.DAL.Entities.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProjectAim")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId1")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("BAHelper.DAL.Entities.Glossary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Definition")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("DocumentId")
                        .HasColumnType("integer");

                    b.Property<int?>("DocumentId1")
                        .HasColumnType("integer");

                    b.Property<string>("Term")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.HasIndex("DocumentId1");

                    b.ToTable("Glossaries");
                });

            modelBuilder.Entity("BAHelper.DAL.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("BAHelper.DAL.Entities.ProjectTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Deadine")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<int?>("ProjectId1")
                        .HasColumnType("integer");

                    b.Property<string>("TaskName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TaskState")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ProjectId1");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("BAHelper.DAL.Entities.Subtask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ProjectTaskId")
                        .HasColumnType("integer");

                    b.Property<int>("TaskId")
                        .HasColumnType("integer");

                    b.Property<int>("TaskState")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProjectTaskId");

                    b.HasIndex("TaskId");

                    b.ToTable("Subtasks");
                });

            modelBuilder.Entity("BAHelper.DAL.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BAHelper.DAL.Entities.UserStory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DocumentId")
                        .HasColumnType("integer");

                    b.Property<int?>("DocumentId1")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.HasIndex("DocumentId1");

                    b.ToTable("UserStories");
                });

            modelBuilder.Entity("BAHelper.DAL.Entities.UserStoryFormula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserStoryId")
                        .HasColumnType("integer");

                    b.Property<int?>("UserStoryId1")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserStoryId");

                    b.HasIndex("UserStoryId1");

                    b.ToTable("UserStoryFormulas");
                });

            modelBuilder.Entity("ProjectTaskUser", b =>
                {
                    b.Property<int>("TasksId")
                        .HasColumnType("integer");

                    b.Property<int>("UsersId")
                        .HasColumnType("integer");

                    b.HasKey("TasksId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("ProjectTaskUser");
                });

            modelBuilder.Entity("ProjectUser", b =>
                {
                    b.Property<int>("ProjectsId")
                        .HasColumnType("integer");

                    b.Property<int>("UsersId")
                        .HasColumnType("integer");

                    b.HasKey("ProjectsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("ProjectUser");
                });

            modelBuilder.Entity("BAHelper.DAL.Entities.AcceptanceCriteria", b =>
                {
                    b.HasOne("BAHelper.DAL.Entities.UserStory", null)
                        .WithMany()
                        .HasForeignKey("UserStoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BAHelper.DAL.Entities.UserStory", null)
                        .WithMany("AcceptanceCriterias")
                        .HasForeignKey("UserStoryId1");
                });

            modelBuilder.Entity("BAHelper.DAL.Entities.Document", b =>
                {
                    b.HasOne("BAHelper.DAL.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BAHelper.DAL.Entities.User", null)
                        .WithMany("Documents")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("BAHelper.DAL.Entities.Glossary", b =>
                {
                    b.HasOne("BAHelper.DAL.Entities.Document", null)
                        .WithMany()
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BAHelper.DAL.Entities.Document", null)
                        .WithMany("Glossary")
                        .HasForeignKey("DocumentId1");
                });

            modelBuilder.Entity("BAHelper.DAL.Entities.Project", b =>
                {
                    b.HasOne("BAHelper.DAL.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BAHelper.DAL.Entities.ProjectTask", b =>
                {
                    b.HasOne("BAHelper.DAL.Entities.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BAHelper.DAL.Entities.Project", null)
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId1");
                });

            modelBuilder.Entity("BAHelper.DAL.Entities.Subtask", b =>
                {
                    b.HasOne("BAHelper.DAL.Entities.ProjectTask", null)
                        .WithMany("Subtasks")
                        .HasForeignKey("ProjectTaskId");

                    b.HasOne("BAHelper.DAL.Entities.ProjectTask", null)
                        .WithMany()
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BAHelper.DAL.Entities.UserStory", b =>
                {
                    b.HasOne("BAHelper.DAL.Entities.Document", null)
                        .WithMany()
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BAHelper.DAL.Entities.Document", null)
                        .WithMany("UserStories")
                        .HasForeignKey("DocumentId1");
                });

            modelBuilder.Entity("BAHelper.DAL.Entities.UserStoryFormula", b =>
                {
                    b.HasOne("BAHelper.DAL.Entities.UserStory", null)
                        .WithMany()
                        .HasForeignKey("UserStoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BAHelper.DAL.Entities.UserStory", null)
                        .WithMany("Formulas")
                        .HasForeignKey("UserStoryId1");
                });

            modelBuilder.Entity("ProjectTaskUser", b =>
                {
                    b.HasOne("BAHelper.DAL.Entities.ProjectTask", null)
                        .WithMany()
                        .HasForeignKey("TasksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BAHelper.DAL.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectUser", b =>
                {
                    b.HasOne("BAHelper.DAL.Entities.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BAHelper.DAL.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BAHelper.DAL.Entities.Document", b =>
                {
                    b.Navigation("Glossary");

                    b.Navigation("UserStories");
                });

            modelBuilder.Entity("BAHelper.DAL.Entities.Project", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("BAHelper.DAL.Entities.ProjectTask", b =>
                {
                    b.Navigation("Subtasks");
                });

            modelBuilder.Entity("BAHelper.DAL.Entities.User", b =>
                {
                    b.Navigation("Documents");
                });

            modelBuilder.Entity("BAHelper.DAL.Entities.UserStory", b =>
                {
                    b.Navigation("AcceptanceCriterias");

                    b.Navigation("Formulas");
                });
#pragma warning restore 612, 618
        }
    }
}
