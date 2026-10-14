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
            #region OrganizerProfile Configuration
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

            #region Registration Configuration
            modelBuilder.Entity<Registration>(entity =>
            {
                entity.ToTable("Registrations");
                entity.HasKey(r => new { r.EventId, r.AttendeeId });

                entity.Property(r=>r.Note).HasMaxLength(300);
                entity.Property(r => r.RegistirationTime).HasDefaultValueSql("GETDATE()");
            });
                        
            #endregion
        }
        DbSet<Organizer> Organizers { get; set; }
        DbSet<Badge> Badges { get; set; }
    }
}
