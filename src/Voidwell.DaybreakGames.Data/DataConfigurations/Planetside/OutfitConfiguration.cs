﻿using Microsoft.EntityFrameworkCore;
using Voidwell.DaybreakGames.Data.Models.Planetside;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Voidwell.DaybreakGames.Data.DataConfigurations
{
    public class OutfitConfiguration : IEntityTypeConfiguration<Outfit>
    {
        public void Configure(EntityTypeBuilder<Outfit> builder)
        {
            builder.ToTable("Outfit");

            builder.HasKey(a => a.Id);

            builder.Ignore(a => a.LeaderCharacter);

            builder.HasOne(a => a.World)
                .WithMany()
                .HasForeignKey(a => a.WorldId);

            builder.HasOne(a => a.Faction)
                .WithMany()
                .HasForeignKey(a => a.FactionId);
        }
    }
}
