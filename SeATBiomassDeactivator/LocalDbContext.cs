using System;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace SeATBiomassDeactivator
{
    public class LocalDbContext : DbContext
    {
        private string ConnectionString { get; set; }

        public LocalDbContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(ConnectionString
                    , builder =>
                    {
                        builder.ServerVersion(new Version(10, 3, 13), ServerType.MariaDb);
                    });
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefreshTokens>(entity =>
            {
                entity.HasKey(key => key.character_id);
                entity.ToTable("refresh_tokens");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(key => key.id);
                entity.ToTable("users");

                entity.HasOne(one => one.Token).WithOne(one => one.User).HasForeignKey<RefreshTokens>(rt => rt.character_id);
            });
        }
    }
}