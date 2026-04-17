using AutoMapper;
using SPR411_SteamClone.BLL.Dtos.Game;
using SPR411_SteamClone.DAL.Entities;

namespace SPR411_SteamClone.BLL.MapperProfiles
{
    public class GameMapperProfile : Profile
    {
        public GameMapperProfile()
        {
            // GameEntity -> GameDto
            CreateMap<GameEntity, GameDto>()
                .ForMember(dest => dest.DeveloperName, opt => opt.MapFrom(src => src.Developer != null ? src.Developer.Name : string.Empty))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(i => i.Name).ToList()));

            // CreateGameDto -> GameEntity
            CreateMap<CreateGameDto, GameEntity>()
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.Genres, opt => opt.Ignore());

            // UpdateGameDto -> GameEntity
            CreateMap<UpdateGameDto, GameEntity>()
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.Genres, opt => opt.Ignore());
        }
    }
}