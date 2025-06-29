using System;
using System.Collections.Generic;
using Trab_Final.BaseDados.Models;

namespace Trab_Final.Services.DTOs
{
    public class Emprestimo
    {
        public int IdEmprestimo { get; set; }

        public int? IdPessoa { get; set; }

        public int? IdLivro { get; set; }

        public DateOnly? DataEmprestimo { get; set; }

        public DateOnly? DataDevolucao { get; set; }

        public string EmprestimoDevolvido { get; set; }

        // Mesma estrutura do modelo: lista de EmprestimoLivro
        public virtual Livro IdLivroNavigation { get; set; }

        public virtual ICollection<EmprestimoLivro> EmprestimoLivros { get; set; } = new List<EmprestimoLivro>();

        public virtual Pessoa IdPessoaNavigation { get; set; }
    }
}
