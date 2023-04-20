using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    [Index(nameof(Email), IsUnique =true)]
    public class Users
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Username é obrigatório! Tente novamente.")]
        public string? Username { get; set; }
        [EmailAddress]
        [Required(ErrorMessage ="Email é obrigatório! Tente novamente.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Senha é obrigatória! Tente novamente.")]
        [MinLength(4, ErrorMessage ="Senha muito curta! Tente novamente.")]
        public string? Password { get; set; }
    }
}
