using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TextFinder.Models
{
    public partial class GisMasterTranslationLogsContext : DbContext
    {
        public GisMasterTranslationLogsContext()
        {
        }

        public GisMasterTranslationLogsContext(DbContextOptions<GisMasterTranslationLogsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ExceptionLog> ExceptionLogs { get; set; }
        public virtual DbSet<FileNotExistException> FileNotExistExceptions { get; set; }
        public virtual DbSet<MatchNotFoundException> MatchNotFoundExceptions { get; set; }
        public virtual DbSet<SuccessfullyUpdatedLog> SuccessfullyUpdatedLogs { get; set; }
        public virtual DbSet<TranslationData> TranslationDatas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=PC0609\\MSSQL2019;Initial Catalog=GisMasterTranslationLogs;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<ExceptionLog>(entity =>
            {
                entity.Property(e => e.Control)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FileName).IsRequired();

                entity.Property(e => e.InputText).IsRequired();

                entity.Property(e => e.TranslatedText).IsRequired();
            });

            modelBuilder.Entity<FileNotExistException>(entity =>
            {
                entity.Property(e => e.Control)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FileName).IsRequired();

                entity.Property(e => e.InputText).IsRequired();

                entity.Property(e => e.TranslatedText).IsRequired();
            });

            modelBuilder.Entity<MatchNotFoundException>(entity =>
            {
                entity.Property(e => e.Control)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FileName).IsRequired();

                entity.Property(e => e.InputText).IsRequired();

                entity.Property(e => e.TranslatedText).IsRequired();
            });

            modelBuilder.Entity<SuccessfullyUpdatedLog>(entity =>
            {
                entity.Property(e => e.Control)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FileName).IsRequired();

                entity.Property(e => e.InputText).IsRequired();

                entity.Property(e => e.TranslatedText).IsRequired();
            });

            modelBuilder.Entity<TranslationData>(entity =>
            {
                entity.Property(e => e.Control)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FileName).IsRequired();

                entity.Property(e => e.InputText).IsRequired();

                entity.Property(e => e.TranslatedText).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
