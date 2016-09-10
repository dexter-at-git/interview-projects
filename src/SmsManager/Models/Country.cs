﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmsManager.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string MobileCode { get; set; }
        public decimal SmsPrice { get; set; }
    }
}
