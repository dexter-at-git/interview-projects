using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmsManager.Data.Entities;

namespace SmsManager.Data.Configurations
{
    public class SmsMessageConfiguration
    {
        public SmsMessageConfiguration(EntityTypeBuilder<SmsMessageEntity> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(c => c.To).HasMaxLength(50).IsRequired();
            entityBuilder.Property(c => c.From).HasMaxLength(50).IsRequired();
            entityBuilder.Property(c => c.Message).HasMaxLength(256).IsRequired();
            entityBuilder.Property(c => c.DateSent).IsRequired();
            entityBuilder.HasOne(x => x.Country).WithMany(x => x.SmsMessages).HasForeignKey(x => x.CountryId);
            entityBuilder.ToTable("SmsMessage");
        }
    }
}