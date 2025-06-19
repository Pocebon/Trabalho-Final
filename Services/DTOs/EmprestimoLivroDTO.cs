using System.ComponentModel.DataAnnotations;

namespace Trab_Final.Services.DTOs
{
    public class EmprestimoLivroDTO
    {
        public int IdEmprestimoLivro { get; set; }
        public int IdLivro { get; set; }
        public int IdEmprestimo { get; set; }
    }

    public class CriarEmprestimoLivroDTO
    {
        public int IdEmprestimo { get; set; }
        public int IdLivro { get; set; }
    }

    public class AtualizarEmprestimoLivroDTO
    {
        [Required]
        public int IdLivro { get; set; }

        [Required]
        public int IdEmprestimo { get; set; }
    }
}
