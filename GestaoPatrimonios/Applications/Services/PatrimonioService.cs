using CsvHelper;
using CsvHelper.Configuration;
using GestaoPatrimonios.Applications.Mapeamentos;
using GestaoPatrimonios.Applications.Regras;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.DTOs.PatrimonioDto;
using GestaoPatrimonios.Exceptions;
using GestaoPatrimonios.Repositories;
using System.Globalization;

namespace GestaoPatrimonios.Applications.Services
{
    public class PatrimonioService
    {
        private readonly PatrimonioRepository _repository;

        public PatrimonioService(PatrimonioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarPatrimonioDto> Listar()
        {
            List<Patrimonio> patrimonios = _repository.Listar();

            List<ListarPatrimonioDto> patrimonioDto = patrimonios.Select(patrimonio => new ListarPatrimonioDto
            {
                PatrimonioID = patrimonio.PatrimonioID,
                Denominacao = patrimonio.Denominacao,
                NumeroPatrimonio = patrimonio.NumeroPatrimonio,
                Valor = patrimonio.Valor,
                Imagem = patrimonio.Imagem,
                LocalizacaoID = patrimonio.LocalizacaoID,
                StatusPatrimonioID = patrimonio.StatusPatrimonioID
            }).ToList();

            return patrimonioDto;
        }

        public ListarPatrimonioDto BuscarPorId(Guid patrimonioId)
        {
            Patrimonio patrimonio = _repository.BuscarPorId(patrimonioId);

            if (patrimonio == null)
            {
                throw new DomainException("Patrimônio não encontrado");
            }

            ListarPatrimonioDto patrimonioDto = new ListarPatrimonioDto
            {
                PatrimonioID = patrimonio.PatrimonioID,
                Denominacao = patrimonio.Denominacao,
                NumeroPatrimonio = patrimonio.NumeroPatrimonio,
                Valor = patrimonio.Valor,
                Imagem = patrimonio.Imagem,
                LocalizacaoID = patrimonio.LocalizacaoID,
                StatusPatrimonioID = patrimonio.StatusPatrimonioID
            };

            return patrimonioDto;
        }

        public void Adicionar(IFormFile arquivoCsv, Guid usuarioId)
        {
            if (arquivoCsv == null || arquivoCsv.Length == 0)
            {
                throw new DomainException("Arquivo CSV é obrigatório.");
            }

            Localizacao localizacaoSemLocal = _repository.BuscarLocalizacaoPorNome("Sem local");

            if (localizacaoSemLocal == null)
            {
                throw new DomainException("Localização 'Sem local' não cadastrada.");
            }

            StatusPatrimonio statusAtivo = _repository.BuscarStatusPatrimonioPorNome("Ativo");

            if (statusAtivo == null)
            {
                throw new DomainException("Status 'Ativo' não cadastrado.");
            }

            TipoAlteracao tipoAlteracao = _repository.BuscarTipoAlteracaoPorNome("Atualização de dados");

            if (tipoAlteracao == null)
            {
                throw new DomainException("Tipo de alteração 'Atualização de dados' não cadastrado.");
            }

            List<ImportarPatrimonioCsvDto> registros;

            // Abre o arquivo enviado (IFormFile)
            using (var stream = arquivoCsv.OpenReadStream())

            // Lê o arquivo como texto
            using (var reader = new StreamReader(stream))

            // Cria leitor de CSV com configurações personalizadas
            // CultureInfo define como números, datas e textos são interpretados
            // InvariantCulture - Padrão universal
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Define que o separador é ponto e vírgula
                Delimiter = ";",

                // Ignora erros caso o cabeçalho não bata 100%
                // Não trava a aplicação por conta de formatação, vamos tratar os erros depois
                HeaderValidated = null,

                // Ignora o erro se faltar algum campo
                MissingFieldFound = null,

                // Ignora dados "quebrados" no CSV
                // Ex: Abriu aspas e não fechou
                BadDataFound = null,

                // Remove espaços extras automaticamente
                TrimOptions = TrimOptions.Trim
            }))
            {
                // Regista o mapa que criamos (CSV -> DTO)
                csv.Context.RegisterClassMap<ImportarPatrimonioCsvMap>();

                // Pega todas as linhas do CSV e converte para uma lista de DTO
                registros = csv.GetRecords<ImportarPatrimonioCsvDto>().ToList();
            }

