using CsvHelper.Configuration;
using GestaoPatrimonios.DTOs.PatrimonioDto;

namespace GestaoPatrimonios.Applications.Mapeamentos
{
    // É tipo um "tradutor de colunas", define como ler o CSV
    public class ImportarPatrimonioCsvMap : ClassMap<ImportarPatrimonioCsvDto>
    {
        // Definindo os mapeamentos
        public ImportarPatrimonioCsvMap()
        {
            // Map - Escolhe a propriedade da DTO
            // Name - Diz qual o nome da coluna CSV para essa propriedade
            Map(m => m.NumeroPatrimonio).Name("Nº invent.");
            Map(m => m.Denominacao).Name("Denominação do imobilizado");
            Map(m => m.DataIncorporacao).Name("Dt. incorp.");
            Map(m => m.ValorAquisicao).Name("ValAquis.");
        }
    }
}
