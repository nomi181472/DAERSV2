﻿// <auto-generated />
using System;
using DAERS.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAERS.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200525114138_addIsMain")]
    partial class addIsMain
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799");

            modelBuilder.Entity("DAERS.API.Models.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category");

                    b.Property<int>("Level");

                    b.Property<string>("Name");

                    b.Property<string>("Type");

                    b.Property<string>("UsedFor");

                    b.HasKey("Id");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("DAERS.API.Models.PhotoE", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("Description");

                    b.Property<int>("ExerciseId");

                    b.Property<bool>("IsMain");

                    b.Property<string>("PublicEId");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.ToTable("PhotosEx");
                });

            modelBuilder.Entity("DAERS.API.Models.PhotoU", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("Description");

                    b.Property<bool>("IsMain");

                    b.Property<string>("PublicId");

                    b.Property<string>("Url");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("DAERS.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<float>("Bmi");

                    b.Property<string>("Category");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<DateTime>("Created");

                    b.Property<string>("Email");

                    b.Property<string>("Gender");

                    b.Property<float>("Height");

                    b.Property<string>("Introduction");

                    b.Property<DateTime>("LastActive");

                    b.Property<float>("Lats");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("UserName");

                    b.Property<float>("Waist");

                    b.Property<float>("Weight");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DAERS.API.Models.PhotoE", b =>
                {
                    b.HasOne("DAERS.API.Models.Exercise", "Exercise")
                        .WithMany("PhotosE")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAERS.API.Models.PhotoU", b =>
                {
                    b.HasOne("DAERS.API.Models.User", "User")
                        .WithMany("Photos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
