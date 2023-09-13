using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

using BanqueNET.Models;

namespace BanqueNET.Helpers
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Compte> Comptes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci").HasCharSet("utf8mb4");

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("client");

                entity.HasCharSet("utf8mb3").UseCollation("utf8_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Adresse).HasMaxLength(200).HasColumnName("adresse");
                entity.Property(e => e.Codepostal).HasMaxLength(30).HasColumnName("codepostal");
                entity.Property(e => e.Motdepasse).HasMaxLength(100).HasColumnName("motdepasse");
                entity.Property(e => e.Nom).HasMaxLength(100).HasColumnName("nom");
                entity.Property(e => e.Prenom).HasMaxLength(50).HasColumnName("prenom");
                entity.Property(e => e.Ville).HasMaxLength(200).HasColumnName("ville");
            });

            modelBuilder.Entity<Compte>(entity =>
            {
                entity.HasKey(e => e.Numero).HasName("PRIMARY");

                entity.ToTable("compte");

                entity.HasCharSet("utf8mb3").UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Idclient, "fk_client_idx");

                entity.Property(e => e.Numero).ValueGeneratedNever().HasColumnName("numero");
                entity.Property(e => e.Idclient).HasColumnName("idclient");
                entity.Property(e => e.Solde).HasPrecision(15, 2).HasColumnName("solde");
                entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Comptes).HasForeignKey(d => d.Idclient).HasConstraintName("fk_client");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
