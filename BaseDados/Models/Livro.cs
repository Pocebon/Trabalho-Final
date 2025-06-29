using System;
using System.Collections.Generic;
using Trab_Final.Services.DTOs;

namespace Trab_Final.BaseDados.Models;

public partial class Livro
{
    public int IdLivro { get; set; }

    public string NomeLivro { get; set; }

    public int? AnoPublicacao { get; set; }

    public int? IdEditora { get; set; }

    public string disponivel { get; set; }

    public int? Id_autor { get; set; }

    public string Prateleira { get; set; }

    public int? Estante { get; set; }

    public virtual Editora IdEditoraNavigation { get; set; }

    public virtual Autor IdAutorNavigation { get; set; }

    public virtual ICollection<Emprestimo> Emprestimo { get; set; } = new List<Emprestimo>();

    public virtual ICollection<EmprestimoLivro> EmprestimoLivros { get; set; } = new List<EmprestimoLivro>();
}
