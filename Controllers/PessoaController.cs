
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Trab_Final.Services.DTOs;

namespace Trab_Final.Controllers
{

    [Route("api/[controller]")]
    [Produces("application/json", "application/xml")]
    [ProducesResponseType(typeof(AutorDTO), StatusCodes.Status200OK, "application/json")]
    [ProducesResponseType(typeof(AutorDTO), StatusCodes.Status200OK, "application/xml")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PessoaService _pessoaService;
        private readonly IMapper _mapper;
        public PersonController(PessoaService pessoaService, IMapper mapper)
        {
            _pessoaService = pessoaService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<PessoaDTO>>> GetAll()
        {
            var pessoasDTOs = await _pessoaService.GetAll();
            return Ok(pessoasDTOs);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PessoaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PessoaDTO>> GetById(int id)
        {
            var pessoa = await _pessoaService.GetById(id);

            if (pessoa == null)
                return NotFound();

            return Ok(pessoa);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PessoaDTO), StatusCodes.Status201Created)]
        public async Task<ActionResult<PessoaDTO>> Create([FromBody] CriarPessoaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pessoaCriada = await _pessoaService.Create(dto);

            return CreatedAtAction(nameof(GetById), new { id = pessoaCriada.IdPessoa }, pessoaCriada);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deletado = await _pessoaService.Delete(id);

            if (!deletado)
                return NotFound();

            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(PessoaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PessoaDTO>> AtualizarParcial(int id, [FromBody] AtualizarPessoaDTO dto)
        {
            var pessoaAtualizada = await _pessoaService.AtualizarParcial(id, dto);

            if (pessoaAtualizada == null)
                return NotFound();

            return Ok(pessoaAtualizada);
        }

    }
}

