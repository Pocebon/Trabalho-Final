using System;

namespace Trab_Final.Services.DTOs
{
    public class EmprestimoDTO
    {
        public int IdEmprestimo { get; set; }
        public DateOnly DataEmprestimo { get; set; }
        public DateOnly? DataDevolucao { get; set; }
        public int IdPessoa { get; set; }
        public int IdLivro { get; set; }
    }

    public class CriarEmprestimoDTO
    {
        public DateOnly DataEmprestimo { get; set; }
        public DateOnly? DataDevolucao { get; set; }
        public int IdPessoa { get; set; }
        public int IdLivro { get; set; }
    }

    public class AtualizarEmprestimoDTO
    {
        public DateOnly? DataEmprestimo { get; set; }
        public DateOnly? DataDevolucao { get; set; }
    }
}
