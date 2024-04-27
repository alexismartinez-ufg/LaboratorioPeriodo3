using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaboratorioPeriodo3.DB.Models;

public partial class Autore
{
    public int AutorId { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    [NotMapped]
    public string NombreCompleto => $"{Nombre} {Apellido}";
}
