using GestaoPatrimonios.Applications.Regras;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.DTOs.CidadeDto;
using GestaoPatrimonios.Exceptions;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Applications.Services
{
    public class CidadeService
    {
        private readonly ICidadeRepository _repository;

        public CidadeService(ICidadeRepository repository)
        {
            _repository = repository;
        }

        public List<ListarCidadeDto> Listar()
        {
            List<Cidade> cidades = _repository.Listar();

            List<ListarCidadeDto> cidadesDto = cidades.Select(cidade => new ListarCidadeDto
            {
                NomeCidade = cidade.NomeCidade

            }).ToList();

            return cidadesDto;
        }

        public ListarCidadeDto BuscarPorId(Guid cidadeId) {
            Cidade cidade = _repository.BuscarPorId(cidadeId);

            if (cidade == null) {
                throw new DomainException("Cidade não encontrada");
            }

            ListarCidadeDto cidadeDto = new ListarCidadeDto
            {
                CidadeID = cidade.CidadeID,
                NomeCidade = cidade.NomeCidade
            };

            return cidadeDto;
        }

        public void Adicionar(CriarCidadeDto dto)
        {
            Validar.ValidarNome(dto.NomeCidade);

            Cidade cidadeExistente = _repository.BuscarPorNomeEstado(dto.NomeCidade, dto.Estado);

            if (cidadeExistente == null)
            {
                throw new DomainException("Já existe um cidade cadastrado com esse nome nessa área");
            }

            Cidade cidadeizacao = new Cidade
            {
                NomeCidade = dto.NomeCidade,
                Estado = dto.Estado
            };

            _repository.Adicionar(cidadeizacao);
        }

        public void Atualizar(Guid cidadeId, CriarCidadeDto dto)
        {
            Validar.ValidarNome(dto.NomeCidade);

            Cidade cidadeizacaoBanco = _repository.BuscarPorId(cidadeId);
            if (cidadeizacaoBanco == null)
            {
                throw new DomainException("Localização não encontrada");
            }

            Cidade cidadeExistente = _repository.BuscarPorNomeEstado(dto.NomeCidade, dto.Estado);

            if (cidadeExistente == null)
            {
                throw new DomainException("Já existe um cidade cadastrado com esse nome nessa área");
            }

            cidadeizacaoBanco.NomeCidade = dto.NomeCidade;
            cidadeizacaoBanco.Estado = dto.Estado;

            _repository.Atualizar(cidadeizacaoBanco);
        }
    }
}
