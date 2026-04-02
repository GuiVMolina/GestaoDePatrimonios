namespace GestaoPatrimonios.DTOs.EnderecoDto
{
    public class CriarEnderecoDto
    {
        public int Numero { get; set; }

        public string Complemento { get; set; } = string.Empty;

        public string CEP { get; set; } = string.Empty;

        public string Logradouro { get; set; } = string.Empty;

        public Guid BairroID { get; set; }
    }
}
