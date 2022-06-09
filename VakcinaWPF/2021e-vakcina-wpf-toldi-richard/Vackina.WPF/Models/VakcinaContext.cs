using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Vakcina.WPF.Models
{
    public partial class VakcinaContext : DbContext
    {
        public VakcinaContext()
        {
        }

        public VakcinaContext(DbContextOptions<VakcinaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<oltas> Oltasok { get; set; }
        public virtual DbSet<orvos> Orvosok { get; set; }
        public virtual DbSet<vakcina> Vakcinak { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;user id=root;database=vp_vakcina", ServerVersion.Parse("10.4.21-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            modelBuilder.Entity<oltas>(entity =>
            {
                entity.HasKey(e => e.taj_szam)
                    .HasName("PRIMARY");

                entity.Property(e => e.taj_szam).ValueGeneratedNever();

                entity.HasOne(d => d.orvos)
                    .WithMany(p => p.oltas)
                    .HasForeignKey(d => d.orvos_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("oltas_ibfk_2");

                entity.HasOne(d => d.vakcina)
                    .WithMany(p => p.oltas)
                    .HasForeignKey(d => d.vakcina_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("oltas_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
