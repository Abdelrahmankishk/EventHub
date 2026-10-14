
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
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");

            builder.HasKey(e => e.EventId);

            builder.Property(e => e.Title)
               .IsRequired()
               .HasMaxLength(150);

            builder.Property(e => e.Description)
                           .IsRequired()
                           .HasMaxLength(1000);

            builder.Property(e => e.StartDate)
                           .IsRequired();

            builder.Property(e => e.EndDate)
                           .IsRequired(false);

            builder.Property(e => e.MaxAttendees)
                           .IsRequired();

            #region Shadow Properties
            builder.Property<DateTime>("CreationDate")
                .IsRequired();
            builder.Property<DateTime>("ModificationDate")
                .IsRequired();
            #endregion

            #region Sessions Self-Referencing Relationships
            builder.HasOne(e=>e.ParenEvent)
                   .WithMany(s=>s.Sessions)
                   .HasForeignKey(s => s.ParentEventId)
                   .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
