using System;
using System.Collections.Generic;

namespace LaboratorioPeriodo3.DB.Models;

public partial class Libro
{
    public int Codigo { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Isbn { get; set; }

    public int? AnioEdicion { get; set; }

    public string? Editorial { get; set; }

    public string? Descripcion { get; set; }

    public string? NacionalidadAutor { get; set; }

    public virtual ICollection<LibrosAutore> LibrosAutores { get; set; } = new List<LibrosAutore>();
}
