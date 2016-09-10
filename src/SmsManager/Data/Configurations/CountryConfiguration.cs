using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmsManager.Data.Entities;

namespace SmsManager.Data.Configurations
{
    public class CountryConfiguration
    {
        public CountryConfiguration(EntityTypeBuilder<CountryEntity> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(c => c.Code).HasMaxLength(2).IsRequired();
            entityBuilder.Property(c => c.MobileCode).HasMaxLength(3).IsRequired();
            entityBuilder.Property(c => c.Name).IsRequired();
            entityBuilder.Property(c => c.SmsPrice).IsRequired();
            entityBuilder.ToTable("Country");
        }
    }

}
