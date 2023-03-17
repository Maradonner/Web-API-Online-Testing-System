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
    [Migration("20221203185425_sfdsdfgfdgааывfdsg")]
    partial class sfdsdfgfdgааывfdsg
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TestingSystem.Models.TriviaAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TriviaAnswers");
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

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TriviaQuizId")
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

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TriviaQuizs");
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
                    b.HasOne("TestingSystem.Models.TriviaQuiz", null)
                        .WithMany("Questions")
                        .HasForeignKey("TriviaQuizId");
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
