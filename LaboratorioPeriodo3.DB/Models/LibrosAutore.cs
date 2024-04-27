using System;
using System.Collections.Generic;

namespace LaboratorioPeriodo3.DB.Models;

public partial class LibrosAutore
{
    public int? CodigoLibro { get; set; }

    public int? AutorId { get; set; }

    public virtual Autore? Autor { get; set; }

    public virtual Libro? CodigoLibroNavigation { get; set; }
}
