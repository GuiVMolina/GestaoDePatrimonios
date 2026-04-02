using System.ComponentModel.DataAnnotations;

namespace GestaoPatrimonios.DTOs.AreaDTO
{
    public class CriarAreaDto
    {
        // string.Empty = null proibido
        // string? = pode ser null
        // null! = "relaxa, confia"

        [Required(ErrorMessage =  "O nome da área é obrigatório")]
        [StringLength(50, ErrorMessage = "O nome da área deve ter no máximo 50 caracteres")]
        public string NomeArea { get; set; } = string.Empty;
    }
}
