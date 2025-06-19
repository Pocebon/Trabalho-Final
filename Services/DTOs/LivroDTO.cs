namespace Trab_Final.Services.DTOs
{
    public class LivroDTO
    {
        public int IdLivro { get; set; }

        public string NomeLivro { get; set; }

        public int? AnoPublicacao { get; set; }

        public int? IdEditora { get; set; }

    }

    public class CriarLivroDTO
    {
        public string NomeLivro { get; set; }
        public int? AnoPublicacao { get; set; }
        public int? IdEditora { get; set; }
        public int? IdAutor { get; set; } 
    }

    public class AtualizarLivroDTO
    {
        public string? NomeLivro { get; set; }
        public int? AnoPublicacao { get; set; }
    }

}
