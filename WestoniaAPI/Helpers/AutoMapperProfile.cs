using AutoMapper;
using WestoniaAPI.DataLayer.DataModels;
using WestoniaAPI.DataLayer.Entities.Security;
using WestoniaAPI.DataLayer.Models;

namespace WestoniaAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Account
            // Account (Entity) -> DmAccount (DataModel)
            CreateMap<Account, DmAccount>()
                .ForMember(dm => dm.Language, opt => opt.MapFrom(a => a.Language))
                .ForMember(dm => dm.HasAcceptedGTCs, opt => opt.MapFrom(a => a.HasAcceptedGTCs)).ReverseMap();

            // DmAccount (DataModel) -> MdlAccount (Model)
            CreateMap<DmAccount, MdlAccount>()
                .ForMember(mdl => mdl.Language, opt => opt.MapFrom(dm => dm.Language))
                .ForMember(mdl => mdl.HasAcceptedGTCs, opt => opt.MapFrom(dm => dm.HasAcceptedGTCs)).ReverseMap();
            #endregion Account

            #region Minecraft User
            // MinecraftUser (Entity) -> DmMinecraftUser (DataModel)
            CreateMap<MinecraftUser, DmMinecraftUser>()
                .ForMember(dm => dm.MinecraftUuid, opt => opt.MapFrom(mu => mu.MinecraftUuid))
                .ForMember(dm => dm.FirstJoin, opt => opt.MapFrom(mu => mu.FirstJoin))
                .ForMember(dm => dm.LastJoin, opt => opt.MapFrom(mu => mu.LastJoin))
                .ForMember(dm => dm.Language, opt => opt.MapFrom(mu => mu.Language)).ReverseMap();

            // DmMinecraftUser (DataModel) -> MdlMinecraftUser (Model)
            CreateMap<DmMinecraftUser, MdlMinecraftUser>()
                .ForMember(mdl => mdl.MinecraftUuid, opt => opt.MapFrom(dm => dm.MinecraftUuid))
                .ForMember(mdl => mdl.FirstJoin, opt => opt.MapFrom(dm => dm.FirstJoin))
                .ForMember(mdl => mdl.LastJoin, opt => opt.MapFrom(dm => dm.LastJoin))
                .ForMember(mdl => mdl.Language, opt => opt.MapFrom(dm => dm.Language)).ReverseMap();
            #endregion
        }
    }
}