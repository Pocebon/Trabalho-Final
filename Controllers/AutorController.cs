using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Components.Forms.Mapping;
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
    public class AutorController : ControllerBase
    {
        private readonly AutorService _autorService;
        private readonly IMapper _mapper;

        public AutorController(AutorService autorService, IMapper mapper)
        {
            _autorService = autorService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<AutorDTO>> GetAll()
        {
            var lista = _autorService.GetAll();

            return Ok(lista);
        }

        [HttpPost]
        [ProducesResponseType(typeof(AutorDTO), StatusCodes.Status201Created)]
        public ActionResult<AutorDTO> Create([FromBody] CriarAutorDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var autorCriado = _autorService.Create(dto);

            return CreatedAtAction(nameof(GetById), new { id = autorCriado.IdAutor }, autorCriado);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AutorDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<AutorDTO> GetById(int id)
        {
            var autor = _autorService.GetById(id);

            if (autor == null)
                return NotFound();

            return Ok(autor);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var deletado = _autorService.Delete(id);

            if (!deletado)
                return NotFound();

            return NoContent(); // HTTP 204
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(AutorDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<AutorDTO> AtualizarParcial(int id, [FromBody] AtualizarAutorDTO dto)
        {
            var autorAtualizado = _autorService.AtualizarParcial(id, dto);

            if (autorAtualizado == null)
                return NotFound();

            return Ok(autorAtualizado);
        }

   
    }
}

