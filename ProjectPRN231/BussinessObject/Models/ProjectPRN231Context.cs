using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace BussinessObject.Models
{
    public partial class ProjectPRN231Context : DbContext
    {
        public ProjectPRN231Context()
        {
        }

        public ProjectPRN231Context(DbContextOptions<ProjectPRN231Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Document> Documents { get; set; } = null!;
        public virtual DbSet<DocumentUser> DocumentUsers { get; set; } = null!;
        public virtual DbSet<GroupMember> GroupMembers { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Type> Types { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                string ConnectionStr = config.GetConnectionString("Test");

                optionsBuilder.UseSqlServer(ConnectionStr);
            }


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>(entity =>
            {
                entity.Property(e => e.DocumentId).HasColumnName("document_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.FilePath)
                    .HasMaxLength(500)
                    .HasColumnName("file_path");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasMaxLength(300)
                    .HasColumnName("title");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Documents__type___3F466844");
            });

            modelBuilder.Entity<DocumentUser>(entity =>
            {
                entity.ToTable("Document_User");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DocumentId).HasColumnName("document_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.DocumentUsers)
                    .HasForeignKey(d => d.DocumentId)
                    .HasConstraintName("FK__Document___docum__4222D4EF");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DocumentUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Document___user___4316F928");
            });

            modelBuilder.Entity<GroupMember>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NameGroup)
                    .HasMaxLength(300)
                    .HasColumnName("name_group");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GroupMembers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__GroupMemb__user___45F365D3");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole)
                    .HasName("PK__Roles__3D48441DCE544E02");

                entity.Property(e => e.IdRole).HasColumnName("id_role");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(100)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(100)
                    .HasColumnName("type_name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ__Users__AB6E6164A22C7DC7")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(300)
                    .HasColumnName("display_name");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.IdRole).HasColumnName("id_role");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("password");

                entity.Property(e => e.UserName)
                    .HasMaxLength(300)
                    .HasColumnName("user_name");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users__id_role__398D8EEE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
