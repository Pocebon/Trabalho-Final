using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Trab_Final.BaseDados.Models;
using Trab_Final.Services.DTOs;

namespace Trab_Final.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json", "application/xml")]
    [ProducesResponseType(typeof(AutorDTO), StatusCodes.Status200OK, "application/json")]
    [ProducesResponseType(typeof(AutorDTO), StatusCodes.Status200OK, "application/xml")]
    [ApiController]
    public class EmprestimoController : ControllerBase
    {
        private readonly EmprestimoService _emprestimoService;
        private readonly IMapper _mapper;

        public EmprestimoController(EmprestimoService emprestimoService, IMapper mapper)
        {
            _emprestimoService = emprestimoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _emprestimoService.GetAllAsync();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EmprestimoDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EditoraDTO>> GetById(int id)
        {
            var emprestimo = await _emprestimoService.GetById(id);

            if (emprestimo == null)
                return NotFound();

            return Ok(emprestimo);
        }

        [HttpPost]
        [ProducesResponseType(typeof(EmprestimoDTO), StatusCodes.Status201Created)]
        public async Task<ActionResult<EmprestimoDTO>> Create([FromBody] CriarEmprestimoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (dto.EmprestimoDevolvido != null && dto.EmprestimoDevolvido != "S" && dto.EmprestimoDevolvido != "N")
                return BadRequest("'EmprestimoDevolvido' deve ser 'S' ou 'N'.");

            var emprestimoCriado = await _emprestimoService.Create(dto);

            return CreatedAtAction(nameof(GetById), new { id = emprestimoCriado.IdEmprestimo }, emprestimoCriado);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _emprestimoService.Delete(id);
            if (!deleted)
                return NotFound();

            return NoContent();

        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(EmprestimoDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmprestimoDTO>> AtualizarParcial(int id, [FromBody] AtualizarEmprestimoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dto.EmprestimoDevolvido != null && dto.EmprestimoDevolvido != "S" && dto.EmprestimoDevolvido != "N")
                return BadRequest("'EmprestimoDevolvido' deve ser 'S' ou 'N'.");

            var emprestimoAtualizado = await _emprestimoService.AtualizarParcial(id, dto);

            if (emprestimoAtualizado == null)
                return NotFound();

            return Ok(emprestimoAtualizado);
        }

        [HttpPut("status")]
        public IActionResult AtualizarStatus([FromBody] EmprestimoStatusDTO dto)
        {
            if (dto.EmprestimoDevolvido != "S")
                return BadRequest("Só é permitido alterar o status para 'S'.");

            var sucesso = _emprestimoService.AtualizarStatus(dto);
            if (!sucesso)
                return NotFound("Empréstimo não encontrado ou já está com status 'S'.");

            return Ok("Status atualizado para 'S'.");
        }
    }
}
