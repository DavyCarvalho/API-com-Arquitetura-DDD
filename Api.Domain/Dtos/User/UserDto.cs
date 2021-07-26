using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.User
{
    public class UserDto
    {
        [Required (ErrorMessage ="Nome é um campo obrigatório!")]
        [StringLength (60, ErrorMessage ="Nome deve ter no máximo {1} caracteres.")]
        public string Name { get; set; }

        [Required (ErrorMessage ="Email é um campo obrigatório!")]
        [EmailAddress (ErrorMessage ="Email em formato invalido.")]
        [StringLength (100, ErrorMessage ="Email deve ter no máximo {1} caracteres.")]
        public string Email { get; set; }
    }
}
