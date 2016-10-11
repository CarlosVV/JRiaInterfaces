using AutoMapper;
using CES.CoreApi.Compliance.Screening.Models;
using CES.CoreApi.Compliance.Screening.ViewModels;
using eh.actimize.com;
using System.Collections.Generic;
using System.Linq;

namespace CES.CoreApi.Compliance.Screening
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<MSRealTimeWSProvider_1_Hits_InnerSet_TupleType, Models.Hit>()
                .ForMember(
                        dest => dest.categories,
                        opt => opt.MapFrom(src => src.categories.categories_InnerSet))
                .ForMember(d => d.categoriesNames,
                             opt => opt.MapFrom(s => s.categories.categories_InnerSet.Select(cat => cat.category)));

            Mapper.CreateMap<ScreeningRequest, Request>();
            Mapper.CreateMap<Response, ScreeningResponse>()
               .ForMember(dest => dest.Code, opts => opts.MapFrom(src => src.Code.ToString()))
               .ForMember(dest => dest.HoldFlag, opts => opts.MapFrom(src => src.LegalHold))
               .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.StatusActimize))
               .ForMember(dest => dest.ActionTaken, opts => opts.MapFrom(src => src.RuleWasApply.fActionID));
        }
    }
}

     
