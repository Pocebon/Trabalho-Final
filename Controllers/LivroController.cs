using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trab_Final.BaseDados.Models;
using Trab_Final.Services.DTOs;

namespace Trab_Final.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json", "application/xml")]
    [ProducesResponseType(typeof(AutorDTO), StatusCodes.Status200OK, "application/json")]
    [ProducesResponseType(typeof(AutorDTO), StatusCodes.Status200OK, "application/xml")]
    [ApiController]
    public class LivroController : ControllerBase
    {

        private readonly LivroService _livroService;
        private readonly IMapper _mapper;

        public LivroController(LivroService livroService, IMapper mapper)
        {
            _livroService = livroService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<LivroDTO>> GetAll()
        {
            var lista = _livroService.GetAll();
            var livrosDTOs = _mapper.Map<List<LivroDTO>>(lista);
            return Ok(livrosDTOs);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LivroDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<LivroDTO> GetById(int id)
        {
            var livro = _livroService.GetById(id);
            if (livro == null)
                return NotFound();
            return Ok(_mapper.Map<LivroDTO>(livro));
        }

        [HttpPost]
        [ProducesResponseType(typeof(LivroDTO), StatusCodes.Status201Created)]
        public ActionResult<LivroDTO> Create([FromBody] CriarLivroDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var livroCriado = _livroService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = livroCriado.IdLivro }, _mapper.Map<LivroDTO>(livroCriado));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Delete(int id)
        {
            var deleted = _livroService.Delete(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(LivroDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<LivroDTO> AtualizarParcial(int id, [FromBody] AtualizarLivroDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var livroAtualizado = _livroService.AtualizarParcial(id, dto);
                if (livroAtualizado == null)
                    return NotFound($"Livro com ID {id} não encontrado.");

                return Ok(livroAtualizado);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest("Dados enviados são invalidos");
            }
        }
    }
}
