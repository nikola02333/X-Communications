using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCommunications.Models
{
    public partial class XCommunicationsContext : DbContext
    {
        public XCommunicationsContext()
        {
        }

        public XCommunicationsContext(DbContextOptions<XCommunicationsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contract> Contract { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Number> Number { get; set; }
        public virtual DbSet<RegistratedUser> RegistratedUser { get; set; }
        public virtual DbSet<Simcard> Simcard { get; set; }
        public virtual DbSet<Worker> Worker { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=INTERNSHIP12\\SQLEXPRESS;Database=XCommunications;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contract>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Tarif)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WorkerId).HasColumnName("WorkerID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Contract)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contract_Customer");

                entity.HasOne(d => d.Worker)
                    .WithMany(p => p.Contract)
                    .HasForeignKey(d => d.WorkerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contract_Worker");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Number>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Cc).HasColumnName("CC");

                entity.Property(e => e.Ndc).HasColumnName("NDC");

                entity.Property(e => e.Sn).HasColumnName("SN");
            });

            modelBuilder.Entity<RegistratedUser>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Imsi).HasColumnName("IMSI");

                entity.HasOne(d => d.IdentificationCardNavigation)
                    .WithMany(p => p.RegistratedUser)
                    .HasForeignKey(d => d.IdentificationCard)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RegistratedUser_Customer");

                entity.HasOne(d => d.ImsiNavigation)
                    .WithMany(p => p.RegistratedUser)
                    .HasForeignKey(d => d.Imsi)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RegistratedUser_SIMCard");

                entity.HasOne(d => d.WorkerNavigation)
                    .WithMany(p => p.RegistratedUser)
                    .HasForeignKey(d => d.Worker)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RegistratedUser_Worker");
            });

            modelBuilder.Entity<Simcard>(entity =>
            {
                entity.HasKey(e => e.Imsi);

                entity.ToTable("SIMCard");

                entity.Property(e => e.Imsi)
                    .HasColumnName("IMSI")
                    .ValueGeneratedNever();

                entity.Property(e => e.Iccid).HasColumnName("ICCID");

                entity.Property(e => e.Pin).HasColumnName("PIN");

                entity.Property(e => e.Puk).HasColumnName("PUK");
            });

            modelBuilder.Entity<Worker>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Operater)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
