using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PryCafeteria.Models;
using PryCafeteria.Models.ViewModels;

namespace PryCafeteria.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsuariosController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly BdcafeteriaContext _context;

        public UsuariosController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, BdcafeteriaContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index(string? buscar, string? tab)
        {
            tab = tab ?? "admins";
            var usuarios = _userManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
            {
                buscar = buscar.ToLower();
                usuarios = usuarios.Where(u =>
                    (u.Nombre != null && u.Nombre.ToLower().Contains(buscar)) ||
                    (u.Apellido != null && u.Apellido.ToLower().Contains(buscar)) ||
                    (u.Email != null && u.Email.ToLower().Contains(buscar)));
            }

            var lista = new List<UsuarioViewModel>();

            foreach (var u in await usuarios.ToListAsync())
            {
                var roles = await _userManager.GetRolesAsync(u);
                var rol = roles.FirstOrDefault() ?? "Sin rol";

                // Filtrar por tab
                if (tab == "admins" && rol != "Admin") continue;
                if (tab == "clientes" && rol != "Cliente") continue;

                lista.Add(new UsuarioViewModel
                {
                    Id = u.Id,
                    Nombre = u.Nombre,
                    Apellido = u.Apellido,
                    Email = u.Email,
                    Rol = rol,
                    FechaRegistro = u.FechaRegistro,
                    TotalPedidos = await _context.Pedidos.CountAsync(p => p.UsuarioId == u.Id)
                });
            }

            ViewBag.Buscar = buscar;
            ViewBag.Tab = tab;
            return View(lista);
        }

        // GET: Usuarios/Crear
        public IActionResult Create()
        {
            ViewBag.Roles = new List<string> { "Admin", "Cliente" };
            return View();
        }

        // POST: Usuarios/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioViewModel modelo)
        {
            if (string.IsNullOrEmpty(modelo.Password))
                ModelState.AddModelError("Password", "La contraseña es obligatoria al crear un usuario");

            if (!ModelState.IsValid)
            {
                ViewBag.Roles = new List<string> { "Admin", "Cliente" };
                return View(modelo);
            }

            // Verificar email duplicado
            var existente = await _userManager.FindByEmailAsync(modelo.Email!);
            if (existente != null)
            {
                ModelState.AddModelError("Email", "Este correo ya está registrado");
                ViewBag.Roles = new List<string> { "Admin", "Cliente" };
                return View(modelo);
            }

            var usuario = new ApplicationUser
            {
                UserName = modelo.Email,
                Email = modelo.Email,
                Nombre = modelo.Nombre,
                Apellido = modelo.Apellido,
                EmailConfirmed = true,
                FechaRegistro = DateTime.Now
            };

            var resultado = await _userManager.CreateAsync(usuario, modelo.Password!);

            if (resultado.Succeeded)
            {
                await _userManager.AddToRoleAsync(usuario, modelo.Rol!);
                TempData["Exito"] = "Usuario creado exitosamente";
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in resultado.Errors)
                ModelState.AddModelError("", error.Description);

            ViewBag.Roles = new List<string> { "Admin", "Cliente" };
            return View(modelo);
        }

        // GET: Usuarios/Editar/id
        public async Task<IActionResult> Edit(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null) return NotFound();

            var roles = await _userManager.GetRolesAsync(usuario);

            var modelo = new UsuarioViewModel
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Rol = roles.FirstOrDefault() ?? "Sin rol"
            };

            ViewBag.Roles = new List<string> { "Admin", "Cliente" };
            return View(modelo);
        }

        // POST: Usuarios/Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UsuarioViewModel modelo)
        {
            // Password no es requerido al editar
            ModelState.Remove("Password");

            if (!ModelState.IsValid)
            {
                ViewBag.Roles = new List<string> { "Admin", "Cliente" };
                return View(modelo);
            }

            var usuario = await _userManager.FindByIdAsync(modelo.Id!);
            if (usuario == null) return NotFound();

            usuario.Nombre = modelo.Nombre;
            usuario.Apellido = modelo.Apellido;
            usuario.Email = modelo.Email;
            usuario.UserName = modelo.Email;

            await _userManager.UpdateAsync(usuario);

            // Actualizar rol
            var rolesActuales = await _userManager.GetRolesAsync(usuario);
            await _userManager.RemoveFromRolesAsync(usuario, rolesActuales);
            await _userManager.AddToRoleAsync(usuario, modelo.Rol!);

            TempData["Exito"] = "Usuario actualizado correctamente";
            return RedirectToAction(nameof(Index));
        }

        // POST: Usuarios/Eliminar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null) return NotFound();

            // HU03 - E6: no eliminar si tiene pedidos
            var tienePedidos = await _context.Pedidos.AnyAsync(p => p.UsuarioId == id);
            if (tienePedidos)
            {
                TempData["Error"] = "No se puede eliminar: el usuario tiene pedidos asociados";
                return RedirectToAction(nameof(Index));
            }

            await _userManager.DeleteAsync(usuario);
            TempData["Exito"] = "Usuario eliminado correctamente";
            return RedirectToAction(nameof(Index));
        }
    }
}