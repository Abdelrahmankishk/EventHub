using EventHub.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.Configurations
{
    internal class AttendeeConfiguration : IEntityTypeConfiguration<Attendee>
    {
        public void Configure(EntityTypeBuilder<Attendee> builder)
        {
            builder.ToTable("Attendees");

            builder.HasKey(a => a.AttendeeId);

            builder.Property(a => a.FullName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(a => a.EmailAddress)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.HasIndex(a => a.EmailAddress)
                   .IsUnique();

            builder.OwnsOne(e => e.HomeAddress, HA =>
            {
                HA.Property(a=>a.PostalCode)
                  .IsRequired()
                  .HasMaxLength(20);

                HA.Property(a=>a.Streat)
                  .IsRequired()
                  .HasMaxLength(100);

                HA.Property(a=>a.Country)
                  .IsRequired()
                  .HasMaxLength(50);

                HA.Property(a=>a.City)
                  .IsRequired()
                  .HasMaxLength(50);
            });
        }
    }
}
