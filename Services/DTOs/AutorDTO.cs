using System;

namespace Trab_Final.Services.DTOs

{
    public class AutorDTO
    {
        public int IdAutor { get; set; }
        public string NomeAutor { get; set; }
        public string Nacionalidade { get; set; }
        public DateTime DataNascimento { get; set; }
    }

    public class CriarAutorDTO
    {
        public string NomeAutor { get; set; }
        public string Nacionalidade { get; set; }
        public DateTime DataNascimento { get; set; }
    }
    public class AtualizarAutorDTO
    {
        public string? NomeAutor { get; set; }
        public string? Nacionalidade { get; set; }
        public DateTime? DataNascimento { get; set; }
    }
}
