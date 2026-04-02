namespace GestaoPatrimonios.DTOs.PatrimonioDto
{
    public class CriarPatrimonioDto
    {
        public int NumeroSerie { get; set; }

        public string NumeroPatrimonio { get; set; } = string.Empty;

        public string? Imagem { get; set; }

        public string Denominacao { get; set; } = string.Empty;

        public decimal? Valor { get; set; }

        public bool? Ativo { get; set; }

        public Guid LocalID { get; set; }

        public Guid TipoPatrimonioID { get; set; }

        public Guid StatusPatrimonioID { get; set; }
    }
}
