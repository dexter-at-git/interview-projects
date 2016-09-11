using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNetCore.Mvc;
using SmsManager.Data;
using SmsManager.Services;

namespace SmsManager.Controllers
{
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
            DateTime fromDateTime;
            if (!DateTime.TryParseExact(dateTimeFrom, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateTime))
            {
                return StatusCode(500, "Incorrect 'From' date");
            }

            DateTime toDateTime;
            if (!DateTime.TryParseExact(dateTimeTo, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out toDateTime))
            {
                return StatusCode(500, "Incorrect 'To' date");
            }

            try
            {
                var messages = _smsManagerService.GetSmsMessages(fromDateTime, toDateTime, 10, 10);

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
                        CountryName = x.Country.Name,
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



        [HttpGet]
        [Route("statistics.{extension}")]
        public IActionResult GetStatistics(string dateFrom, string dateTo, IEnumerable<int> mccList)
        {
            try
            {
                var messages = _smsManagerService.GetSmsMessages(DateTime.Now, DateTime.Now, 10, 10);

                var response = messages.GroupBy(x=>new {Day = x.DateSent.ToString("yyyy-MM-dd"), x.Country.MobileCode, x.Country.SmsPrice}).Select(x => new StatisticsResponse()
                {
                    Day = x.Key.Day,
                    Count = x.Count(),
                    TotalPrice = x.Sum(y=>y.Country.SmsPrice),
                    MobileCountryCode = x.Key.MobileCode,
                    Price = x.Key.SmsPrice
                });


                return this.Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

                throw;
            }

        }


    }
}
