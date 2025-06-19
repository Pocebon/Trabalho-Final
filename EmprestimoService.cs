using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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

        public List<EmprestimoDTO> GetAll()
        {
            var emprestimos = _context.Emprestimos.ToList();
            return _mapper.Map<List<EmprestimoDTO>>(emprestimos);
        }

        public EmprestimoDTO GetById(int id)
        {
            var emprestimo = _context.Emprestimos.FirstOrDefault(e => e.IdEmprestimo == id);
            return emprestimo == null ? null : _mapper.Map<EmprestimoDTO>(emprestimo);
        }

        public EmprestimoDTO Create(CriarEmprestimoDTO dto)
        {
            var emprestimo = _mapper.Map<Emprestimo>(dto);

            _context.Emprestimos.Add(emprestimo);
            _context.SaveChanges();

            return _mapper.Map<EmprestimoDTO>(emprestimo);
        }

        public bool Delete(int id)
        {
            var emprestimo = _context.Emprestimos.FirstOrDefault(e => e.IdEmprestimo == id);
            if (emprestimo == null)
                return false;
            _context.Emprestimos.Remove(emprestimo);
            _context.SaveChanges();
            return true;
        }

        public EmprestimoDTO AtualizarParcial(int id, AtualizarEmprestimoDTO dto)
        {
            var emprestimo = _context.Emprestimos.FirstOrDefault(e => e.IdEmprestimo == id);
            if (emprestimo == null)
                return null;
            _mapper.Map(dto, emprestimo); // aplica valores do DTO sobre a entidade existente
            _context.SaveChanges();
            return _mapper.Map<EmprestimoDTO>(emprestimo);
        }
    }
  }
