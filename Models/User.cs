using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDO.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z][a-z]{2,}$")]
        public string Name { get; set; } = null!;

        [Required]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9]{2,}$")]
        public string UserName { get; set; } = null!;
        [Required]
        [RegularExpression(@"^[a-z][a-z0-9]+@(gmail|yahoo)\.(com)$")]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "The password must be at least 6 characters long.")]
        public string Password { get; set; } = null!;

        [NotMapped]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        [ValidateNever]
        public ICollection<ToDoItem> ToDoItems { get; set; } = new List<ToDoItem>();
    }
}
