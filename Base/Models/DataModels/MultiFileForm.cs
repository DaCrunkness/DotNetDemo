using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Models.DataModels
{
    public class MultiFileForm
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;


        [Required]
        public string TextField { get; set; } = string.Empty;

        [Required]
        public int NumberField { get; set; }

        [Required]
        public bool CheckboxField { get; set; }

        [Required]
        public DateTime DateField { get; set; }

        [Required]
        public string DropdownField { get; set; } = string.Empty;

        [Required]
        public string RadioField { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailField { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        public string PasswordField { get; set; } = string.Empty;

        [Required]
        public string TextAreaField { get; set; } = string.Empty;

        [ValidateNever]
        public List<MultiFile> MultiFiles{ get; set; } 
    }
}
