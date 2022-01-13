using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace VodilaASPApp.Models
{
    public partial class VodilaContext : DbContext
    {
        public VodilaContext()
        {
        }

        public VodilaContext(DbContextOptions<VodilaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<Shippment> Shippments { get; set; }
        public virtual DbSet<Shippmentsdriver> Shippmentsdrivers { get; set; }
        public virtual DbSet<Useraccount> Useraccounts { get; set; }
        public virtual DbSet<Userconfidential> Userconfidentials { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Vodila;Username=postgres;Password=Ulolaz42");
                optionsBuilder.UseNpgsql("Host=ec2-63-32-30-191.eu-west-1.compute.amazonaws.com;" +
                    "Port=5432;Database=d160tv7t557vsg;Username=mwrfuoxburwlbp;" +
                    "Password=e75bba1e9fd4656ab84a172a0714c166416678017d9b6ccc1c309b3a2f085bfd;" +
                    "SSL Mode=Require;Trust Server Certificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Russia.1251");

            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("car");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.ToTable("route");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Arrivalplace)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("arrivalplace");

                entity.Property(e => e.Departureplace)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("departureplace");

                entity.Property(e => e.Distance).HasColumnName("distance");

                entity.Property(e => e.Payment).HasColumnName("payment");
            });

            modelBuilder.Entity<Shippment>(entity =>
            {
                entity.ToTable("shippment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Arrivaltime).HasColumnName("arrivaltime");

                entity.Property(e => e.Bonus).HasColumnName("bonus");

                entity.Property(e => e.Carid).HasColumnName("carid");

                entity.Property(e => e.Departuretime).HasColumnName("departuretime");

                entity.Property(e => e.Preferedarrivaltime).HasColumnName("preferedarrivaltime");

                entity.Property(e => e.Prefereddeparturetime).HasColumnName("prefereddeparturetime");

                entity.Property(e => e.Routeid).HasColumnName("routeid");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Shippments)
                    .HasForeignKey(d => d.Carid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("shippment_carid_fkey");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.Shippments)
                    .HasForeignKey(d => d.Routeid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("shippment_routeid_fkey");
            });

            modelBuilder.Entity<Shippmentsdriver>(entity =>
            {
                entity.HasKey(e => new { e.Shippmentid, e.Driverid })
                    .HasName("shippmentsdrivers_pkey");

                entity.ToTable("shippmentsdrivers");

                entity.Property(e => e.Shippmentid).HasColumnName("shippmentid");

                entity.Property(e => e.Driverid).HasColumnName("driverid");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Shippmentsdrivers)
                    .HasForeignKey(d => d.Driverid)
                    .HasConstraintName("shippmentsdrivers_driverid_fkey");

                entity.HasOne(d => d.Shippment)
                    .WithMany(p => p.Shippmentsdrivers)
                    .HasForeignKey(d => d.Shippmentid)
                    .HasConstraintName("shippmentsdrivers_shippmentid_fkey");
            });

            modelBuilder.Entity<Useraccount>(entity =>
            {
                entity.ToTable("useraccount");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Employmentdate)
                    .HasColumnType("date")
                    .HasColumnName("employmentdate");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("firstname");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("lastname");

                entity.Property(e => e.Patronymic)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("patronymic");

                entity.Property(e => e.Position).HasColumnName("position");

                entity.Property(e => e.Profileimage).HasColumnName("profileimage");
            });

            modelBuilder.Entity<Userconfidential>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("userconfidential");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userconfidential_userid_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
