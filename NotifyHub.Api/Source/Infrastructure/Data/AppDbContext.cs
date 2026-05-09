using Microsoft.EntityFrameworkCore;
using NotifyHub.Api.Source.Domain.Entities;

namespace NotifyHub.Api.Source.Infrastructure.Data
{
    public class AppDbContext:DbContext
    {
        // Constructor - receives database options
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        #region DbSet
        public DbSet<Call> Calls { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Stack> Stacks { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Call table
            modelBuilder.Entity<Call>(entity =>
            {
                // Primary key
                entity.HasKey(e => e.CallID);

                // Column configurations
                entity.Property(e => e.CallID)
                    .HasColumnType("uniqueidentifier");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.LandMark)
                    .HasMaxLength(500);

                entity.Property(e => e.Type)
                    .IsRequired();

                entity.Property(e => e.Status)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Comments)
                    .HasMaxLength(1000);

                entity.Property(e => e.CreatedDttm)
                    .IsRequired()
                    .HasColumnType("datetime2");

                entity.Property(e => e.UpdatedDttm)
                    .IsRequired()
                    .HasColumnType("datetime2");
            });

            // Configure Unit table
            modelBuilder.Entity<Unit>(entity =>
            {
                entity.HasKey(e => e.UnitID);

                entity.Property(e => e.UnitID)
                    .ValueGeneratedOnAdd(); // if int

                entity.Property(e => e.UnitName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(u => u.StkNbr)
                    .IsUnique();

                entity.Property(e => e.UnitType)
                    .IsRequired();

                entity.Property(e => e.IsActive)
                    .IsRequired();

                entity.Property(e => e.IsAvail)
                    .IsRequired();

                entity.Property(e => e.CreatedOn)
                    .IsRequired()
                    .HasColumnType("datetime2");

                entity.Property(e => e.UpdatedOn)
                    .IsRequired()
                    .HasColumnType("datetime2");
            });
            // Configure Stack table
            modelBuilder.Entity<Stack>(entity =>
            {
                // Primary Key
                entity.HasKey(e => e.StackID);

                entity.Property(e => e.StackID)
                    .ValueGeneratedOnAdd(); // for Guid

                // Foreign Key → Call
                entity.Property(e => e.CallID)
                    .IsRequired();

                entity.HasOne<Call>()
                    .WithMany()
                    .HasForeignKey(s => s.CallID);

                // 🔥 Index for FK (important)
                entity.HasIndex(e => e.CallID);

                // StkNbr
                entity.Property(e => e.StkNbr)
                    .IsRequired();

                entity.HasIndex(e => e.StkNbr)
                    .IsUnique();

                // Location
                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(500);

                // Enum
                entity.Property(e => e.Status)
                    .IsRequired();

                // Dates
                entity.Property(e => e.CreatedDttm)
                    .IsRequired()
                    .HasColumnType("datetime2");

                entity.Property(e => e.UpdatedDttm)
                    .IsRequired()
                    .HasColumnType("datetime2");
            });

            // Configure User table
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId)
                    .ValueGeneratedOnAdd(); // ✅ correct for Guid

                entity.Property(e => e.Username)
                    .IsRequired();

                entity.Property(e => e.Password)
                    .IsRequired();

                entity.Property(e => e.Role)
                    .IsRequired();
            });
            // Configure Notification table
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.NotifyId);

                entity.Property(e => e.NotifyId)
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Message)
                      .IsRequired()
                      .HasMaxLength(500);

                entity.Property(e => e.UserId)
                      .IsRequired();

                entity.Property(e => e.IsRead)
                      .IsRequired();

                entity.Property(e => e.CreatedAt)
                      .IsRequired();
            });
        }
    }
}
