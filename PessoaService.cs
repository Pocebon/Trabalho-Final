using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<PessoaDTO>> GetAll()
        {
            var persons = await _context.Pessoas.ToListAsync();
            var personsDto = _mapper.Map<List<PessoaDTO>>(persons);
            return personsDto;
        }

        public async Task<PessoaDTO?> GetById(int id)
        {
            var pessoa = await _context.Pessoas.FirstOrDefaultAsync(p => p.IdPessoa == id);
            return pessoa == null ? null : _mapper.Map<PessoaDTO>(pessoa);
        }

        public async Task<PessoaDTO> Create(CriarPessoaDTO dto)
        {
            var pessoa = _mapper.Map<Pessoa>(dto);

            await _context.Pessoas.AddAsync(pessoa);        
            await _context.SaveChangesAsync();              

            return _mapper.Map<PessoaDTO>(pessoa);
        }

        public async Task<bool> Delete(int id)
        {
            var pessoa = await _context.Pessoas.FirstOrDefaultAsync(p => p.IdPessoa == id);

            if (pessoa == null)
                return false;

            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<PessoaDTO?> AtualizarParcial(int id, AtualizarPessoaDTO dto)
        {
            var pessoa = await _context.Pessoas.FirstOrDefaultAsync(p => p.IdPessoa == id);
            if (pessoa == null)
                return null;

            _mapper.Map(dto, pessoa); // atualiza os campos da entidade com os do DTO

            await _context.SaveChangesAsync();

            return _mapper.Map<PessoaDTO>(pessoa);
        }
    }
}
