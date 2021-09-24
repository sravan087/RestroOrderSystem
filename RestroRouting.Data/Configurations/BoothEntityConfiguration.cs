using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestroRouting.Domain.Entities;
using System;

namespace RestroRouting.Data.Configurations
{
    public class BoothEntityConfiguration : IEntityTypeConfiguration<Booth>
    {
        public void Configure(EntityTypeBuilder<Booth> builder)
        {
            builder.ToTable("Booth");


            builder.HasKey(b => b.BoothId);
            builder.Property<int>("BoothNumber").HasColumnName("BoothNumber");
            builder.Property<string>("Name").HasColumnName("Name");
            builder.Property<string>("Status").HasColumnName("Status");
            builder.Property<DateTime>("EffectiveStartDate").HasColumnName("EffectiveStartDate");
            builder.Property<DateTime?>("EffectiveEndDate").HasColumnName("EffectiveEndDate");
            builder.OwnsMany<Order>("Orders", x =>
            {
                x.WithOwner().HasForeignKey("BoothId");
                x.HasKey(b => b.OrderId);
                x.Property<decimal>("OrderCost").HasColumnName("OrderCost");
                x.Property<string>("Status").HasColumnName("Status");


                x.OwnsMany<OrderMenuItem>("OrderMenuItems", m =>
                {
                    m.WithOwner().HasForeignKey("OrderId");
                    m.HasKey(b => b.OrderMenuItemId);
                    m.Property<Guid>("MenuItemId").HasColumnName("MenuItemId");
                    m.Property<int>("Quantity").HasColumnName("Quantity");
                    m.Property<decimal>("ItemPrice").HasColumnName("ItemPrice");
                    m.Property<decimal>("Cost").HasColumnName("Cost");
                    m.Property<string>("ItemType").HasColumnName("ItemType");
                    m.Property<bool>("ReadyToServe").HasColumnName("ReadyToServe");
                });
            });
        }
    }
}
