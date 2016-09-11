using System;
using SmsManager.Models;

namespace SmsManager.Data.Entities
{
    public class SmsMessageEntity
    {
        public int Id { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Message { get; set; }
        public DateTime DateSent { get; set; }
        public SmsStatus Status { get; set; }
        public int CountryId { get; set; }
        public CountryEntity Country { get; set; }
    }
}