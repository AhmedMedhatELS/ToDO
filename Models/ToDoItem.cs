using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ToDO.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime Deadline { get; set; }

        [ValidateNever]
        public int UserId { get; set; }

        [ValidateNever]
        public User User { get; set; }
    }
}
