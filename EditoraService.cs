using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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

        public List<EditoraDTO> GetAll()
        {
            var editoras = _context.Editoras.ToList();
            return _mapper.Map<List<EditoraDTO>>(editoras);
        }

        public EditoraDTO Create(CriarEditoraDTO dto)
        {
            var editora = _mapper.Map<Editora>(dto);

            _context.Editoras.Add(editora);
            _context.SaveChanges();
            return _mapper.Map<EditoraDTO>(editora);
        }

        public EditoraDTO GetById(int id)
        {
            var editora = _context.Editoras.FirstOrDefault(e => e.IdEditora == id);
            if (editora == null)
                return null;

            return editora == null ? null : _mapper.Map<EditoraDTO>(editora);
        }

        public bool Delete(int id)
        {
            var editora = _context.Editoras.FirstOrDefault(e => e.IdEditora == id);
            if (editora == null)
                return false;
            _context.Editoras.Remove(editora);
            _context.SaveChanges();
            return true;
        }
        public EditoraDTO AtualizarParcial(int id, AtualizarEditoraDTO dto)
        {
            var editora = _context.Editoras.FirstOrDefault(e => e.IdEditora == id);
            if (editora == null)
                return null;
            _mapper.Map(dto, editora); // aplica valores do DTO sobre a entidade existente
            _context.SaveChanges();
            return _mapper.Map<EditoraDTO>(editora);
        }
    }
}
