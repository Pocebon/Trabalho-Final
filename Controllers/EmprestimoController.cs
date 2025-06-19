using System;
using System.Collections.Generic;
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
        public ActionResult<List<EmprestimoDTO>> GetAll()
        {
            var lista = _emprestimoService.GetAll();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EmprestimoDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<EmprestimoDTO> GetById(int id)
        {
            var emprestimo = _emprestimoService.GetById(id);

            if (emprestimo == null)
                return NotFound();

            return Ok(emprestimo);
        }

        [HttpPost]
        [ProducesResponseType(typeof(EmprestimoDTO), StatusCodes.Status201Created)]
        public ActionResult<EmprestimoDTO> Create([FromBody] CriarEmprestimoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var emprestimoCriado = _emprestimoService.Create(dto);

            return CreatedAtAction(nameof(GetById), new { id = emprestimoCriado.IdEmprestimo }, emprestimoCriado);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult Delete(int id)
        {
            var deleted = _emprestimoService.Delete(id);
            if (!deleted)
                return NotFound();

            return NoContent();

        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(EmprestimoDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<EmprestimoDTO> AtualizarParcial(int id, [FromBody] AtualizarEmprestimoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var emprestimoAtualizado = _emprestimoService.AtualizarParcial(id, dto);
            if (emprestimoAtualizado == null)
                return NotFound();

            return Ok(emprestimoAtualizado);
        }
    }
}
