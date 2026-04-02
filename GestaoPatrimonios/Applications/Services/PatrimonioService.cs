using GestaoPatrimonios.Applications.Regras;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.DTOs.PatrimonioDto;
using GestaoPatrimonios.Exceptions;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Applications.Services
{
    public class PatrimonioService
    {
        private readonly IPatrimonioRepository _repository;

        public PatrimonioService(IPatrimonioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarPatrimonioDto> Listar()
        {
            List<Patrimonio> patrimonios = _repository.Listar();

            List<ListarPatrimonioDto> patrimoniosDto = patrimonios.Select(patrimonio => new ListarPatrimonioDto
            {
                PatrimonioID = patrimonio.PatrimonioID,
                NumeroSerie = patrimonio.NumeroSerie,
                NumeroPatrimonio = patrimonio.NumeroPatrimonio,
                Imagem = patrimonio.Imagem,
                Denominacao = patrimonio.Denominacao,
                Valor = patrimonio.Valor,
                Ativo = patrimonio.Ativo,
                LocalID = patrimonio.LocalID,
                TipoPatrimonioID = patrimonio.TipoPatrimonioID,
                StatusPatrimonioID = patrimonio.StatusPatrimonioID
            }).ToList();

            return patrimoniosDto;
        }

        public ListarPatrimonioDto BuscarPorId(Guid patrimonioId)
        {
            Patrimonio patrimonio = _repository.BuscarPorId(patrimonioId);

            if (patrimonio == null)
            {
                throw new DomainException("Patrimônio não encontrado");
            }

            ListarPatrimonioDto patrimoniosDto = new ListarPatrimonioDto
            {
                PatrimonioID = patrimonio.PatrimonioID,
                NumeroSerie = patrimonio.NumeroSerie,
                NumeroPatrimonio = patrimonio.NumeroPatrimonio,
                Imagem = patrimonio.Imagem,
                Denominacao = patrimonio.Denominacao,
                Valor = patrimonio.Valor,
                Ativo = patrimonio.Ativo,
                LocalID = patrimonio.LocalID,
                TipoPatrimonioID = patrimonio.TipoPatrimonioID,
                StatusPatrimonioID = patrimonio.StatusPatrimonioID
            };

            return patrimoniosDto;
        }

        public void Adicionar(CriarPatrimonioDto dto)
        {
            Validar.ValidarNumero(dto.NumeroPatrimonio);

            Patrimonio patrimonioExiste = _repository.BuscarPorNumeroPatrimonio(dto.NumeroPatrimonio);

            if (patrimonioExiste == null)
            {
                throw new DomainException("Já existe um patrimônio cadastrado com esse número");
            }

            if (!_repository.LocalizacaoExiste(dto.LocalID))
            {
                throw new DomainException("Local informado não existe");
            }

            if (!_repository.TipoPatrimonioExiste(dto.TipoPatrimonioID))
            {
                throw new DomainException("Tipo de patrimônio informado não existe");
            }

            if (!_repository.StatusPatrimonioExiste(dto.StatusPatrimonioID))
            {
                throw new DomainException("Status de patrimônio informado não existe");
            }

            Patrimonio patrimonio = new Patrimonio
            {
                NumeroSerie = dto.NumeroSerie,
                NumeroPatrimonio = dto.NumeroPatrimonio,
                Imagem = dto.Imagem,
                Denominacao = dto.Denominacao,
                Valor = dto.Valor,
                Ativo = dto.Ativo,
                LocalID = dto.LocalID,
                TipoPatrimonioID = dto.TipoPatrimonioID,
                StatusPatrimonioID = dto.StatusPatrimonioID
            };

            _repository.Adicionar(patrimonio);
        }

        public void Atualizar(Guid patrimonioId, CriarPatrimonioDto dto)
        {
            Validar.ValidarNumero(dto.NumeroPatrimonio);

            Patrimonio patrimonioBanco = _repository.BuscarPorId(patrimonioId);

            if (patrimonioBanco == null)
            {
                throw new DomainException("Patrimônio não encontrado");
            }

            Patrimonio patrimonioExiste = _repository.BuscarPorNumeroPatrimonio(dto.NumeroPatrimonio);

            if (patrimonioExiste == null)
            {
                throw new DomainException("Já existe um patrimônio cadastrado com esse número");
            }

            if (!_repository.LocalizacaoExiste(dto.LocalID))
            {
                throw new DomainException("Local informado não existe");
            }

            if (!_repository.TipoPatrimonioExiste(dto.TipoPatrimonioID))
            {
                throw new DomainException("Tipo de patrimônio informado não existe");
            }

            patrimonioBanco.NumeroSerie = dto.NumeroSerie;
            patrimonioBanco.NumeroPatrimonio = dto.NumeroPatrimonio;
            patrimonioBanco.Imagem = dto.Imagem;
            patrimonioBanco.Denominacao = dto.Denominacao;
            patrimonioBanco.Valor = dto.Valor;
            patrimonioBanco.LocalID = dto.LocalID;
            patrimonioBanco.TipoPatrimonioID = dto.TipoPatrimonioID;
            patrimonioBanco.StatusPatrimonioID = dto.StatusPatrimonioID;

            _repository.Atualizar(patrimonioBanco);
        }

        public void AtualizarStatus(Guid patrimonioId, CriarPatrimonioDto dto)
        {
            Validar.ValidarNumero(dto.NumeroPatrimonio);

            Patrimonio patrimonioBanco = _repository.BuscarPorId(patrimonioId);

            if (patrimonioBanco == null)
            {
                throw new DomainException("Patrimônio não encontrado");
            }

            Patrimonio patrimonioExiste = _repository.BuscarPorNumeroPatrimonio(dto.NumeroPatrimonio);

            if (patrimonioExiste == null)
            {
                throw new DomainException("Já existe um patrimônio cadastrado com esse número");
            }

            if (!_repository.StatusPatrimonioExiste(dto.StatusPatrimonioID))
            {
                throw new DomainException("Status de patrimônio informado não existe");
            }

            patrimonioBanco.Ativo = dto.Ativo;

            _repository.Atualizar(patrimonioBanco);
        }
    }
}
