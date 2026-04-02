using GestaoPatrimonios.Applications.Regras;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.DTOs.StatusPatrimonioDto;
using GestaoPatrimonios.Exceptions;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Applications.Services
{
    public class StatusPatrimonioService
    {
        private readonly IStatusPatrimonioRepository _repository;

        public StatusPatrimonioService(IStatusPatrimonioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarStatusPatrimonioDto> Listar()
        {
            List<StatusPatrimonio> statusPatrimonios = _repository.Listar();

            List<ListarStatusPatrimonioDto> statusDto = statusPatrimonios.Select(statusPatrimonio => new ListarStatusPatrimonioDto
            {
                StatusPatrimonioID = statusPatrimonio.StatusPatrimonioID,
                NomeStatus = statusPatrimonio.NomeStatus
            }).ToList();

            return statusDto;
        }

        public ListarStatusPatrimonioDto BuscarPorId(Guid statusPatrimonioId)
        {
            StatusPatrimonio statusPatrimonio = _repository.BuscarPorId(statusPatrimonioId);

            if (statusPatrimonio == null)
            {
                throw new DomainException("Status de patrimônio não encontrado");
            }

            ListarStatusPatrimonioDto statusDto = new ListarStatusPatrimonioDto
            {
                StatusPatrimonioID = statusPatrimonio.StatusPatrimonioID,
                NomeStatus = statusPatrimonio.NomeStatus
            };

            return statusDto;
        }

        public void Adicionar(CriarStatusPatrimonioDto dto)
        {
            Validar.ValidarNome(dto.NomeStatus);

            StatusPatrimonio statusPatrimonioExistente = _repository.BuscarPorNome(dto.NomeStatus);

            if (statusPatrimonioExistente == null)
            {
                throw new DomainException("Já existe um status de patrimônio com esse nome");
            }

            StatusPatrimonio statusPatrimonio = new StatusPatrimonio
            {
                NomeStatus = dto.NomeStatus
            };

            _repository.Adicionar(statusPatrimonio);
        }

        public void Atualizar(Guid statusPatrimonioId, CriarStatusPatrimonioDto dto)
        {
            Validar.ValidarNome(dto.NomeStatus);

            StatusPatrimonio statusPatrimonioBanco = _repository.BuscarPorId(statusPatrimonioId);

            if (statusPatrimonioBanco == null)
            {
                throw new DomainException("Status de patrimônio não encontrado");
            }

            StatusPatrimonio statusPatrimonioExistente = _repository.BuscarPorNome(dto.NomeStatus);

            if (statusPatrimonioExistente == null)
            {
                throw new DomainException("Já existe um status de patrimônio com esse nome");
            }

            statusPatrimonioBanco.NomeStatus = dto.NomeStatus;

            _repository.Atualizar(statusPatrimonioBanco);
        }
    }
}
