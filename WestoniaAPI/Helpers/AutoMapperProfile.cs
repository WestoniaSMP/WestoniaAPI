using AutoMapper;
using WestoniaAPI.DataLayer.DataModels.Security;
using WestoniaAPI.DataLayer.Entities.Security;
using WestoniaAPI.DataLayer.Models.Security;

namespace WestoniaAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Westonia User
            // WestoniaUser (Entity) -> DmWestoniaUser (DataModel)
            CreateMap<WestoniaUser, DmWestoniaUser>()
                .ForMember(dm => dm.MinecraftUuid, opt => opt.MapFrom(mu => mu.MinecraftUuid))
                .ForMember(dm => dm.FirstJoin, opt => opt.MapFrom(mu => mu.FirstJoin))
                .ForMember(dm => dm.LastJoin, opt => opt.MapFrom(mu => mu.LastJoin))
                .ForMember(dm => dm.Language, opt => opt.MapFrom(mu => mu.Language)).ReverseMap();

            // DmWestoniaUser (DataModel) -> MdlWestoniaUser (Model)
            CreateMap<DmWestoniaUser, MdlWestoniaUser>()
                .ForMember(mdl => mdl.MinecraftUuid, opt => opt.MapFrom(dm => dm.MinecraftUuid))
                .ForMember(mdl => mdl.FirstJoin, opt => opt.MapFrom(dm => dm.FirstJoin))
                .ForMember(mdl => mdl.LastJoin, opt => opt.MapFrom(dm => dm.LastJoin))
                .ForMember(mdl => mdl.Language, opt => opt.MapFrom(dm => dm.Language)).ReverseMap();
            #endregion
        }
    }
}