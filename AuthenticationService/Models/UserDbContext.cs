using Microsoft.EntityFrameworkCore;

namespace ClaimUserService.Models
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> option): base(option)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id = 1,
                    UserName = "user1",
                    Password = "user1"
                },
                new User
                {
                    Id = 2,
                    UserName = "user2",
                    Password = "user2"
                },
                new User
                {
                    Id = 3,
                    UserName = "user3",
                    Password = "user3"
                }
                );
        }

        }
    }
