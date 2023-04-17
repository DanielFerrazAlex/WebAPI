using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Username é obrigatório! Tente novamente.")]
        public string? Username { get; set; }
        [Required(ErrorMessage ="Email é obrigatório! Tente novamente.")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage ="Digite um Email válido")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Senha é obrigatória! Tente novamente.")]
        public string? Password { get; set; }
    }
}
