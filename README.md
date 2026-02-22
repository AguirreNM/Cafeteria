# ☕ Cafetería Starbucks — Sistema de Gestión MVP

Sistema de gestión para cafetería desarrollado con **ASP.NET Core 8.0 MVC**, **Entity Framework Core** y **ASP.NET Identity**.

## 🚀 Funcionalidades Implementadas (MVP)

### HU01 — Registro de clientes
- Registro con nombre, apellido, email y contraseña
- Validaciones de formato y longitud en todos los campos
- Verificación de email duplicado
- Asignación automática de rol "Cliente"

### HU02 — Login / Logout
- Autenticación con ASP.NET Identity
- Bloqueo automático tras 5 intentos fallidos (15 minutos)
- Opción "Recordarme" con cookie de 14 días
- Redirección automática: Admin → Dashboard, Cliente → Home
- Mensaje de error genérico (no revela si el email existe)

### HU03 — Gestión de usuarios (Admin)
- Listado con tabs: Administradores / Clientes
- Búsqueda por nombre o email
- Crear, editar y eliminar usuarios
- Cambio de roles
- Fecha de registro y total de pedidos por usuario
- Bloqueo de eliminación si el usuario tiene pedidos

### HU04 — Gestión de productos
- CRUD completo de productos
- Validación de nombre duplicado
- Validaciones de longitud (3–100 caracteres)
- Control de disponibilidad
- Bloqueo de eliminación si tiene ventas registradas

### HU05 — Gestión de categorías
- CRUD completo de categorías
- Validación de nombre duplicado
- Bloqueo de eliminación si tiene productos asociados

### HU08 — Tamaños y precios por producto
- CRUD de tamaños y combinaciones producto-tamaño
- Validación de precio mayor a 0
- Validación de combinación única producto+tamaño
- Bloqueo de eliminación si tiene ventas registradas

### HU09 — Gestión de stock
- Control de stock por producto y tamaño
- Estados: En stock / Bajo stock (≤5) / Agotado
- Alerta visual en el Dashboard

### HU20 — Dashboard administrativo
- Tarjetas de resumen: productos, categorías, cupones activos, stock crítico
- Pedidos del día y pedidos pendientes
- Ingresos del día (pedidos entregados)
- Listado de productos con stock crítico
- Últimos productos agregados

---

## 🛠️ Tecnologías Utilizadas

- **Framework:** ASP.NET Core 8.0 MVC
- **ORM:** Entity Framework Core 8
- **Autenticación:** ASP.NET Core Identity
- **Base de Datos:** SQL Server / SQL Server Express
- **Frontend:** Razor Views, Bootstrap 5, jQuery, Font Awesome
- **Patrón:** MVC (Model-View-Controller)

---

## 📋 Requisitos Previos

- .NET 8.0 SDK
- SQL Server 2019+ o SQL Server Express
- Visual Studio 2022 (recomendado)

---

## 🔧 Instalación

### 1. Clonar el repositorio
```bash
git clone https://github.com/AguirreNM/Cafeter-a.git
cd Cafeter-a
```

### 2. Crear la base de datos
Ejecuta el script SQL ubicado en la raíz del proyecto:
```sql
BDCAFETERIA.sql
```

### 3. Configurar la cadena de conexión
Edita `PryCafeteria/PryCafeteria/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "BDCAFETERIAConn": "Server=TU_SERVIDOR;Database=BDCAFETERIA;Integrated Security=True;TrustServerCertificate=True;"
  }
}
```

### 4. Aplicar migraciones
```bash
cd PryCafeteria/PryCafeteria
dotnet ef database update
```

### 5. Ejecutar el proyecto
```bash
dotnet run
```

---

## 👤 Usuario Administrador por Defecto

```
Email:      admin@gmail.com
Contraseña: admin123
Rol:        Admin
```

---

## 📊 Estructura del Proyecto

```
PryCafeteria/
├── Controllers/
│   ├── CuentasController.cs       # Login, registro, logout
│   ├── DashboardController.cs     # Panel administrativo
│   ├── UsuariosController.cs      # Gestión de usuarios
│   ├── ProductosController.cs     # CRUD productos
│   ├── CategoriasController.cs    # CRUD categorías
│   ├── TamaniosController.cs      # CRUD tamaños
│   ├── ProductosTamaniosController.cs  # Stock y precios
│   ├── CuponesController.cs       # CRUD cupones
│   └── MetodosPagosController.cs  # Métodos de pago
├── Models/
│   ├── ApplicationUser.cs         # Usuario extendido con Identity
│   ├── CustomClaimsPrincipalFactory.cs  # Claims personalizados
│   ├── ViewModels/                # LoginViewModel, RegistroViewModel, UsuarioViewModel
│   └── ...                        # Modelos de BD (EF)
├── Views/
│   ├── Cuentas/                   # Login y registro
│   ├── Dashboard/                 # Panel admin
│   ├── Usuarios/
│   ├── Productos/
│   ├── Categorias/
│   ├── Tamanios/
│   ├── ProductosTamanios/
│   └── Shared/_Layout.cshtml
├── Migrations/                    # Migraciones EF
├── wwwroot/
│   ├── css/                       # site.css, dashboard.css, login-style.css
│   ├── js/                        # login-script.js
│   └── images/productos/          # Imágenes por categoría
└── Program.cs                     # Configuración de la aplicación
```

---

## 🗄️ Modelo de Base de Datos

| Tabla | Descripción |
|---|---|
| AspNetUsers | Usuarios (Identity + Nombre, Apellido, FechaRegistro) |
| AspNetRoles | Roles: Admin, Cliente |
| Categorias | Categorías de productos |
| Productos | Catálogo de productos |
| Tamanios | Tamaños disponibles (Pequeño, Mediano, Grande) |
| ProductosTamanios | Precios y stock por producto+tamaño |
| Pedidos | Órdenes de compra |
| DetallePedido | Items de cada pedido |
| Cupones | Descuentos por porcentaje o monto fijo |
| MetodosPago | Métodos de pago disponibles |
| DireccionesEntrega | Direcciones de clientes |

---

## 🎨 Paleta de Colores

| Color | Hex |
|---|---|
| Verde Starbucks | #00704A |
| Verde Oscuro | #005238 |
| Crema | #D4AF77 |
| Fondo claro | #F0EBE0 |

---

## 🚧 Pendiente (fuera del MVP)

- [ ] Carrito de compras
- [ ] Proceso de checkout completo
- [ ] Historial de movimientos de stock
- [ ] Gráficos en el dashboard
- [ ] Subida de imágenes desde formulario
- [ ] Recuperación de contraseña por email
- [ ] Paginación en listados

---

## 👨‍💻 Autor

Desarrollado por **AguirreNM**
