using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementDomain.Entity;

namespace TaskManagementApplication.Data
{
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyTask>().HasKey(x => new
            {
                x.Id
            }
            );
            modelBuilder.Entity<MyTask>().HasOne(x => x.User).WithMany(x => x.Tasks).HasForeignKey(x => x.UserId);

            modelBuilder.Entity<MyTask>().HasKey(x => new
            {
                x.Id
            }
           );
            modelBuilder.Entity<MyTask>().HasOne(x => x.Project).WithMany(x => x.Tasks).HasForeignKey(x => x.ProjectId);

            modelBuilder.Entity<Notification>().HasKey(x => new
            {
                x.Id
            }
           );
            modelBuilder.Entity<Notification>().HasOne(x => x.User).WithMany(x => x.Notifications).HasForeignKey(x => x.UserId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<MyTask> MyTasks { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}
