using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PryCafeteria.Models;

public partial class Categoria
{
    public int CategoriaId { get; set; }

    [Display(Name = "Categoría")]
    public string NombreCategoria { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
