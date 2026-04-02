using GestaoPatrimonios.Contexts;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Repositories
{
    public class PatrimonioRepository : IPatrimonioRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public PatrimonioRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<Patrimonio> Listar()
        {
            return _context.Patrimonio.OrderBy(patrimonio => patrimonio.NumeroPatrimonio).ToList();
        }

        public Patrimonio BuscarPorId(Guid patrimonioId)
        {
            return _context.Patrimonio.Find(patrimonioId);
        }

        public Patrimonio BuscarPorNumeroPatrimonio(string numeroPatrimonio, Guid? patrimonioId = null)
        {
            return _context.Patrimonio.FirstOrDefault(patrimonio => patrimonio.NumeroPatrimonio == numeroPatrimonio && patrimonio.PatrimonioID == patrimonioId);
        }

        public bool LocalizacaoExiste(Guid localizacaoId)
        {
            return _context.Patrimonio.Any(localizacao => localizacao.LocalID == localizacaoId);
        }

        public bool TipoPatrimonioExiste(Guid tipoPatrimonioId)
        {
            return _context.Patrimonio.Any(tipoPatrimonio => tipoPatrimonio.TipoPatrimonioID == tipoPatrimonioId);
        }

        public bool StatusPatrimonioExiste(Guid statusPatrimonioId)
        {
            return _context.Patrimonio.Any(statusPatrimonio => statusPatrimonio.StatusPatrimonioID == statusPatrimonioId);
        }

        public void Adicionar(Patrimonio patrimonio)
        {
            _context.Patrimonio.Add(patrimonio);
            _context.SaveChanges();
        }

        public void Atualizar(Patrimonio patrimonio)
        {
            if (patrimonio == null) { return; }

            Patrimonio patrimonioBanco = _context.Patrimonio.Find(patrimonio.PatrimonioID);

            if (patrimonioBanco == null) { return; }

            patrimonioBanco.NumeroSerie = patrimonio.NumeroSerie;
            patrimonioBanco.NumeroPatrimonio = patrimonio.NumeroPatrimonio;
            patrimonioBanco.Imagem = patrimonio.Imagem;
            patrimonioBanco.Denominacao = patrimonio.Denominacao;
            patrimonioBanco.Valor = patrimonio.Valor;
            patrimonioBanco.LocalID = patrimonio.LocalID;
            patrimonioBanco.TipoPatrimonioID = patrimonio.TipoPatrimonioID;
            patrimonioBanco.StatusPatrimonioID = patrimonio.StatusPatrimonioID;

            _context.SaveChanges();
        }

        public void AtualizarStatus(Patrimonio patrimonio)
        {
            if (patrimonio == null) { return; }

            Patrimonio patrimonioBanco = _context.Patrimonio.Find(patrimonio.PatrimonioID);

            if (patrimonioBanco == null) { return; }

            patrimonioBanco.Ativo = patrimonio.Ativo;

            _context.SaveChanges();
        }
    }
}
