using SPR411_SteamClone.BLL.Dtos.Genre;

namespace SPR411_SteamClone.BLL.Dtos.Game
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        
        public int DeveloperId { get; set; }
        public string DeveloperName { get; set; } = string.Empty;
        
        public List<GenreDto> Genres { get; set; } = [];
        public List<string> Images { get; set; } = [];
    }
}