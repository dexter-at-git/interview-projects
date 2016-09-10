using SmsManager.Models;

namespace SmsManager.Services
{
    public interface ISmsSender
    {
        SmsMessage SendSms(string to, string from, string message);
    }
}