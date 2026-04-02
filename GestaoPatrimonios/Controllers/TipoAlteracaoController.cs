using GestaoPatrimonios.Applications.Services;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.DTOs.TipoAlteracaoDto;
using GestaoPatrimonios.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPatrimonios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoAlteracaoController : ControllerBase
    {
        private readonly TipoAlteracaoService _service;

        public TipoAlteracaoController(TipoAlteracaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarTipoAlteracaoDto>> Listar()
        {
            List<ListarTipoAlteracaoDto> tipoAlteracoes = _service.Listar();

            return Ok(tipoAlteracoes);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarTipoAlteracaoDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarTipoAlteracaoDto tipoAlteracao = _service.BuscarPorId(id);

                return Ok(tipoAlteracao);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarTipoAlteracaoDto dto)
        {
            try
            {
                _service.Adicionar(dto);

                return Created();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public ActionResult Atualizar(Guid id, CriarTipoAlteracaoDto dto)
        {
            try
            {
                _service.Atualizar(id, dto);

                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
