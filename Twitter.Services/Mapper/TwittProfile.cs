using AutoMapper;
using Twitter.DataAccess.Entities;
using Twitter.Service.Models.Twitter;
using Twitter.DataAccess;

namespace Twitter.Service.Mapper
{
    public class TwittProfile : Profile
    {
        public TwittProfile()
        {
            CreateMap<TwittInfo, Twitt>()
                .ForMember(dest => dest.TwitterId, opt => opt.MapFrom(src => src.Data.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Data.Text))
                .ForMember(dest => dest.HashTags, opt => opt.MapFrom(src => src.Data.Entities.HashTags));
               

            CreateMap<Models.Twitter.HashTag, DataAccess.Entities.HashTag>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => src.Tag))
                .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.Start))
                .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.End));

        }
    }
}