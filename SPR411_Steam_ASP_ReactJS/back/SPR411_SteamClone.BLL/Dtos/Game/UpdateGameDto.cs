using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SPR411_SteamClone.BLL.Dtos.Game
{
    public class UpdateGameDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int DeveloperId { get; set; }
        public List<string> Genres { get; set; } = [];
        
        // Зображення для оновлення (знадобляться для Блоку 3)
        public IFormFile? PreviewImage { get; set; }
        public List<IFormFile> Images { get; set; } = [];
    }
}