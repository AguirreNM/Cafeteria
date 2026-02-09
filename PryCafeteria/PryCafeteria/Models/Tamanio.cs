using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PryCafeteria.Models;

public partial class Tamanio
{
    public int TamanioId { get; set; }

    [Display(Name = "Tamaño")]
    public string NombreTamanio { get; set; } = null!;

    public virtual ICollection<ProductosTamanio> ProductosTamanios { get; set; } = new List<ProductosTamanio>();
}
