using GestaoPatrimonios.Applications.Regras;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.DTOs.TipoAlteracaoDto;
using GestaoPatrimonios.Exceptions;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Applications.Services
{
    public class TipoAlteracaoService
    {
        private readonly ITipoAlteracaoRepository _repository;

        public TipoAlteracaoService(ITipoAlteracaoRepository repository)
        {
            _repository = repository;
        }

        public List<ListarTipoAlteracaoDto> Listar()
        {
            List<TipoAlteracao> tiposAlteracao = _repository.Listar();

            List<ListarTipoAlteracaoDto> tiposDto = tiposAlteracao.Select(tipoAlteracao => new ListarTipoAlteracaoDto{
                TipoAlteracaoID = tipoAlteracao.TipoAlteracaoID,
                NomeTipo = tipoAlteracao.NomeTipo,
            }).ToList();

            return tiposDto;
        }

        public ListarTipoAlteracaoDto BuscarPorId(Guid tipoAlteracaoId)
        {
            TipoAlteracao tipoAlteracao = _repository.BuscarPorId(tipoAlteracaoId);

            if (tipoAlteracao == null)
            {
                throw new DomainException("Tipo de alteração não encontrado");
            }

            ListarTipoAlteracaoDto tipoDto = new ListarTipoAlteracaoDto
            {
                TipoAlteracaoID = tipoAlteracao.TipoAlteracaoID,
                NomeTipo = tipoAlteracao.NomeTipo
            };

            return tipoDto;
        }

        public void Adicionar(CriarTipoAlteracaoDto dto)
        {
            Validar.ValidarNome(dto.NomeTipo);

            TipoAlteracao tipoAlteracaoExistente = _repository.BuscarPorNome(dto.NomeTipo);

            if (tipoAlteracaoExistente == null)
            {
                throw new DomainException("Já existe um tipo de alteração com esse nome");
            }

            TipoAlteracao tipoAlteracao = new TipoAlteracao
            {
                NomeTipo = dto.NomeTipo
            };

            _repository.Adicionar(tipoAlteracao);
        }

        public void Atualizar(Guid tipoAlteracaoId, CriarTipoAlteracaoDto dto)
        {
            Validar.ValidarNome(dto.NomeTipo);

            TipoAlteracao tipoAlteracaoBanco = _repository.BuscarPorId(tipoAlteracaoId);

            if (tipoAlteracaoBanco == null)
            {
                throw new DomainException("Tipo de alteração não encontrado");
            }

            TipoAlteracao tipoAlteracaoExistente = _repository.BuscarPorNome(dto.NomeTipo);

            if (tipoAlteracaoExistente == null)
            {
                throw new DomainException("Já existe um tipo de alteração com esse nome");
            }

            tipoAlteracaoBanco.NomeTipo = dto.NomeTipo;

            _repository.Atualizar(tipoAlteracaoBanco);
        }
    }
}
