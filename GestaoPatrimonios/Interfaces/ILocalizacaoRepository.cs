using GestaoPatrimonios.Domains;

namespace GestaoPatrimonios.Interfaces
{
    public interface ILocalizacaoRepository
    {
        List<Localizacao> Listar();

        Localizacao BuscarPorId(Guid localizacaoId);

        Localizacao BuscarPorNome(string nomeLocal, Guid areaId);

        bool AreaExiste(Guid areaId);

        void Adicionar(Localizacao localizacao);

        void Atualizar(Localizacao localizacao);

    }
}
