using System.Collections.Generic;
using System.Threading.Tasks;
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
    
        public class Editora : ControllerBase
        {
            private readonly EditoraService _editoraService;
            private readonly IMapper _mapper;

            public Editora(EditoraService editoraService, IMapper mapper)
            {
                _editoraService = editoraService;
                _mapper = mapper;
            }
            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var lista = await _editoraService.GetAll();
                return Ok(lista);
            }

            [HttpPost]
            [ProducesResponseType(typeof(EditoraDTO), StatusCodes.Status201Created)]
        public async Task<ActionResult<EditoraDTO>> Create([FromBody] CriarEditoraDTO dto)
        {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var editoraCriada = await _editoraService.Create(dto);

                return CreatedAtAction(nameof(GetById), new { id = editoraCriada.IdEditora }, editoraCriada);
            }


            [HttpGet("{id}")]
            [ProducesResponseType(typeof(EditoraDTO), StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EditoraDTO>> GetById(int id)
        {
                var editora = await _editoraService.GetById(id);

                if (editora == null)
                    return NotFound();

                return Ok(editora);
            }

            [HttpDelete("{id}")]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
                var deleted = await _editoraService.Delete(id);

                if (!deleted)
                   return NotFound();

                return NoContent();
            }

            [HttpPatch("{id}")]
            [ProducesResponseType(typeof(EditoraDTO), StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EditoraDTO>> AtualizarParcial(int id, [FromBody] AtualizarEditoraDTO dto)
        {              
                var editoraAtualizada = await _editoraService.AtualizarParcial(id, dto);

                if (editoraAtualizada == null)
                    return NotFound();

                return Ok(editoraAtualizada);
            }
        }
    }


