namespace GestaoPatrimonios.DTOs.UsuarioDto
{
    public class CriarUsuarioDto
    {
        public string NIF { get; set; } = string.Empty;

        public string CPF { get; set; } = string.Empty;

        public string CarteiraTrabalho { get; set; } = string.Empty;

        public string RG { get; set; } = string.Empty;

        public string NomeUsuario { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public Guid CargoID { get; set; }

        public Guid EnderecoID { get; set; }

        public Guid TipoUsuarioID { get; set; }
    }
}
