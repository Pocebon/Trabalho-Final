using System;

namespace Trab_Final.Services.DTOs
{
    public class PessoaDTO
    {
        public int IdPessoa { get; set; }

        public string NomePessoa { get; set; }

        public DateOnly? DataNascimento { get; set; }

        public string CpfPessoa { get; set; }
    }
    public class CriarPessoaDTO
    {
        public string NomePessoa { get; set; }
        public DateOnly? DataNascimento { get; set; }
        public string CpfPessoa { get; set; }
    }

    public class AtualizarPessoaDTO
    {
        public string? NomePessoa { get; set; }
        public DateOnly? DataNascimento { get; set; }
        public string? CpfPessoa { get; set; }
    }
}
