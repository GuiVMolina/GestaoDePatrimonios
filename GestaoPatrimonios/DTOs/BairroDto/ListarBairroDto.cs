namespace GestaoPatrimonios.DTOs.BairroDto
{
    public class ListarBairroDto
    {
        public Guid BairroId { get; set; }

        public string NomeBairro { get; set; } = string.Empty;

        public Guid CidadeID { get; set; }
    }
}
