using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ASP_Shop.ViewModels
{
    public class EditProductVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ім'я товару є обов'язковим")]
        [MaxLength(200, ErrorMessage = "Максимальна к-сть символів 200")]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "Ціна має бути більшою за 0")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Кількість не може бути від'ємною")]
        public int Amount { get; set; }

        public int? CategoryId { get; set; }
        public string? ImageUrl { get; set; }
        public string? ExistingImage { get; set; }
        public IFormFile? NewImage { get; set; }
        public IEnumerable<SelectListItem> SelectItems { get; set; } = [];
    }
}
