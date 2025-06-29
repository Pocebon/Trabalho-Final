using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Trab_Final.BaseDados.Models;
using Trab_Final.Services.DTOs;

namespace Trab_Final
{
    public class EmprestimoLivroService
    {
        private readonly ApiJulianoContext _context;
        private readonly IMapper _mapper;

        public EmprestimoLivroService(ApiJulianoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EmprestimoLivroDTO>> GetAll()
        {
            var emprestimosLivros = _context.EmprestimoLivros.ToList();
            return _mapper.Map<List<EmprestimoLivroDTO>>(emprestimosLivros);
        }

        public async Task<EmprestimoLivroDTO?> GetById(int id)
        {
            var emprestimoLivro = await _context.EmprestimoLivros.FirstOrDefaultAsync(el => el.IdEmprestimoLivro == id);
            return  _mapper.Map<EmprestimoLivroDTO>(emprestimoLivro);
        }

        public async Task<EmprestimoLivroDTO> Create(CriarEmprestimoLivroDTO dto)
        {
            var emprestimoLivro = _mapper.Map<EmprestimoLivro>(dto);

            _context.EmprestimoLivros.Add(emprestimoLivro);
            await _context.SaveChangesAsync();
            return _mapper.Map<EmprestimoLivroDTO>(emprestimoLivro);
        }

        public async Task<bool> Delete(int id)
        {
            var emprestimoLivro = await _context.EmprestimoLivros.FirstOrDefaultAsync(el => el.IdEmprestimoLivro == id);

            if (emprestimoLivro == null)
                return false;

            _context.EmprestimoLivros.Remove(emprestimoLivro);
           await _context.SaveChangesAsync();
            return true;
        }

        public async Task<EmprestimoLivroDTO?> AtualizarParcial(int id, AtualizarEmprestimoLivroDTO dto)
        {
            var emprestimoLivro = await _context.EmprestimoLivros.FirstOrDefaultAsync(el => el.IdEmprestimoLivro == id);

            if (emprestimoLivro == null)
                return null;

            _mapper.Map(dto, emprestimoLivro); // aplica valores do DTO sobre a entidade existente
            await _context.SaveChangesAsync();

            return _mapper.Map<EmprestimoLivroDTO>(emprestimoLivro);
        }
    }
}
