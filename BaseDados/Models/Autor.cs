using System;
using System.Collections.Generic;

namespace Trab_Final.BaseDados.Models;

public partial class Autor
{
    public int IdAutor { get; set; }

    public string NomeAutor { get; set; }

    public string Nacionalidade { get; set; }

    public DateTime? DataNascimento { get; set; }
}
