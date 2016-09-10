using System.Collections.Generic;

namespace SmsManager.Data.Entities
{
    public class CountryEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string MobileCode { get; set; }
        public decimal SmsPrice { get; set; }
        public ICollection<SmsMessageEntity> SmsMessages { get; set; } 
    }
}