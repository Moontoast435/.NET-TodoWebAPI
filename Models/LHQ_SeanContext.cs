using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TodoWebAPI;

namespace TodoWebAPI.Models
{
    public partial class LHQ_SeanContext : DbContext
    {
        public LHQ_SeanContext()
        {
        }

        public LHQ_SeanContext(DbContextOptions<LHQ_SeanContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; } = null!;
        public DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=SEANLAPTOP;Initial Catalog=LHQ_Sean;Integrated Security=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GUserIdx)
                    .HasColumnName("gUserIdx")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.SPasswordHash)
                    .IsUnicode(false)
                    .HasColumnName("sPasswordHash")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SUsername)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("sUsername")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Todo>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.description)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.complete)
                    .HasColumnName("complete");

                entity.Property(e => e.userId)
                    .HasColumnName("userId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
