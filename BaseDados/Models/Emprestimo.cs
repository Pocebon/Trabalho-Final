using System;
using System.Collections.Generic;

namespace Trab_Final.BaseDados.Models;

public partial class Emprestimo
{
    public int IdEmprestimo { get; set; }

    public int? IdPessoa { get; set; }

    public DateOnly? DataEmprestimo { get; set; }

    public DateOnly? DataDevolucao { get; set; }

    public virtual ICollection<EmprestimoLivro> EmprestimoLivros { get; set; } = new List<EmprestimoLivro>();

    public virtual Pessoa IdPessoaNavigation { get; set; }
}
