using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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

        public List<AutorDTO> GetAll()
        {
            var autors = _context.Autors.ToList();
            var autoresDTOs = _mapper.Map<List<AutorDTO>>(autors);
            return autoresDTOs;

        }

        public AutorDTO Create(CriarAutorDTO dto)
        {
            var autor = _mapper.Map<Autor>(dto);
            _context.Autors.Add(autor);
            _context.SaveChanges();
            return _mapper.Map<AutorDTO>(autor);
        }
        public AutorDTO GetById(int id)
        {
            var autor = _context.Autors.FirstOrDefault(a => a.IdAutor == id);

            if (autor == null)
                return null;

            return _mapper.Map<AutorDTO>(autor);
        }
        public bool Delete(int id)
        {
            var autor = _context.Autors.FirstOrDefault(a => a.IdAutor == id);

            if (autor == null)
                return false;

            _context.Autors.Remove(autor);
            _context.SaveChanges();
            return true;
        }

        public AutorDTO AtualizarParcial(int id, AtualizarAutorDTO dto)
        {
            var autor = _context.Autors.FirstOrDefault(a => a.IdAutor == id);


            _mapper.Map(dto, autor); 

            _context.SaveChanges();

            return _mapper.Map<AutorDTO>(autor);
        }

    }
}
