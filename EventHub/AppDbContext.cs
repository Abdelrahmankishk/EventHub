using EventHub.Configurations;
using EventHub.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub
{
    public class AppDbContext : DbContext 
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=EventHub;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region OrganizerProfile Configration
            modelBuilder.Entity<OrganizerProfile>(entity=>
            {
                entity.ToTable("OrganizerProfiles");
                entity.HasKey(op => op.ProfileId);
                entity.Property(p => p.Biography)
                                .IsRequired()
                                .HasMaxLength(500);
                entity.Property(p => p.WebsiteLink).HasMaxLength(250);
                entity.Property(p => p.LogoLink).HasMaxLength(250);
            });
            #endregion 

            modelBuilder.ApplyConfiguration(new AttendeeConfiguration());

            modelBuilder.ApplyConfiguration(new EventConfiguration());
        }
        DbSet<Organizer> Organizers { get; set; }
        DbSet<Badge> Badges { get; set; }
    }
}
