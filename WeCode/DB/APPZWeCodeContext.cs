using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WeCode
{
    public partial class APPZWeCodeContext : DbContext
    {
        public APPZWeCodeContext()
        {
        }

        public APPZWeCodeContext(DbContextOptions<APPZWeCodeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActualResult> ActualResults { get; set; }
        public virtual DbSet<CodeBlock> CodeBlocks { get; set; }
        public virtual DbSet<ExpectedResult> ExpectedResults { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TaskResult> TaskResults { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.

                //optionsBuilder.UseSqlServer();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Ukrainian_CI_AS");

            modelBuilder.Entity<ActualResult>(entity =>
            {
                entity.ToTable("ActualResult");

                entity.Property(e => e.ActualResultId).ValueGeneratedNever();

                entity.HasOne(d => d.CodeBlock)
                    .WithMany(p => p.ActualResults)
                    .HasForeignKey(d => d.CodeBlockId)
                    .HasConstraintName("R_12");

                entity.HasOne(d => d.TaskResult)
                    .WithMany(p => p.ActualResults)
                    .HasForeignKey(d => d.TaskResultId)
                    .HasConstraintName("R_8");
            });

            modelBuilder.Entity<CodeBlock>(entity =>
            {
                entity.ToTable("CodeBlock");

                entity.Property(e => e.CodeBlockId).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ExpectedResult>(entity =>
            {
                entity.ToTable("ExpectedResult");

                entity.Property(e => e.ExpectedResultId).ValueGeneratedNever();

                entity.HasOne(d => d.CodeBlock)
                    .WithMany(p => p.ExpectedResults)
                    .HasForeignKey(d => d.CodeBlockId)
                    .HasConstraintName("R_10");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.ExpectedResults)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("R_6");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("Task");

                entity.Property(e => e.TaskId).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("R_13");
            });

            modelBuilder.Entity<TaskResult>(entity =>
            {
                entity.ToTable("TaskResult");

                entity.Property(e => e.TaskResultId).ValueGeneratedNever();

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(d => d.SubmittedByNavigation)
                    .WithMany(p => p.TaskResults)
                    .HasForeignKey(d => d.SubmittedBy)
                    .HasConstraintName("R_4");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.TaskResults)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("R_3");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("R_2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
