using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmsManager.Models.Responses;
using SmsManager.Services.Interfaces;

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
        
        [HttpGet]
        [Route("countries.{extension}")]
        public IActionResult GetCountries(string extension)
        {
            if (extension == "xml")
            {
                HttpContext.Response.ContentType = "application/xml";
            }

            try
            {
                var countries = _smsManagerService.GetCountries();

                var response = countries.Select(x => new CountryResponse()
                {
                    Name = x.Name,
                    CountryCode = x.Code,
                    MobileCountryCode = x.MobileCode,
                    Price = x.SmsPrice
                });

                return this.Ok(response);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }
        }
   
        [HttpGet]
        [Route("send.{extension}")]
        public IActionResult SendSms(string from, string to, string text, string extension)
        {
            if (extension == "xml")
            {
                HttpContext.Response.ContentType = "application/xml";
            }

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
                return this.StatusCode(500, ex.Message);
            }
        }
        
        [HttpGet]
        [Route("sent.{extension}")]
        public IActionResult GetSentSMS(string dateTimeFrom, string dateTimeTo, int skip, int take, string extension)
        {
            if (extension == "xml")
            {
                HttpContext.Response.ContentType = "application/xml";
            }

            DateTime fromDateTime;
            if (!DateTime.TryParseExact(dateTimeFrom, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateTime))
            {
                return StatusCode(400, "Incorrect 'From' date");
            }

            DateTime toDateTime;
            if (!DateTime.TryParseExact(dateTimeTo, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out toDateTime))
            {
                return StatusCode(400, "Incorrect 'To' date");
            }

            try
            {
                var messages = _smsManagerService.GetSmsMessages(fromDateTime, toDateTime, 10, 10);

                var response = new SentSmsResponse()
                {
                    Count = messages.Count(),
                    SmsMessageList = messages.Select(x => new SentSmsMessageResponse()
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
                return this.StatusCode(500, ex.Message);
            }
        }
        
        [HttpGet]
        [Route("statistics.{extension}")]
        public IActionResult GetStatistics(string dateFrom, string dateTo, IEnumerable<string> mccList, string extension)
        {
            if (extension == "xml")
            {
                HttpContext.Response.ContentType = "application/xml";
            }

            DateTime fromDateTime;
            if (!DateTime.TryParseExact(dateFrom, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateTime))
            {
                return StatusCode(400, "Incorrect 'From' date");
            }

            DateTime toDateTime;
            if (!DateTime.TryParseExact(dateTo, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out toDateTime))
            {
                return StatusCode(400, "Incorrect 'To' date");
            }

            try
            {
                var messages = _smsManagerService.GetSmsMessages(fromDateTime, toDateTime, mccList);

                var response = messages.GroupBy(x => new { Day = x.DateSent.ToString("yyyy-MM-dd"), x.Country.MobileCode, x.Country.SmsPrice }).Select(x => new StatisticsResponse()
                {
                    Day = x.Key.Day,
                    Count = x.Count(),
                    TotalPrice = Math.Round(x.Sum(sum => sum.Country.SmsPrice), 2, MidpointRounding.AwayFromZero),
                    MobileCountryCode = x.Key.MobileCode,
                    Price = x.Key.SmsPrice
                });
                
                return this.Ok(response);

            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }
        }
    }
}