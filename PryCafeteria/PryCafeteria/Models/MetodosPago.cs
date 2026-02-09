using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PryCafeteria.Models;

public partial class MetodosPago
{
    public int MetodoPagoId { get; set; }

    [Display(Name = "Nombre")]
    public string NombreMetodoPago { get; set; } = null!;

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
