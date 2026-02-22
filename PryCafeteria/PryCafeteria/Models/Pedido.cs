using System;
using System.Collections.Generic;

namespace PryCafeteria.Models;

public partial class Pedido
{
    public int PedidoId { get; set; }

    public string UsuarioId { get; set; } = null!;

    public DateTime FechaPedido { get; set; }

    public decimal Descuento { get; set; }

    public decimal Total { get; set; }

    public int MetodoPagoId { get; set; }

    public int? DireccionId { get; set; }

    public string TipoEntrega { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual DireccionesEntrega? Direccion { get; set; }

    public virtual MetodosPago MetodoPago { get; set; } = null!;

    public virtual ApplicationUser Usuario { get; set; } = null!;
}
