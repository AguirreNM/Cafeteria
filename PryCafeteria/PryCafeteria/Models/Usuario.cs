using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PryCafeteria.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    [Display(Name = "Contraseña")]
    public string Contrasena { get; set; } = null!;

    public int RolId { get; set; }

    public virtual ICollection<DireccionesEntrega> DireccionesEntregas { get; set; } = new List<DireccionesEntrega>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual Role Rol { get; set; } = null!;
}
