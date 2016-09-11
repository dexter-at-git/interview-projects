using AutoMapper;
using SmsManager.Data.Entities;

namespace SmsManager.Models.Profiles
{
    public class SmsManagerProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<SmsMessageEntity, SmsMessage>();
            CreateMap<CountryEntity, Country>();


            CreateMap<SmsMessage, SmsMessageEntity>();
            CreateMap<Country, CountryEntity>();
        }
    }
}