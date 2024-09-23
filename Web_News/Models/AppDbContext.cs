
using Microsoft.EntityFrameworkCore;
using Web_News.Services.PasswordH;

namespace Web_News.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  Thiết Lập khóa cho Bảng            ROLE - USER - USERROLE
            modelBuilder.Entity<UserRole>()
           .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);
            // Thiết Lập khóa cho Bảng            Category - News
            modelBuilder.Entity<NewsCategory>()
                .HasKey(nc => new { nc.NewsId, nc.CategoryId });

            modelBuilder.Entity<NewsCategory>()
                .HasOne(nc => nc.News)
                .WithMany(n => n.NewsCategories)
                .HasForeignKey(nc => nc.NewsId);

            modelBuilder.Entity<NewsCategory>()
                .HasOne(nc => nc.Category)
                .WithMany(c => c.NewsCategories)
                .HasForeignKey(nc => nc.CategoryId);

            // Sử dụng để thêm dữ liệu cho Bảng     ROLE - USER - USERROLE
            // Seed dữ liệu cho bảng Role
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleID = 1, NameRole = "Admin", Describe = "Administrator Role" },
                new Role { RoleID = 2, NameRole = "User", Describe = "Vai trò người dùng trải nghiệm" },
                new Role { RoleID = 3, NameRole = "Reporter", Describe = "Vai trò người viết tin tức" },
                new Role { RoleID = 4, NameRole = "Editor", Describe = "Vai trò người kiểm duyệt tin tức" }

            );
            // Seed dữ liệu cho bảng User 
            modelBuilder.Entity<User>().HasData(

                new User { UserID = 1, Name = "Administrator", Email = "admin@gmail.com", UserName = "admin", Password = PasswordHasher.HashPassword("admin"), RegistrationDate = DateTime.Now },
                new User { UserID = 2, Name = "Huỳnh Ngọc Trợ", Email = "hngoctro@gmail.com", UserName = "NgocTro", Password = PasswordHasher.HashPassword("1234qwer"), RegistrationDate = DateTime.Now },
                new User { UserID = 3, Name = "Trần Văn Phúc", Email = "phucbin366@gmail.com", UserName = "VanPhuc", Password = PasswordHasher.HashPassword("1234qwer"), RegistrationDate = DateTime.Now },
                new User { UserID = 4, Name = "Cao Thị Phương Vy", Email = "caothiphuongvy27@gmail.com", UserName = "PhuongVy", Password = PasswordHasher.HashPassword("1234qwer"), RegistrationDate = DateTime.Now },
                new User { UserID = 5, Name = "Nguyễn Thị Ngọc Quý", Email = "nguyenngocquy182752@gmail.com", UserName = "NgocQuy", Password = PasswordHasher.HashPassword("1234qwer"), RegistrationDate = DateTime.Now },
                 new User { UserID = 6, Name = "Nguyễn Văn Ánh", Email = "reporter@gmail.com", UserName = "reporter", Password = PasswordHasher.HashPassword("1234qwer"), RegistrationDate = DateTime.Now },
                  new User { UserID = 7, Name = "Cao Văn Lãnh", Email = "editor@gmail.com", UserName = "editor", Password = PasswordHasher.HashPassword("1234qwer"), RegistrationDate = DateTime.Now }
            );

            // Seed dữ liệu cho bảng UserRole
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { UserId = 1, RoleId = 1 },
                new UserRole { UserId = 2, RoleId = 3 },
                new UserRole { UserId = 3, RoleId = 2 },
                new UserRole { UserId = 4, RoleId = 3 },
                new UserRole { UserId = 5, RoleId = 2 },
                new UserRole { UserId = 6, RoleId = 3 },
                new UserRole { UserId = 7, RoleId = 4 }
            );
            // Seed dữ liệu cho bảng Category
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 50, NameCategory = "Công nghệ", Describe = "Latest technology news", ParentCategoryId = null },
                new Category { CategoryId = 51, NameCategory = "Sức khỏe", Describe = "Health and wellness tips", ParentCategoryId = null }
            );
            // Seed dữ liệu cho bảng News
            modelBuilder.Entity<News>().HasData(
                new News
                {
                    NewsId = 50,
                    Title = "Công nghệ mới",
                    PublishDate = DateTime.Now,
                    Content = "Công nghệ mới đang được phát triển toàn cầu.",
                    Image = "ct.jpeg",
                    CreatedByUserId = 1,
                    Status = true,
                    ApprovalStatus = ApprovalStatus.Approved,
                    RejectionReason = null
                },
                new News
                {
                    NewsId = 51,
                    Title = "Sức khỏe cộng đồng",
                    PublishDate = DateTime.Now,
                    Content = "Sức khỏe cộng đồng Sức khỏe cộng đồng.",
                    Image = "ct.jpeg",
                    CreatedByUserId = 1,
                    Status = true,
                    ApprovalStatus = ApprovalStatus.Approved,
                    RejectionReason = null
                }
            );
            // ----------------------------------------------------------------------
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<NewsCategory> NewsCategories { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
    }
}
