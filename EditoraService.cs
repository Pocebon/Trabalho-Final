using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Trab_Final.BaseDados.Models;
using Trab_Final.Services.DTOs;

namespace Trab_Final
{
    public class EditoraService
    {
        private readonly ApiJulianoContext _context;
        private readonly IMapper _mapper;

        public EditoraService(ApiJulianoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EditoraDTO>> GetAll()
        {
            var editoras = _context.Editoras.ToList();
            return _mapper.Map<List<EditoraDTO>>(editoras);
        }

        public async Task<EditoraDTO> Create(CriarEditoraDTO dto)
        {
            var editora = _mapper.Map<Editora>(dto);

           await  _context.Editoras.AddAsync(editora);
           await  _context.SaveChangesAsync();
            return _mapper.Map<EditoraDTO>(editora);
        }

        public async Task<EditoraDTO?> GetById(int id)
        {
            var editora = await _context.Editoras.FirstOrDefaultAsync(e => e.IdEditora == id);
            if (editora == null)
                return null;

            return _mapper.Map<EditoraDTO>(editora);
        }

        public async Task<bool> Delete(int id)
        {
            var editora = await _context.Editoras.FirstOrDefaultAsync(e => e.IdEditora == id);

            if (editora == null)
                return false;

            _context.Editoras.Remove(editora);
           await _context.SaveChangesAsync();
            return true;
        }
        public async Task<EditoraDTO?> AtualizarParcial(int id, AtualizarEditoraDTO dto)
        {
            var editora = await _context.Editoras.FirstOrDefaultAsync(e => e.IdEditora == id);

            if (editora == null)
                return null;

            _mapper.Map(dto, editora); // aplica valores do DTO sobre a entidade existente
            await _context.SaveChangesAsync();
            return _mapper.Map<EditoraDTO>(editora);
        }
    }
}
