using GestaoPatrimonios.Applications.Regras;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.DTOs.EnderecoDto;
using GestaoPatrimonios.Exceptions;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Applications.Services
{
    public class EnderecoService
    {
        private readonly IEnderecoRepository _repository;

        public EnderecoService(IEnderecoRepository repository)
        {
            _repository = repository;
        }

        public List<ListarEnderecoDto> Listar()
        {
            List<Endereco> enderecos = _repository.Listar();
            List<ListarEnderecoDto> enderecosDto = enderecos.Select(endereco => new ListarEnderecoDto
            {
                Numero = endereco.Numero,
                Complemento = endereco.Complemento,
                CEP = endereco.CEP,
                Logradouro = endereco.Logradouro,
                BairroID = endereco.BairroID
            }).ToList();

            return enderecosDto;
        }

        public ListarEnderecoDto BuscarPorId(Guid enderecoId)
        {
            Endereco endereco = _repository.BuscarPorId(enderecoId);

            if (endereco == null)
            {
                throw new DomainException("Endereço não encontrado");
            }

            ListarEnderecoDto enderecosDto = new ListarEnderecoDto
            {
                Numero = endereco.Numero,
                Complemento = endereco.Complemento,
                CEP = endereco.CEP,
                Logradouro = endereco.Logradouro,
                BairroID = endereco.BairroID
            };

            return enderecosDto;
        }

        public void Adicionar(CriarEnderecoDto dto)
        {
            Validar.ValidarNome(dto.Logradouro);

            Endereco enderecoExistente = _repository.BuscarPorLogradouroENumero(dto.Logradouro, dto.Numero, dto.BairroID);

            if (enderecoExistente == null)
            {
                throw new DomainException("Já existe um endereço cadastrado com esse nome nesse bairro");
            }

            if (!_repository.BairroExiste(dto.BairroID))
            {
                throw new DomainException("Bairro informado não existe");
            }

            Endereco endereco = new Endereco
            {   
                Numero = dto.Numero,
                Complemento = dto.Complemento,
                CEP = dto.CEP,
                Logradouro = dto.Logradouro,
                BairroID = dto.BairroID
            };

            _repository.Adicionar(endereco);
        }

        public void Atualizar(Guid enderecoId, CriarEnderecoDto dto)
        {
            Validar.ValidarNome(dto.Logradouro);

            Endereco enderecoBanco = _repository.BuscarPorId(enderecoId);
            if (enderecoBanco == null)
            {
                throw new DomainException("Endereço não encontrado");
            }

            Endereco enderecoExistente = _repository.BuscarPorLogradouroENumero(dto.Logradouro, dto.Numero, dto.BairroID);

            if (enderecoExistente == null)
            {
                throw new DomainException("Já existe um endereço cadastrado com esse nome nesse bairro");
            }

            if (!_repository.BairroExiste(dto.BairroID))
            {
                throw new DomainException("Bairro informado não existe");
            }

            enderecoBanco.Numero = dto.Numero;
            enderecoBanco.Complemento = dto.Complemento;
            enderecoBanco.CEP = dto.CEP;
            enderecoBanco.Logradouro = dto.Logradouro;
            enderecoBanco.BairroID = dto.BairroID;

            _repository.Atualizar(enderecoBanco);
        }
    }
}
