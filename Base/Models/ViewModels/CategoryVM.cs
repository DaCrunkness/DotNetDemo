using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class CategoryVM
    {
        [Required]
        [MaxLength(40)]
        [Display(Name = "New Category")]
        public string? Name { get; set; }
    }
}