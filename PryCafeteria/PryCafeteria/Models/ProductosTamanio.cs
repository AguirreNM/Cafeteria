using System;
using System.Collections.Generic;

namespace PryCafeteria.Models;

public partial class ProductosTamanio
{
    public int ProductoTamanioId { get; set; }

    public int ProductoId { get; set; }

    public int TamanioId { get; set; }

    public decimal Precio { get; set; }

    public int Stock { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual Producto Producto { get; set; } = null!;

    public virtual Tamanio Tamanio { get; set; } = null!;
}
