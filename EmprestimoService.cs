using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Trab_Final.BaseDados.Models;
using Trab_Final.Services.DTOs;

namespace Trab_Final
{
    public class EmprestimoService
    {
        private readonly ApiJulianoContext _context;
        private readonly IMapper _mapper;


        public EmprestimoService(ApiJulianoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EmprestimoDTO>> GetAllAsync()
        {
            var emprestimos = await _context.Emprestimos.ToListAsync();
            return _mapper.Map<List<EmprestimoDTO>>(emprestimos);
        }

        public async Task<EmprestimoDTO?> GetById(int id)
        {
            var emprestimo = await _context.Emprestimos.FirstOrDefaultAsync(e => e.IdEmprestimo == id);
            return _mapper.Map<EmprestimoDTO>(emprestimo);
        }

        public async Task<EmprestimoDTO> Create(CriarEmprestimoDTO dto)
        {
            var emprestimo = _mapper.Map<Emprestimo>(dto);

            _context.Emprestimos.Add(emprestimo);
           await _context.SaveChangesAsync();

            return _mapper.Map<EmprestimoDTO>(emprestimo);
        }

        public async Task<bool> Delete(int id)
        {
            var emprestimo = await _context.Emprestimos.FirstOrDefaultAsync(e => e.IdEmprestimo == id);

            if (emprestimo == null)
                return false;

            _context.Emprestimos.Remove(emprestimo);
           await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AtualizarEmprestimoDTO> AtualizarParcial(int id, AtualizarEmprestimoDTO dto)
        {
            var emprestimo = await _context.Emprestimos.FirstOrDefaultAsync(e => e.IdEmprestimo == id);

            if (emprestimo == null)
                return null;

            _mapper.Map(dto, emprestimo); // aplica valores do DTO sobre a entidade existente
           await _context.SaveChangesAsync();
            return _mapper.Map<AtualizarEmprestimoDTO>(emprestimo);
        }

        public bool AtualizarStatus(EmprestimoStatusDTO dto)
        {
            var emprestimo = _context.Emprestimos
                .FirstOrDefault(e => e.IdEmprestimo == dto.IdEmprestimo);

            if (emprestimo == null || dto.EmprestimoDevolvido != "S")
                return false;

            // Atualiza o status do empréstimo
            emprestimo.EmprestimoDevolvido = "S";

            // Marca o livro como disponível
            var livro = _context.Livros.FirstOrDefault(l => l.IdLivro == emprestimo.IdLivro);
            if (livro != null)
            {
                livro.disponivel = "S";
            }

            _context.SaveChanges();
            return true;
        }
    }

}