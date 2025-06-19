using System;
using System.Collections.Generic;

namespace Trab_Final.BaseDados.Models;

public partial class Pessoa
{
    public int IdPessoa { get; set; }

    public string NomePessoa { get; set; }

    public DateOnly? DataNascimento { get; set; }

    public string CpfPessoa { get; set; }

    public virtual ICollection<Emprestimo> Emprestimos { get; set; } = new List<Emprestimo>();
}
