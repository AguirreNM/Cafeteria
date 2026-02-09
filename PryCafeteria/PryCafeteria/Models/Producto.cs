using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PryCafeteria.Models;

public partial class Producto
{
    public int ProductoId { get; set; }

    [Display(Name = "Producto")]
    public string NombreProducto { get; set; } = null!;

    [Display(Name = "Descripción")]
    public string? Descripcion { get; set; }

    public int CategoriaId { get; set; }

    public string? Imagen { get; set; }

    public bool Disponible { get; set; }

    [Display(Name = "Categoría")]
    public virtual Categoria Categoria { get; set; } = null!;

    public virtual ICollection<ProductosTamanio> ProductosTamanios { get; set; } = new List<ProductosTamanio>();
}
