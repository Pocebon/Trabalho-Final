using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Trab_Final.BaseDados.Models;
using Trab_Final.Services.DTOs;


namespace Trab_Final
{
    public class PessoaService
    {
        private readonly ApiJulianoContext _context;
        private readonly IMapper _mapper;

        public PessoaService(ApiJulianoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<PessoaDTO> GetAll()
        {
            var persons = _context.Pessoas.ToList();
            var PersonsDto = _mapper.Map<List<PessoaDTO>>(persons);
            return PersonsDto;

        }
        public PessoaDTO GetById(int id)
        {
            var pessoa = _context.Pessoas.FirstOrDefault(p => p.IdPessoa == id);
            return pessoa == null ? null : _mapper.Map<PessoaDTO>(pessoa);
        }

        public PessoaDTO Create(CriarPessoaDTO dto)
        {
            var pessoa = _mapper.Map<Pessoa>(dto);
            _context.Pessoas.Add(pessoa);
            _context.SaveChanges();

            return _mapper.Map<PessoaDTO>(pessoa);
        }

        public bool Delete(int id)
        {
            var pessoa = _context.Pessoas.FirstOrDefault(p => p.IdPessoa == id);
            if (pessoa == null)
                return false;

            _context.Pessoas.Remove(pessoa);
            _context.SaveChanges();
            return true;
        }
        public PessoaDTO AtualizarParcial(int id, AtualizarPessoaDTO dto)
        {
            var pessoa = _context.Pessoas.FirstOrDefault(p => p.IdPessoa == id);
            if (pessoa == null)
                return null;

            _mapper.Map(dto, pessoa); // aplica valores do DTO sobre a entidade existente

            _context.SaveChanges();

            return _mapper.Map<PessoaDTO>(pessoa);
        }
    }
}
