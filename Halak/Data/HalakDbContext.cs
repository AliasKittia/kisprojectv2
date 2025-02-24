using Microsoft.EntityFrameworkCore;
using Halak.Models;
using System;

namespace Halak.Data
{
    public class HalakDbContext : DbContext
    {
        public HalakDbContext(DbContextOptions<HalakDbContext> options) : base(options) { }

        public required DbSet<HalakModel> Halak { get; set; }
        public required DbSet<FogasokModel> Fogasok { get; set; }
        public required DbSet<TavakModel> Tavak { get; set; }
        public required DbSet<HorgaszokModel> Horgaszok { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // HalakModel konfiguráció
            modelBuilder.Entity<HalakModel>()
                .HasKey(h => h.id);

            // FogasokModel konfiguráció
            modelBuilder.Entity<FogasokModel>()
                .HasKey(f => f.id);

            // TavakModel konfiguráció
            modelBuilder.Entity<TavakModel>()
                .HasKey(t => t.id);

            // HorgaszokModel konfiguráció
            modelBuilder.Entity<HorgaszokModel>()
                .HasKey(h => h.id);
        }
    }
}