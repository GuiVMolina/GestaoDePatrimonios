using GestaoPatrimonios.Applications.Services;
using GestaoPatrimonios.DTOs.PatrimonioDto;
using GestaoPatrimonios.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPatrimonios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPatrimonioController : ControllerBase
    {
        private readonly TipoPatrimonioService _service;

        public TipoPatrimonioController(TipoPatrimonioService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarTipoPatrimonioDto>> Listar()
        {
            List<ListarTipoPatrimonioDto> tipoPatrimonios = _service.Listar();

            return Ok(tipoPatrimonios);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarTipoPatrimonioDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarTipoPatrimonioDto tipoPatrimonio = _service.BuscarPorId(id);

                return Ok(tipoPatrimonio);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarTipoPatrimonioDto dto)
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
        public ActionResult Atualizar(Guid id, CriarTipoPatrimonioDto dto)
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
