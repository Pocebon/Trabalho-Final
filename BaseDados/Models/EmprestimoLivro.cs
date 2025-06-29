using System;
using System.Collections.Generic;
using Trab_Final.Services.DTOs;

namespace Trab_Final.BaseDados.Models;

public partial class EmprestimoLivro
{
    public int IdEmprestimoLivro { get; set; }

    public int? IdLivro { get; set; }

    public int? IdEmprestimo { get; set; }

    public virtual Emprestimo IdEmprestimoNavigation { get; set; }

    public virtual Livro IdLivroNavigation { get; set; }
}
