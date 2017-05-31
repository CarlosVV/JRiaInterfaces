using AutoMapper;
using CES.CoreApi.Shared.Persistence.Model;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CES.CoreApi.Shared.Persistence.Data
{
    public class PersistenceMapperProfile : Profile
    {
        public override string ProfileName { get { return "RepositoryMapperProfile"; } }

        public PersistenceMapperProfile()
        {
            CreateMap<IDataReader, PersistenceModel>()
                    .ForMember(dest => dest.PersistenceID, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<int>("fPersistenceID")))
                    .ForMember(dest => dest.Disabled, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<bool>("fDisabled")))
                    .ForMember(dest => dest.Delete, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<bool>("fDelete")))
                    .ForMember(dest => dest.Changed, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<bool>("fChanged")))
                    .ForMember(dest => dest.Time, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<DateTime>("fTime")))
                    .ForMember(dest => dest.Modified, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<DateTime>("fModified")))
                    .ForMember(dest => dest.ModifiedID, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<int>("fModifiedID")));

            CreateMap<IDataReader, PersistenceEventModel>()
                    .ForMember(dest => dest.PersistenceEventID, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<int>("fPersistenceEventID")))
                    .ForMember(dest => dest.PersistenceID, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<int>("fPersistenceID")))
                    .ForMember(dest => dest.AppID, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<int>("fAppID")))
                    .ForMember(dest => dest.ProviderID, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<int>("fProviderID")))
                    .ForMember(dest => dest.PersistenceEventTypeID, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<int>("fPersistenceEventTypeID")))
                    .ForMember(dest => dest.RequesterInfoID, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<int>("fRequesterInfoID")))
                    .ForMember(dest => dest.JsonContent, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<dynamic>("fContent")))
                    .ForMember(dest => dest.Disabled, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<bool>("fDisabled")))
                    .ForMember(dest => dest.Delete, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<bool>("fDelete")))
                    .ForMember(dest => dest.Changed, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<bool>("fChanged")))
                    .ForMember(dest => dest.Time, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<DateTime>("fTime")))
                    .ForMember(dest => dest.Modified, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<DateTime>("fModified")))
                    .ForMember(dest => dest.ModifiedID, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<int>("fModifiedID")));

            CreateMap<IDataReader, RequesterInfoModel>()
                    .ForMember(dest => dest.AppID, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<int>("fAppID")))
                    .ForMember(dest => dest.RequesterInfoID, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<int>("fRequesterInfoID")))
                    .ForMember(dest => dest.AppObjectID, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<int>("fAppObjectID")))
                    .ForMember(dest => dest.AgentID, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<int>("fAgentID")))
                    .ForMember(dest => dest.AgentLocID, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<int>("fLocAgentID")))
                    .ForMember(dest => dest.UserID, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<int>("fUserID")))
                    .ForMember(dest => dest.TerminalID, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<string>("fTerminalID")))
                    .ForMember(dest => dest.TerminalUserID, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<string>("fTerminalUserID")))
                    .ForMember(dest => dest.LocalTime, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<DateTime>("fLocalTime")))
                    .ForMember(dest => dest.UtcTime, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<DateTime>("fUtcTime")))
                    .ForMember(dest => dest.Timezone, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<string>("fTimeZone")))
                    .ForMember(dest => dest.Locale, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<string>("fLocale")))
                    .ForMember(dest => dest.Version, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<string>("fVersion")))
                    .ForMember(dest => dest.Disabled, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<bool>("fDisabled")))
                    .ForMember(dest => dest.Delete, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<bool>("fDelete")))
                    .ForMember(dest => dest.Changed, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<bool>("fChanged")))
                    .ForMember(dest => dest.Time, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<DateTime>("fTime")))
                    .ForMember(dest => dest.Modified, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<DateTime>("fModified")))
                    .ForMember(dest => dest.ModifiedID, opts => opts.MapFrom(reader => ((SqlDataReader)reader).GetSafeValue<int>("fModifiedID")));

            
        }
    
    }
}

