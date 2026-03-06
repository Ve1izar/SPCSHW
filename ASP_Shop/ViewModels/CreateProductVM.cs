using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ASP_Shop.ViewModels
{
    public class CreateProductVM
    {
        [Required(ErrorMessage = "Ім'я товару є обов'язковим")]
        [MaxLength(200, ErrorMessage = "Максимальна к-сть символів 200")]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "Ціна має бути більшою за 0")]
        public decimal Price { get; set; } = 0m;

        [Range(0, int.MaxValue, ErrorMessage = "Кількість не може бути від'ємною")]
        public int Amount { get; set; } = 0;

        public string? ImageUrl { get; set; }

        public IFormFile? Image { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public int? CategoryId { get; set; } = 0;
        public IEnumerable<SelectListItem> SelectItems { get; set; } = [];
    }
}
