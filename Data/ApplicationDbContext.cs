using LeHuuTam.Models;
using Microsoft.EntityFrameworkCore;

namespace LeHuuTam.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Products> Products { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình cho Camera
            modelBuilder.Entity<Products>()
                .Property(c => c.Price)
                .HasColumnType("decimal(18, 2)");

            // Cấu hình cho User
            modelBuilder.Entity<Users>()
                .Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Users>()
                .Property(u => u.Email)
                .HasMaxLength(100);  // Không có IsRequired(), cho phép NULL

            modelBuilder.Entity<Users>()
                .Property(u => u.Password)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
