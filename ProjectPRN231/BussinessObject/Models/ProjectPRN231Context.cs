using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BussinessObject.Models
{
    public partial class ProjectPRN231Context : IdentityDbContext<User>
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
        public virtual DbSet<Type> Types { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                string connectionStr = config.GetConnectionString("Test");

                optionsBuilder.UseSqlServer(connectionStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasKey(e => e.DocumentId);
                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.FilePath).IsRequired();
                entity.HasOne(d => d.Type)
                    .WithMany(t => t.Documents)
                    .HasForeignKey(d => d.TypeId);
            });

            modelBuilder.Entity<DocumentUser>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Document)
                    .WithMany(d => d.DocumentUsers)
                    .HasForeignKey(e => e.DocumentId);
                entity.HasOne(e => e.User)
                    .WithMany(u => u.DocumentUsers)
                    .HasForeignKey(e => e.UserId);
            });

            modelBuilder.Entity<GroupMember>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NameGroup).IsRequired();
                entity.Property(e => e.IsActive).IsRequired();
                entity.Property(e => e.Note);
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TypeName).IsRequired();
            });

            modelBuilder.Entity<User>()
           .HasOne(u => u.GroupMember)
             .WithMany(g => g.Users)
             .HasForeignKey(u => u.GroupId);


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
