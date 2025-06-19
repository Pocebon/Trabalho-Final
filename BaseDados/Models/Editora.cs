using System;
using System.Collections.Generic;

namespace Trab_Final.BaseDados.Models;

public partial class Editora
{
    public int IdEditora { get; set; }

    public string NomeEditora { get; set; }

    public string CnpjEditora { get; set; }

    public virtual ICollection<Livro> Livros { get; set; } = new List<Livro>();
}
