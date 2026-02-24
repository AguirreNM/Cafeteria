# ☕ Cafetería Starbucks — Sistema de Gestión MVP

Sistema de gestión para cafetería desarrollado con **ASP.NET Core 8.0 MVC**, **Entity Framework Core** y **ASP.NET Identity**.

---

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
- Validación de stock no negativo
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

| Tecnología | Versión | Uso |
|---|---|---|
| ASP.NET Core MVC | 8.0 | Framework principal |
| Entity Framework Core | 8.0 | ORM y migraciones |
| ASP.NET Core Identity | 8.0 | Autenticación y roles |
| SQL Server / SQL Server Express | 2019+ | Base de datos |
| Bootstrap | 5 | Estilos y componentes UI |
| jQuery | 3.x | Validaciones cliente |

---

## 📋 Requisitos Previos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server 2019+ **o** SQL Server Express (instalación gratuita)
- Visual Studio 2022 (recomendado) **o** VS Code con extensión C#

---

## 🔧 Instalación paso a paso

### 1. Clonar el repositorio

```bash
git clone https://github.com/AguirreNM/Cafeter-a.git
cd Cafeter-a
```

### 2. Identificar el nombre de tu servidor SQL

Este es el paso más importante al instalar en una PC nueva. Abre **SQL Server Management Studio (SSMS)** — el nombre que aparece en el campo "Server name" al conectarte es el que necesitas.

Los nombres más comunes son:

| Caso | Server name a usar |
|---|---|
| SQL Server Express (instalación estándar) | `localhost\SQLEXPRESS` |
| SQL Server Express con nombre de PC | `NOMBRE-PC\SQLEXPRESS` |
| SQL Server Developer/Standard | `localhost` |
| SQL LocalDB (Visual Studio) | `(localdb)\MSSQLLocalDB` |

Si no sabes cuál es, ejecútalo en SSMS:
```sql
SELECT @@SERVERNAME
```

### 3. Configurar la cadena de conexión

Edita el archivo `PryCafeteria/PryCafeteria/appsettings.json` y reemplaza el valor de `Server` con el nombre de tu servidor:

```json
{
  "ConnectionStrings": {
    "BDCAFETERIAConn": "Persist Security Info=False;Integrated Security=True;Initial Catalog=BDCAFETERIA;Server=localhost\\SQLEXPRESS;Encrypt=True;TrustServerCertificate=True;"
  }
}
```

> **Nota:** El archivo `appsettings.Development.json.example` en el proyecto sirve como plantilla. Puedes copiarlo como `appsettings.Development.json` y poner ahí tu configuración local (este archivo está en .gitignore y no se sube a GitHub).

### 4. Ejecutar el proyecto

**Opción A — Visual Studio 2022:**
- Abre `PryCafeteria.sln`
- Presiona `F5` o el botón ▶ Run

**Opción B — Terminal:**
```bash
cd PryCafeteria/PryCafeteria
dotnet run
```

La app estará disponible en `https://localhost:7238` (el puerto puede variar, revisa la consola).

> **La base de datos se crea automáticamente al ejecutar la app por primera vez.** No necesitas correr ningún script SQL ni `dotnet ef database update` — el sistema aplica las migraciones solo.

### 6. Primer acceso

Al ejecutar por primera vez, el sistema crea automáticamente el usuario admin:

```
Email:      admin@gmail.com
Contraseña: admin123
Rol:        Admin
```

---

## ❗ Solución de problemas comunes

### Error: "A network-related error occurred"
El servidor SQL no se encontró. Verifica que:
- SQL Server esté corriendo (busca "SQL Server" en los Servicios de Windows)
- El nombre del servidor en `appsettings.json` sea correcto

### Error: "Cannot open database BDCAFETERIA"
La base de datos no existe. Ejecuta `dotnet ef database update`

### Error: "Login failed for user"
Estás usando autenticación de usuario y contraseña en vez de Windows Authentication. La cadena de conexión incluida usa `Integrated Security=True` que usa tu usuario de Windows — no necesitas usuario/contraseña de SQL Server.

