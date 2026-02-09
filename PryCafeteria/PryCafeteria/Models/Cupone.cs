using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PryCafeteria.Models;

public partial class Cupone
{
    public int CuponId { get; set; }

    [Display(Name = "Nombre")]
    public string NombreCupon { get; set; } = null!;

    [Display(Name = "Tipo")]
    public string TipoDescuento { get; set; } = null!;

    [Display(Name = "Valor")]
    public decimal ValorDescuento { get; set; }

    [Display(Name = "Fecha de Inicio")]
    public DateTime FechaInicio { get; set; }

    [Display(Name = "Fecha Final")]
    public DateTime FechaFin { get; set; }

    public bool Activo { get; set; }
}
