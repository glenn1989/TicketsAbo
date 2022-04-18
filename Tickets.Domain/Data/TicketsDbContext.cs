using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Tickets.Domain.Entities;

namespace Tickets.Domain.Data
{
    public partial class TicketsDbContext : DbContext
    {
        public TicketsDbContext()
        {
        }

        public TicketsDbContext(DbContextOptions<TicketsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aankopen> Aankopens { get; set; } = null!;
        public virtual DbSet<Abonnement> Abonnements { get; set; } = null!;
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Club> Clubs { get; set; } = null!;
        public virtual DbSet<Competitie> Competities { get; set; } = null!;
        public virtual DbSet<Plaat> Plaats { get; set; } = null!;
        public virtual DbSet<Stadion> Stadions { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<Vak> Vaks { get; set; } = null!;
        public virtual DbSet<VakStadion> VakStadions { get; set; } = null!;
        public virtual DbSet<Wedstrijd> Wedstrijds { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQL19_VIVES; Database=Tickets; Trusted_Connection=True; MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aankopen>(entity =>
            {
                entity.HasKey(e => e.AankoopId);

                entity.ToTable("Aankopen");

                entity.Property(e => e.AankoopId).HasColumnName("AankoopID");

                entity.Property(e => e.Aankoopdatum).HasColumnType("date");

                entity.Property(e => e.ClientId)
                    .HasMaxLength(450)
                    .HasColumnName("ClientID");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Aankopens)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_Aankopen_AspNetUsers");
            });

            modelBuilder.Entity<Abonnement>(entity =>
            {
                entity.HasKey(e => e.Abonnementsnummer);

                entity.ToTable("Abonnement");

                entity.Property(e => e.AankoopId).HasColumnName("AankoopID");

                entity.Property(e => e.PlaatsId).HasColumnName("PlaatsID");

                entity.HasOne(d => d.Aankoop)
                    .WithMany(p => p.Abonnements)
                    .HasForeignKey(d => d.AankoopId)
                    .HasConstraintName("FK_Abonnement_Aankopen");

                entity.HasOne(d => d.Plaats)
                    .WithMany(p => p.Abonnements)
                    .HasForeignKey(d => d.PlaatsId)
                    .HasConstraintName("FK_Abonnement_Plaats");

                entity.HasOne(d => d.StamnummerNavigation)
                    .WithMany(p => p.Abonnements)
                    .HasForeignKey(d => d.Stamnummer)
                    .HasConstraintName("FK_Abonnement_Club");
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Club>(entity =>
            {
                entity.HasKey(e => e.Stamnummer);

                entity.ToTable("Club");

                entity.Property(e => e.Stamnummer).ValueGeneratedNever();

                entity.Property(e => e.Clubnaam)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Stadion)
                    .WithMany(p => p.Clubs)
                    .HasForeignKey(d => d.StadionId)
                    .HasConstraintName("FK_Club_Stadion");
            });

            modelBuilder.Entity<Competitie>(entity =>
            {
                entity.ToTable("Competitie");

                entity.Property(e => e.CompetitieId)
                    .ValueGeneratedNever()
                    .HasColumnName("CompetitieID");

                entity.Property(e => e.Begindatum).HasColumnType("date");

                entity.Property(e => e.Competitienaam)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Einddatum).HasColumnType("date");
            });

            modelBuilder.Entity<Plaat>(entity =>
            {
                entity.HasKey(e => e.PlaatsId);

                entity.Property(e => e.PlaatsId).HasColumnName("PlaatsID");

                entity.Property(e => e.StadionId).HasColumnName("StadionID");

                entity.Property(e => e.VakId).HasColumnName("VakID");

                entity.HasOne(d => d.VakStadion)
                    .WithMany(p => p.Plaats)
                    .HasForeignKey(d => new { d.VakId, d.StadionId })
                    .HasConstraintName("FK_Plaats_VakStadion");
            });

            modelBuilder.Entity<Stadion>(entity =>
            {
                entity.ToTable("Stadion");

                entity.Property(e => e.StadionId).ValueGeneratedNever();

                entity.Property(e => e.StadionNaam)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Ticket");

                entity.Property(e => e.TicketId).HasColumnName("TicketID");

                entity.Property(e => e.AankoopId).HasColumnName("AankoopID");

                entity.Property(e => e.PlaatsId).HasColumnName("PlaatsID");

                entity.Property(e => e.WedstrijdId).HasColumnName("WedstrijdID");

                entity.HasOne(d => d.Aankoop)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.AankoopId)
                    .HasConstraintName("FK_Ticket_Aankopen1");

                entity.HasOne(d => d.Plaats)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.PlaatsId)
                    .HasConstraintName("FK_Ticket_Plaats");

                entity.HasOne(d => d.Wedstrijd)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.WedstrijdId)
                    .HasConstraintName("FK_Ticket_Wedstrijd");
            });

            modelBuilder.Entity<Vak>(entity =>
            {
                entity.ToTable("Vak");

                entity.Property(e => e.VakId)
                    .ValueGeneratedNever()
                    .HasColumnName("VakID");

                entity.Property(e => e.VakNaam)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VakStadion>(entity =>
            {
                entity.HasKey(e => new { e.VakId, e.StadionId });

                entity.ToTable("VakStadion");

                entity.Property(e => e.VakId).HasColumnName("VakID");

                entity.Property(e => e.StadionId).HasColumnName("StadionID");

                entity.Property(e => e.Prijs).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Stadion)
                    .WithMany(p => p.VakStadions)
                    .HasForeignKey(d => d.StadionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VakStadion_Stadion");

                entity.HasOne(d => d.Vak)
                    .WithMany(p => p.VakStadions)
                    .HasForeignKey(d => d.VakId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VakStadion_Vak");
            });

            modelBuilder.Entity<Wedstrijd>(entity =>
            {
                entity.ToTable("Wedstrijd");

                entity.Property(e => e.WedstrijdId)
                    .ValueGeneratedNever()
                    .HasColumnName("WedstrijdID");

                entity.Property(e => e.CompetitieId).HasColumnName("CompetitieID");

                entity.Property(e => e.Datum).HasColumnType("date");

                entity.Property(e => e.ThuisploegId).HasColumnName("ThuisploegID");

                entity.Property(e => e.UitploegId).HasColumnName("UitploegID");

                entity.HasOne(d => d.Competitie)
                    .WithMany(p => p.Wedstrijds)
                    .HasForeignKey(d => d.CompetitieId)
                    .HasConstraintName("FK_Wedstrijd_Competitie");

                entity.HasOne(d => d.Thuisploeg)
                    .WithMany(p => p.WedstrijdThuisploegs)
                    .HasForeignKey(d => d.ThuisploegId)
                    .HasConstraintName("FK_Wedstrijd_Club");

                entity.HasOne(d => d.Uitploeg)
                    .WithMany(p => p.WedstrijdUitploegs)
                    .HasForeignKey(d => d.UitploegId)
                    .HasConstraintName("FK_Wedstrijd_Club1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
