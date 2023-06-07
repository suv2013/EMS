using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LeaveRequestApp.Models
{
    public partial class LeaveAppDBContext : DbContext
    {
        public LeaveAppDBContext()
        {
        }

        public LeaveAppDBContext(DbContextOptions<LeaveAppDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LeaveRequest> LeaveRequest { get; set; }
        public virtual DbSet<LeaveRequestApproval> LeaveRequestApproval { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserTeam> UserTeam { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<LeaveRequest>(entity =>
            {
                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.RequestDetails)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LeaveRequest)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LeaveRequ__UserI__4F7CD00D");
            });

            modelBuilder.Entity<LeaveRequestApproval>(entity =>
            {
                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.HasOne(d => d.Approver)
                    .WithMany(p => p.LeaveRequestApproval)
                    .HasForeignKey(d => d.ApproverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LeaveRequ__Appro__5629CD9C");

                entity.HasOne(d => d.LeaveRequest)
                    .WithMany(p => p.LeaveRequestApproval)
                    .HasForeignKey(d => d.LeaveRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LeaveRequ__Leave__5535A963");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.Property(e => e.TeamName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserRole__RoleId__3E52440B");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserRole__UserId__3D5E1FD2");
            });

            //modelBuilder.Entity<UserRole>().HasOne(s => s.User);

            modelBuilder.Entity<UserTeam>(entity =>
            {
                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.UserTeam)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserTeam__TeamId__4222D4EF");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTeam)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserTeam__UserId__412EB0B6");
            });
        }
    }
}
