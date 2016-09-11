using SmsManager.Models;

namespace SmsManager.Services.Interfaces
{
    public interface ISmsSender
    {
        SmsMessage SendSms(string to, string from, string message);
    }
}