using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<LivroDTO>> GetAll()
        {
            var livros = await _context.Livros.ToListAsync();
            return _mapper.Map<List<LivroDTO>>(livros);
        }

        public async Task<LivroDTO?> GetById(int id)
        {
            var livro = await _context.Livros.FirstOrDefaultAsync(l => l.IdLivro == id);
            return  _mapper.Map<LivroDTO>(livro);
        }

        public async Task<LivroDTO> Create(CriarLivroDTO dto)
        {
            var livro = _mapper.Map<Livro>(dto);

           await _context.Livros.AddAsync(livro);
           await  _context.SaveChangesAsync();

            return _mapper.Map<LivroDTO>(livro);
        }

        public async Task<bool> Delete(int id)
        {
            var livro = await _context.Livros.FirstOrDefaultAsync(l => l.IdLivro == id);
            if (livro == null)
                return false;

            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<AtualizarLivroDTO> AtualizarParcial(int id, AtualizarLivroDTO dto)
        {
            var livro = await _context.Livros.FirstOrDefaultAsync(l => l.IdLivro == id);

            if (livro == null)
                return null;

            _mapper.Map(dto, livro); // aplica valores do DTO sobre a entidade existente
           await _context.SaveChangesAsync();
            return _mapper.Map<AtualizarLivroDTO>(livro);
        }

        public List<Livro> GetLivrosDisponiveis()
        {
            return _context.Livros
                .Where(l => l.disponivel == "S")
                .ToList();
        }
        public List<Livro> GetLivrosPorAutor(int id_autor)
        {
            return _context.Livros
                .Where(l => l.Id_autor == id_autor)    // NÃO ESTA FUNCIONANDO
                .ToList();
        }
    }
}
