using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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

        public List<EmprestimoLivroDTO> GetAll()
        {
            var emprestimosLivros = _context.EmprestimoLivros.ToList();
            return _mapper.Map<List<EmprestimoLivroDTO>>(emprestimosLivros);
        }

        public EmprestimoLivroDTO GetById(int id)
        {
            var emprestimoLivro = _context.EmprestimoLivros.FirstOrDefault(el => el.IdEmprestimoLivro == id);
            return emprestimoLivro == null ? null : _mapper.Map<EmprestimoLivroDTO>(emprestimoLivro);
        }

        public EmprestimoLivroDTO Create(CriarEmprestimoLivroDTO dto)
        {
            var emprestimoLivro = _mapper.Map<EmprestimoLivro>(dto);
            _context.EmprestimoLivros.Add(emprestimoLivro);
            _context.SaveChanges();
            return _mapper.Map<EmprestimoLivroDTO>(emprestimoLivro);
        }

        public bool Delete(int id)
        {
            var emprestimoLivro = _context.EmprestimoLivros.FirstOrDefault(el => el.IdEmprestimoLivro == id);
            if (emprestimoLivro == null)
                return false;
            _context.EmprestimoLivros.Remove(emprestimoLivro);
            _context.SaveChanges();
            return true;
        }

        public EmprestimoLivroDTO AtualizarParcial(int id, AtualizarEmprestimoLivroDTO dto)
        {
            var emprestimoLivro = _context.EmprestimoLivros.FirstOrDefault(el => el.IdEmprestimoLivro == id);
            if (emprestimoLivro == null)
                return null;
            _mapper.Map(dto, emprestimoLivro); // aplica valores do DTO sobre a entidade existente
            _context.SaveChanges();
            return _mapper.Map<EmprestimoLivroDTO>(emprestimoLivro);
        }
    }
}
