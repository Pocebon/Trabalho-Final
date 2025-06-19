using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Trab_Final.BaseDados.Models;
using Trab_Final.Services.DTOs;

namespace Trab_Final
{
    public class LivroService
    {
        private readonly ApiJulianoContext _context;
        private readonly IMapper _mapper;

        public LivroService(ApiJulianoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<LivroDTO> GetAll()
        {
            var livros = _context.Livros.ToList();
            return _mapper.Map<List<LivroDTO>>(livros);
        }

        public LivroDTO GetById(int id)
        {
            var livro = _context.Livros.FirstOrDefault(l => l.IdLivro == id);
            return livro == null ? null : _mapper.Map<LivroDTO>(livro);
        }

        public LivroDTO Create(CriarLivroDTO dto)
        {
            var livro = _mapper.Map<Livro>(dto);
            _context.Livros.Add(livro);
            _context.SaveChanges();
            return _mapper.Map<LivroDTO>(livro);
        }

        public bool Delete(int id)
        {
            var livro = _context.Livros.FirstOrDefault(l => l.IdLivro == id);
            if (livro == null)
                return false;
            _context.Livros.Remove(livro);
            _context.SaveChanges();
            return true;
        }
        public LivroDTO AtualizarParcial(int id, AtualizarLivroDTO dto)
        {
            var livro = _context.Livros.FirstOrDefault(l => l.IdLivro == id);
            if (livro == null)
                return null;
            _mapper.Map(dto, livro); // aplica valores do DTO sobre a entidade existente
            _context.SaveChanges();
            return _mapper.Map<LivroDTO>(livro);
        }
    }
}
