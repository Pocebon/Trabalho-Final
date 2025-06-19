using System;
using System.Collections.Generic;

namespace Trab_Final.BaseDados.Models;

public partial class Livro
{
    public int IdLivro { get; set; }

    public string NomeLivro { get; set; }

    public int? AnoPublicacao { get; set; }

    public int? IdEditora { get; set; }

    public virtual ICollection<EmprestimoLivro> EmprestimoLivros { get; set; } = new List<EmprestimoLivro>();

    public virtual Editora IdEditoraNavigation { get; set; }
}
