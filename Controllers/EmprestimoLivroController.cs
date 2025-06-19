using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trab_Final.Services.DTOs;

namespace Trab_Final.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json", "application/xml")]
    [ProducesResponseType(typeof(AutorDTO), StatusCodes.Status200OK, "application/json")]
    [ProducesResponseType(typeof(AutorDTO), StatusCodes.Status200OK, "application/xml")]
    [ApiController]
    public class EmprestimoLivroController : ControllerBase
    {
        private readonly EmprestimoLivroService _emprestimoLivroService;
        private readonly IMapper _mapper;

        public EmprestimoLivroController(EmprestimoLivroService emprestimoLivroService, IMapper mapper)
        {
            _emprestimoLivroService = emprestimoLivroService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<EmprestimoLivroDTO>> GetAll()
        {
            var lista = _emprestimoLivroService.GetAll();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EmprestimoLivroDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<EmprestimoLivroDTO> GetById(int id)
        {
            var emprestimoLivro = _emprestimoLivroService.GetById(id);
            if (emprestimoLivro == null)
                return NotFound();
            return Ok(emprestimoLivro);
        }

        [HttpPost]
        [ProducesResponseType(typeof(EmprestimoLivroDTO), StatusCodes.Status201Created)]
        public ActionResult<EmprestimoLivroDTO> Create([FromBody] CriarEmprestimoLivroDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var emprestimoLivroCriado = _emprestimoLivroService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = emprestimoLivroCriado.IdEmprestimoLivro }, emprestimoLivroCriado);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var emprestimoLivro = _emprestimoLivroService.GetById(id);
            if (emprestimoLivro == null)
                return NotFound();
            _emprestimoLivroService.Delete(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(EmprestimoLivroDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<EmprestimoLivroDTO> Update(int id, [FromBody] AtualizarEmprestimoLivroDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var emprestimoLivro = _emprestimoLivroService.GetById(id);
            if (emprestimoLivro == null)
                return NotFound();
            var emprestimoLivroAtualizado = _emprestimoLivroService.AtualizarParcial(id, dto);
            return Ok(emprestimoLivroAtualizado);
        }
    }
}
