using System;

namespace SmsManager.Models
{
    public class SmsMessage
    {
        public int Id { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Message { get; set; }
        public DateTime DateSent { get; set; }
        public SmsStatus Status { get; set; }
        public Country Country { get; set; }
    }
}