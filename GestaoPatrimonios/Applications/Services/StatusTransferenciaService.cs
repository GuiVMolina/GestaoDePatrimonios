using GestaoPatrimonios.Applications.Regras;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.DTOs.StatusTransferenciaDto;
using GestaoPatrimonios.Exceptions;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Applications.Services
{
    public class StatusTransferenciaService
    {
        private readonly IStatusTransferenciaRepository _repository;

        public StatusTransferenciaService(IStatusTransferenciaRepository repository)
        {
            _repository = repository;
        }

        public List<ListarStatusTransferenciaDto> Listar()
        {
            List<StatusTransferencia> statusTransferencias = _repository.Listar();

            List<ListarStatusTransferenciaDto> statusDto = statusTransferencias.Select(statusTransferencia => new ListarStatusTransferenciaDto
            {
                StatusTransferenciaID = statusTransferencia.StatusTransferenciaID,
                NomeStatus = statusTransferencia.NomeStatus
            }).ToList();

            return statusDto;
        }

        public ListarStatusTransferenciaDto BuscarPorId(Guid statusTransferenciaId)
        {
            StatusTransferencia statusTransferencia = _repository.BuscarPorId(statusTransferenciaId);

            if (statusTransferencia == null)
            {
                throw new DomainException("Status de transferência não encontrado");
            }

            ListarStatusTransferenciaDto statusDto = new ListarStatusTransferenciaDto
            {
                StatusTransferenciaID = statusTransferencia.StatusTransferenciaID,
                NomeStatus = statusTransferencia.NomeStatus
            };

            return statusDto;
        }

        public void Adicionar(CriarStatusTransferenciaDto dto)
        {
            Validar.ValidarNome(dto.NomeStatus);

            StatusTransferencia statusTransferenciaExistente = _repository.BuscarPorNome(dto.NomeStatus);

            if (statusTransferenciaExistente == null)
            {
                throw new DomainException("Já existe um status de transferência com esse nome");
            }

            StatusTransferencia statusTransferencia = new StatusTransferencia
            {
                NomeStatus = dto.NomeStatus
            };

            _repository.Adicionar(statusTransferencia);
        }

        public void Atualizar(Guid statusTransfenciaId, CriarStatusTransferenciaDto dto)
        {
            Validar.ValidarNome(dto.NomeStatus);

            StatusTransferencia statusTransferenciaBanco = _repository.BuscarPorId(statusTransfenciaId);

            if (statusTransferenciaBanco == null)
            {
                throw new DomainException("Status de transferência não encontrado");
            }

            StatusTransferencia statusTransferenciaExistente = _repository.BuscarPorNome(dto.NomeStatus);

            if (statusTransferenciaExistente == null)
            {
                throw new DomainException("Já existe um status de transferência com esse nome");
            }

            statusTransferenciaBanco.NomeStatus = dto.NomeStatus;

            _repository.Atualizar(statusTransferenciaBanco);
        }
    }
}
