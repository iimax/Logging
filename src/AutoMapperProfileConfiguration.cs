using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logging.Models;

namespace Logging
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        : this("MyProfile")
        {
        }
        protected AutoMapperProfileConfiguration(string profileName)
        : base(profileName)
        {
            // .ForMember(d => d.Time, opt => opt.MapFrom(src => src.TimeStamp.ToUniversalTime()))
            CreateMap<DeviceLogModel, LogModelToES>()
                .ForMember(d => d.LocalTime, opt => opt.MapFrom(src => src.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(d => d.Msg, opt => opt.MapFrom(src => src.RenderedMessage))
                .ForMember(d => d.Exception, opt => opt.MapFrom(src => src.ExceptionObject));
        }
    }
}
