﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Web_News.Models;

#nullable disable

namespace Web_News.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Web_News.Models.Advertisement", b =>
                {
                    b.Property<int>("AdvertisementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdvertisementId"));

                    b.Property<int>("ApprovalStatus")
                        .HasColumnType("int");

                    b.Property<int?>("ApprovedByUserId")
                        .HasColumnType("int");

                    b.Property<int>("BannerPosition")
                        .HasColumnType("int");

                    b.Property<string>("ContactName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("AdvertisementId");

                    b.HasIndex("ApprovedByUserId");

                    b.ToTable("Advertisements");
                });

            modelBuilder.Entity("Web_News.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Describe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameCategory")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("ParentCategoryId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 50,
                            Describe = "Latest technology news",
                            NameCategory = "Công nghệ"
                        },
                        new
                        {
                            CategoryId = 51,
                            Describe = "Health and wellness tips",
                            NameCategory = "Sức khỏe"
                        });
                });

            modelBuilder.Entity("Web_News.Models.News", b =>
                {
                    b.Property<int>("NewsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NewsId"));

                    b.Property<int>("ApprovalStatus")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RejectionReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("NewsId");

                    b.HasIndex("CreatedByUserId");

                    b.ToTable("News");

                    b.HasData(
                        new
                        {
                            NewsId = 50,
                            ApprovalStatus = 1,
                            Content = "Công nghệ mới đang được phát triển toàn cầu.",
                            CreatedByUserId = 1,
                            Image = "ct.jpeg",
                            PublishDate = new DateTime(2024, 9, 21, 13, 11, 28, 557, DateTimeKind.Local).AddTicks(4504),
                            Status = true,
                            Title = "Công nghệ mới"
                        },
                        new
                        {
                            NewsId = 51,
                            ApprovalStatus = 1,
                            Content = "Sức khỏe cộng đồng Sức khỏe cộng đồng.",
                            CreatedByUserId = 1,
                            Image = "ct.jpeg",
                            PublishDate = new DateTime(2024, 9, 21, 13, 11, 28, 557, DateTimeKind.Local).AddTicks(4507),
                            Status = true,
                            Title = "Sức khỏe cộng đồng"
                        });
                });

            modelBuilder.Entity("Web_News.Models.NewsCategory", b =>
                {
                    b.Property<int>("NewsId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.HasKey("NewsId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("NewsCategories");
                });

            modelBuilder.Entity("Web_News.Models.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleID"));

                    b.Property<string>("Describe")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("NameRole")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleID = 1,
                            Describe = "Administrator Role",
                            NameRole = "Admin"
                        },
                        new
                        {
                            RoleID = 2,
                            Describe = "Vai trò người dùng trải nghiệm",
                            NameRole = "User"
                        },
                        new
                        {
                            RoleID = 3,
                            Describe = "Vai trò người viết tin tức",
                            NameRole = "Reporter"
                        },
                        new
                        {
                            RoleID = 4,
                            Describe = "Vai trò người kiểm duyệt tin tức",
                            NameRole = "Editor"
                        });
                });

            modelBuilder.Entity("Web_News.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("Address")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("IdFacebook")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("IdGoogle")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PasswordResetCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ResetCodeExpiration")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserID = 1,
                            Email = "admin@gmail.com",
                            Name = "Administrator",
                            Password = "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=",
                            RegistrationDate = new DateTime(2024, 9, 21, 13, 11, 28, 557, DateTimeKind.Local).AddTicks(4334),
                            UserName = "admin"
                        },
                        new
                        {
                            UserID = 2,
                            Email = "hngoctro@gmail.com",
                            Name = "Huỳnh Ngọc Trợ",
                            Password = "71EwYhTZpjYe4dW0UubSu3DcfruFv54Cw9R0f7V9a+w=",
                            RegistrationDate = new DateTime(2024, 9, 21, 13, 11, 28, 557, DateTimeKind.Local).AddTicks(4365),
                            UserName = "NgocTro"
                        },
                        new
                        {
                            UserID = 3,
                            Email = "phucbin366@gmail.com",
                            Name = "Trần Văn Phúc",
                            Password = "71EwYhTZpjYe4dW0UubSu3DcfruFv54Cw9R0f7V9a+w=",
                            RegistrationDate = new DateTime(2024, 9, 21, 13, 11, 28, 557, DateTimeKind.Local).AddTicks(4379),
                            UserName = "VanPhuc"
                        },
                        new
                        {
                            UserID = 4,
                            Email = "caothiphuongvy27@gmail.com",
                            Name = "Cao Thị Phương Vy",
                            Password = "71EwYhTZpjYe4dW0UubSu3DcfruFv54Cw9R0f7V9a+w=",
                            RegistrationDate = new DateTime(2024, 9, 21, 13, 11, 28, 557, DateTimeKind.Local).AddTicks(4393),
                            UserName = "PhuongVy"
                        },
                        new
                        {
                            UserID = 5,
                            Email = "nguyenngocquy182752@gmail.com",
                            Name = "Nguyễn Thị Ngọc Quý",
                            Password = "71EwYhTZpjYe4dW0UubSu3DcfruFv54Cw9R0f7V9a+w=",
                            RegistrationDate = new DateTime(2024, 9, 21, 13, 11, 28, 557, DateTimeKind.Local).AddTicks(4406),
                            UserName = "NgocQuy"
                        },
                        new
                        {
                            UserID = 6,
                            Email = "reporter@gmail.com",
                            Name = "Nguyễn Văn Ánh",
                            Password = "71EwYhTZpjYe4dW0UubSu3DcfruFv54Cw9R0f7V9a+w=",
                            RegistrationDate = new DateTime(2024, 9, 21, 13, 11, 28, 557, DateTimeKind.Local).AddTicks(4419),
                            UserName = "reporter"
                        },
                        new
                        {
                            UserID = 7,
                            Email = "editor@gmail.com",
                            Name = "Cao Văn Lãnh",
                            Password = "71EwYhTZpjYe4dW0UubSu3DcfruFv54Cw9R0f7V9a+w=",
                            RegistrationDate = new DateTime(2024, 9, 21, 13, 11, 28, 557, DateTimeKind.Local).AddTicks(4432),
                            UserName = "editor"
                        });
                });

            modelBuilder.Entity("Web_News.Models.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            RoleId = 1
                        },
                        new
                        {
                            UserId = 2,
                            RoleId = 3
                        },
                        new
                        {
                            UserId = 3,
                            RoleId = 2
                        },
                        new
                        {
                            UserId = 4,
                            RoleId = 3
                        },
                        new
                        {
                            UserId = 5,
                            RoleId = 2
                        },
                        new
                        {
                            UserId = 6,
                            RoleId = 3
                        },
                        new
                        {
                            UserId = 7,
                            RoleId = 4
                        });
                });

            modelBuilder.Entity("Web_News.Models.Advertisement", b =>
                {
                    b.HasOne("Web_News.Models.User", "ApprovedByUser")
                        .WithMany()
                        .HasForeignKey("ApprovedByUserId");

                    b.Navigation("ApprovedByUser");
                });

            modelBuilder.Entity("Web_News.Models.Category", b =>
                {
                    b.HasOne("Web_News.Models.Category", "ParentCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentCategoryId");

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("Web_News.Models.News", b =>
                {
                    b.HasOne("Web_News.Models.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedByUser");
                });

            modelBuilder.Entity("Web_News.Models.NewsCategory", b =>
                {
                    b.HasOne("Web_News.Models.Category", "Category")
                        .WithMany("NewsCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web_News.Models.News", "News")
                        .WithMany("NewsCategories")
                        .HasForeignKey("NewsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("News");
                });

            modelBuilder.Entity("Web_News.Models.UserRole", b =>
                {
                    b.HasOne("Web_News.Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web_News.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Web_News.Models.Category", b =>
                {
                    b.Navigation("NewsCategories");

                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("Web_News.Models.News", b =>
                {
                    b.Navigation("NewsCategories");
                });

            modelBuilder.Entity("Web_News.Models.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Web_News.Models.User", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
