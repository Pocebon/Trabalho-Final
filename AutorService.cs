using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Trab_Final.BaseDados.Models;
using Trab_Final.Services.DTOs;


namespace Trab_Final
{
    public class AutorService
    {

        private readonly ApiJulianoContext _context;
        private readonly IMapper _mapper;

        public AutorService(ApiJulianoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AutorDTO>> GetAll()
        {
            var autors = await _context.Autors.ToListAsync();
            var autoresDTOs = _mapper.Map<List<AutorDTO>>(autors);
            return autoresDTOs;
        }

        public async Task<AutorDTO> Create(CriarAutorDTO dto)
        {
            var autor = _mapper.Map<Autor>(dto);

           await _context.Autors.AddAsync(autor);
           await  _context.SaveChangesAsync();

            return _mapper.Map<AutorDTO>(autor);
        }
        public async Task<AutorDTO?> GetById(int id)
        {
            var autor = await _context.Autors.FirstOrDefaultAsync(a => a.IdAutor == id);

            return _mapper.Map<AutorDTO>(autor);
        }
        public async Task<bool> Delete(int id)
        {
            var autor = await _context.Autors.FirstOrDefaultAsync(a => a.IdAutor == id);

            if (autor == null)
                return false;

            _context.Autors.Remove(autor);
           await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AutorDTO?> AtualizarParcial(int id, AtualizarAutorDTO dto)
        {
            var autor = await _context.Autors.FirstOrDefaultAsync(a => a.IdAutor == id);


            _mapper.Map(dto, autor); 

           await _context.SaveChangesAsync();

            return _mapper.Map<AutorDTO>(autor);
        }

    }
}
