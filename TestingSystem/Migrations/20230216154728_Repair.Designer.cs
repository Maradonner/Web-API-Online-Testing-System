﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestingSystem.Data;

#nullable disable

namespace TestingSystem.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230216154728_Repair")]
    partial class Repair
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TestingSystem.Models.ActiveTrivia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TriviaQuizId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TriviaQuizId");

                    b.ToTable("ActiveTrivias");
                });

            modelBuilder.Entity("TestingSystem.Models.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ActiveTriviaId")
                        .HasColumnType("int");

                    b.Property<int>("CorrectAnswerId")
                        .HasColumnType("int");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<int>("TriviaOptionId")
                        .HasColumnType("int");

                    b.Property<int>("TriviaQuestionId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActiveTriviaId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("TestingSystem.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("TestingSystem.Models.TriviaOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TriviaQuestionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TriviaQuestionId");

                    b.ToTable("TriviaOptions");
                });

            modelBuilder.Entity("TestingSystem.Models.TriviaQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("PictureUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TriviaQuizId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TriviaQuizId");

                    b.ToTable("TriviaQuestions");
                });

            modelBuilder.Entity("TestingSystem.Models.TriviaQuiz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AccumulateTime")
                        .HasColumnType("int");

                    b.Property<int?>("LivesCount")
                        .HasColumnType("int");

                    b.Property<string>("PictureUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionTime")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TriviaQuizs");
                });

            modelBuilder.Entity("TestingSystem.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TokenCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TokenExpired")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TestingSystem.Models.ActiveTrivia", b =>
                {
                    b.HasOne("TestingSystem.Models.TriviaQuiz", "TriviaQuiz")
                        .WithMany()
                        .HasForeignKey("TriviaQuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TriviaQuiz");
                });

            modelBuilder.Entity("TestingSystem.Models.Answer", b =>
                {
                    b.HasOne("TestingSystem.Models.ActiveTrivia", "ActiveTrivia")
                        .WithMany("Answers")
                        .HasForeignKey("ActiveTriviaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ActiveTrivia");
                });

            modelBuilder.Entity("TestingSystem.Models.TriviaOption", b =>
                {
                    b.HasOne("TestingSystem.Models.TriviaQuestion", "TriviaQuestion")
                        .WithMany("Options")
                        .HasForeignKey("TriviaQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TriviaQuestion");
                });

            modelBuilder.Entity("TestingSystem.Models.TriviaQuestion", b =>
                {
                    b.HasOne("TestingSystem.Models.TriviaQuiz", "TriviaQuiz")
                        .WithMany("Questions")
                        .HasForeignKey("TriviaQuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TriviaQuiz");
                });

            modelBuilder.Entity("TestingSystem.Models.User", b =>
                {
                    b.HasOne("TestingSystem.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Role");
                });

            modelBuilder.Entity("TestingSystem.Models.ActiveTrivia", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("TestingSystem.Models.TriviaQuestion", b =>
                {
                    b.Navigation("Options");
                });

            modelBuilder.Entity("TestingSystem.Models.TriviaQuiz", b =>
                {
                    b.Navigation("Questions");
                });
#pragma warning restore 612, 618
        }
    }
}
