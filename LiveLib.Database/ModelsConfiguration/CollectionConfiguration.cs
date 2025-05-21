using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveLib.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiveLib.Database.ModelsConfiguration
{
    public class CollectionConfiguration : IEntityTypeConfiguration<Collection>
    {
        public void Configure(EntityTypeBuilder<Collection> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasMany(c => c.Users)
                .WithMany(u => u.Collections)
                .UsingEntity(j => j.ToTable("UserCollections"));

            builder.HasMany(c => c.Books)
                .WithMany(u => u.Collections)
                .UsingEntity(j => j.ToTable("BookCollections"));
        }
    }
}
