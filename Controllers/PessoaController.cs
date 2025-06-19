
using System.Collections.Generic;
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
        public ActionResult<List<PessoaDTO>> GetAll()
        {
            var lista = _pessoaService.GetAll();
            var pessoasDTOs = _mapper.Map<List<PessoaDTO>>(lista);
            return Ok(pessoasDTOs);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PessoaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PessoaDTO> GetById(int id)
        {
            var pessoa = _pessoaService.GetById(id);

            if (pessoa == null)
                return NotFound();

            return Ok(pessoa);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PessoaDTO), StatusCodes.Status201Created)]
        public ActionResult<PessoaDTO> Create([FromBody] CriarPessoaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pessoaCriada = _pessoaService.Create(dto);

            return CreatedAtAction(nameof(GetById), new { id = pessoaCriada.IdPessoa }, pessoaCriada);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var deletado = _pessoaService.Delete(id);

            if (!deletado)
                return NotFound();

            return NoContent(); // 204 - sucesso, sem conteúdo
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(PessoaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PessoaDTO> AtualizarParcial(int id, [FromBody] AtualizarPessoaDTO dto)
        {
            var pessoaAtualizada = _pessoaService.AtualizarParcial(id, dto);

            if (pessoaAtualizada == null)
                return NotFound();

            return Ok(pessoaAtualizada);
        }

    }


}

