using AforismiChuckNorris.Data.Entities;
using AforismiChuckNorris.Data.Models;
using AutoMapper;

namespace AforismiChuckNorris.Data.MappingProfiles
{
    public class AphorismProfile : Profile
    {
        public AphorismProfile()
        {
            CreateMap<Aphorism, AphorismDto>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => MapChuckNorrisName(src)))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate.ToLocalTime().ToString()))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.UpdateDate.ToLocalTime().ToString()));
        }

        private string MapChuckNorrisName(Aphorism aphorism)
        {
            string subject = "Chuck Norris";

            if (!string.IsNullOrEmpty(aphorism.Subject))
                subject = aphorism.Subject;

            return string.Format(aphorism.Value, subject);
        }
    }
}
