using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmsManager.Data;
using SmsManager.Models;
using SmsManager.Services;

namespace SmsManager.Controllers
{


    public class SendSmsResponse
    {
        [JsonProperty(PropertyName = "state")]
        public SmsStatus SmsStatus { get; set; }
    }

    public class SentSmsResponse
    {
        [JsonProperty(PropertyName = "totalCount")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "items")]
        public IEnumerable<SentSmsMessageResponse> SmsMessageList { get; set; }
    }
    
    public class SentSmsMessageResponse
    {
        [JsonProperty(PropertyName = "dateTime")]
        public DateTime DateSent { get; set; }

        [JsonProperty(PropertyName = "mcc")]
        public string MobileCountryCode { get; set; }

        [JsonProperty(PropertyName = "from")]
        public string From { get; set; }

        [JsonProperty(PropertyName = "to")]
        public string To { get; set; }

        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }

        [JsonProperty(PropertyName = "state")]
        public SmsStatus SmsStatus { get; set; }

    }



    [Route("api/[controller]")]
    public class SmsController : Controller
    {
        private readonly ISmsManagerService _smsManagerService;

        public SmsController(ISmsManagerService smsManagerService)
        {
            _smsManagerService = smsManagerService;
        }



        ///sms/send.json?from=The+Sender&to=%2B4917421293388&text=Hello+World       
        [HttpGet]
        [Route("send.{extension}")]
        public IActionResult SendSms(string from, string to, string text, string extension)
        {

         //   HttpContext.Response.ContentType = "application/json";

            try
            {
                var status = _smsManagerService.Send(to, from, text);

                var response = new SendSmsResponse()
                {
                    SmsStatus = status
                };


                return this.Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet]
        [Route("sent.{extension}")]
        public IActionResult GetSentSMS(string dateTimeFrom, string dateTimeTo, int skip, int take)
        {
            try
            {
                var messages = _smsManagerService.GetSmsMessages(DateTime.Now, DateTime.Now, 10, 10);

                var response = new SentSmsResponse()
                {
                    Count = messages.Count(),
                    SmsMessageList = messages.Select(x=>new SentSmsMessageResponse()
                    {
                        SmsStatus = x.Status,
                        To = x.To,
                        From = x.From,
                        DateSent = x.DateSent,
                        MobileCountryCode = x.Country.MobileCode,
                        Price = x.Country.SmsPrice
                    })
                };

                return this.Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        
        
    }
}