### Error al ejecutar `dotnet ef`
Instala las herramientas de EF:
```bash
dotnet tool install --global dotnet-ef --version 8.*
```

---

## 📊 Estructura del Proyecto

```
PryCafeteria/
├── Controllers/
│   ├── CuentasController.cs           # Login, registro, logout
│   ├── DashboardController.cs         # Panel administrativo
│   ├── UsuariosController.cs          # Gestión de usuarios (CRUD)
│   ├── ProductosController.cs         # CRUD productos
│   ├── CategoriasController.cs        # CRUD categorías
│   ├── TamaniosController.cs          # CRUD tamaños
│   ├── ProductosTamaniosController.cs # Precios y stock
│   ├── CuponesController.cs           # CRUD cupones
│   └── MetodosPagosController.cs      # Métodos de pago
├── Models/
│   ├── ApplicationUser.cs             # Usuario extendido con Identity
│   ├── BdcafeteriaContext.cs          # DbContext de EF Core
│   ├── CustomClaimsPrincipalFactory.cs # Claims con nombre del usuario
│   ├── ViewModels/
│   │   ├── LoginViewModel.cs
│   │   ├── RegistroViewModel.cs
│   │   └── UsuarioViewModel.cs
│   └── (modelos de BD: Categoria, Producto, Tamanio, etc.)
├── Migrations/
│   ├── 20260210012412_InitialWithIdentity.cs  # Migración inicial
│   └── 20260222013522_AgregarFechaRegistro.cs # Agrega FechaRegistro
├── Views/
│   ├── Cuentas/        # Login y registro
│   ├── Dashboard/      # Panel admin
│   ├── Usuarios/       # Gestión de usuarios
│   ├── Productos/
│   ├── Categorias/
│   ├── Tamanios/
│   ├── ProductosTamanios/
│   ├── Cupones/
│   ├── MetodosPagos/
│   └── Shared/
│       └── _Layout.cshtml  # Layout principal con navbar
├── wwwroot/
│   ├── css/            # site.css, dashboard.css, login-style.css
│   ├── js/             # login-script.js
│   └── images/productos/ # Imágenes por categoría
├── appsettings.json                    # Configuración (editar Server aquí)
├── appsettings.Development.json.example # Plantilla de configuración
└── Program.cs                          # Configuración y seeding inicial
```

---

## 🗄️ Modelo de Base de Datos

| Tabla | Descripción |
|---|---|
| **AspNetUsers** | Usuarios extendidos con Nombre, Apellido, FechaRegistro |
| **AspNetRoles** | Roles del sistema: Admin, Cliente |
| **AspNetUserRoles** | Relación usuario-rol (generada por Identity) |
| **AspNetUserClaims** | Claims adicionales por usuario (generada por Identity) |
| **AspNetUserLogins** | Logins externos OAuth (generada por Identity) |
| **AspNetUserTokens** | Tokens de seguridad (generada por Identity) |
| **AspNetRoleClaims** | Claims por rol (generada por Identity) |
| **__EFMigrationsHistory** | Historial de migraciones aplicadas (generada por EF Core) |
| **Categorias** | Categorías de productos |
| **Productos** | Catálogo de productos con imagen y disponibilidad |
| **Tamanios** | Tamaños disponibles (Pequeño, Mediano, Grande) |
| **ProductosTamanios** | Precios y stock por combinación producto+tamaño |
| **Pedidos** | Órdenes de compra con estado y total |
| **DetallePedido** | Items individuales de cada pedido |
| **Cupones** | Descuentos por porcentaje o monto fijo |
| **MetodosPago** | Métodos de pago disponibles |
| **DireccionesEntrega** | Direcciones registradas por los clientes |

> La base de datos se crea completamente desde cero con `dotnet ef database update`. No se necesita ningún script SQL adicional.

---

## 🎨 Paleta de Colores

| Color | Hex |
|---|---|
| Verde Starbucks | #00704A |
| Verde Oscuro | #005238 |
| Crema / Beige | #D4AF77 |
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
