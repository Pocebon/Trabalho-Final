using System;
using System.Collections.Generic;
using Trab_Final.Services.DTOs;

namespace Trab_Final.BaseDados.Models;

public partial class Pessoa
{
    public int IdPessoa { get; set; }

    public string NomePessoa { get; set; }

    public DateOnly? DataNascimento { get; set; }

    public string CpfPessoa { get; set; }

    public virtual ICollection<Emprestimo> Emprestimo { get; set; } = new List<Emprestimo>();
}
