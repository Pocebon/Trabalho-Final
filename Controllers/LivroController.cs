using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<ActionResult<List<LivroDTO>>> GetAll()
        {
            var livrosDTOs = await _livroService.GetAll();
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

            if (dto.disponivel != "S" && dto.disponivel != "N")
                return BadRequest("'Disponivel' deve ser apenas 'S' ou 'N'.");


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

            if (dto.disponivel != null && dto.disponivel != "S" && dto.disponivel != "N")
                return BadRequest("'Disponivel' deve ser apenas 'S' ou 'N'.");

            try
            {
                var livroAtualizado = _livroService.AtualizarParcial(id, dto);
                if (livroAtualizado == null)
                    return NotFound("Livro com esse ID não encontrado.");

                return Ok(livroAtualizado);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest("Dados são invalidos");
            }
        }

        [HttpGet("livos disponiveis")]
        [ProducesResponseType(typeof(List<LivroDTO>), StatusCodes.Status200OK)]
        public ActionResult<List<LivroDTO>> GetLivrosDisponiveis()
        {
            var livros = _livroService.GetLivrosDisponiveis();
            var livrosDTO = _mapper.Map<List<LivroDTO>>(livros);
            return Ok(livrosDTO);
        }

        [HttpGet("por-autor/{idAutor:int}")]
        [ProducesResponseType(typeof(List<LivroDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<LivroDTO>> GetLivrosPorAutor(int idAutor)
        {
            var livros = _livroService.GetLivrosPorAutor(idAutor);

            if (livros == null || livros.Count == 0)
                return NotFound("Nenhum livro encontrado para esse autor.");

            var livrosDTO = _mapper.Map<List<LivroDTO>>(livros);
            return Ok(livrosDTO);
        }
    }
}