            var erros = new List<string>();

            foreach (var item in registros)
            {
                // Se não tem número de patrimônio, ignora o registro
                if (string.IsNullOrWhiteSpace(item.NumeroPatrimonio))
                {
                    continue;
                }

                // Remove espaços extras do número
                string numeroPatrimonio = item.NumeroPatrimonio.Trim();

                if (string.IsNullOrWhiteSpace(item.Denominacao))
                {
                    erros.Add($"Patrimônio {numeroPatrimonio} sem denominação");
                    continue;
                }

                string denominacao = item.Denominacao.Trim();

                DateTime? dataIncorporacao = null;

                // Usa o formato brasileiro apenas para ler
                // Depois pega o DateTime e formata
                if (!string.IsNullOrWhiteSpace(item.DataIncorporacao))
                {
                    if (DateTime.TryParse(item.DataIncorporacao, new CultureInfo("pt-BR"), DateTimeStyles.None, out DateTime dataConvertida))
                    {
                        dataIncorporacao = dataConvertida;
                    }

                    decimal? valorAquisicao = null;

                    // TryParse - Converte string para decimal
                    // NumberStyles.Any - Define quais formatos de números são permitidos
                    // Out decimal - Se der certo, cria a variável com o valor já convertido
                    if (!string.IsNullOrWhiteSpace(item.ValorAquisicao))
                    {
                        // Remove separador de mlhar e ajusta decimal
                        string valorTexto = item.ValorAquisicao.Replace(".", "").Replace(",", ".");

                        if (decimal.TryParse(valorTexto, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valorConvertido))
                        {
                            valorAquisicao = valorConvertido;
                        }

                        Validar.ValidarNumeroPatrimonio(numeroPatrimonio);
                        Validar.ValidarNome(denominacao);

                        bool patrimonioExistente = _repository.BuscarPorNumeroPatrimonio(numeroPatrimonio);

                        if (patrimonioExistente == true)
                        {
                            continue;
                        }

                        Patrimonio patrimonio = new Patrimonio
                        {
                            Denominacao = denominacao,
                            NumeroPatrimonio = numeroPatrimonio,
                            Valor = valorAquisicao,
                            Imagem = null,
                            LocalizacaoID = localizacaoSemLocal.LocalizacaoID,
                            StatusPatrimonioID = statusAtivo.StatusPatrimonioID
                        };

                        _repository.Adicionar(patrimonio);

                        LogPatrimonio log = new LogPatrimonio
                        {
                            DataTransferencia = dataIncorporacao ?? DateTime.Now,
                            TipoAlteracaoID = tipoAlteracao.TipoAlteracaoID,
                            StatusPatrimonioID = patrimonio.StatusPatrimonioID,
                            PatrimonioID = patrimonio.PatrimonioID,
                            UsuarioID = usuarioId,
                            LocalizacaoID = patrimonio.LocalizacaoID
                        };

                        _repository.AdicionarLog(log);
                    }
                }
            }
        }

        public void AtualizarStatus(Guid patrimonioId, AtualizarStatusPatrimonioDto dto)
        {
            Patrimonio patrimonioBanco = _repository.BuscarPorId(patrimonioId);

            if (patrimonioBanco == null)
            {
                throw new DomainException("Patrimônio não encontrado.");
            }

            if (!_repository.StatusPatrimonioExiste(dto.StatusPatrimonioID))
            {
                throw new DomainException("Status de patrimônio informado não existe.");
            }

            patrimonioBanco.StatusPatrimonioID = dto.StatusPatrimonioID;

            _repository.AtualizarStatus(patrimonioBanco);
        }
    }
}
