# ☕ Cafetería - Sistema de Gestión

Sistema de gestión para cafetería desarrollado con **ASP.NET Core 8.0 MVC** y **Entity Framework Core**.

## 🚀 Características

- ✅ Gestión completa de productos (CRUD)
- ✅ Categorización de productos (Bebidas calientes, frías, frappe, repostería, sándwiches)
- ✅ Sistema de tamaños y precios diferenciados
- ✅ Gestión de usuarios y roles
- ✅ Sistema de cupones de descuento
- ✅ Gestión de métodos de pago
- ✅ Control de stock por producto y tamaño
- ✅ Interfaz con diseño moderno

## 🛠️ Tecnologías Utilizadas

- **Framework:** ASP.NET Core 8.0 MVC
- **ORM:** Entity Framework Core
- **Base de Datos:** SQL Server
- **Frontend:** Razor Pages, Bootstrap 5, jQuery
- **Patrón:** MVC (Model-View-Controller)

## 📋 Requisitos Previos

- .NET 8.0 SDK
- SQL Server 2019+ o SQL Server Express
- Visual Studio 2022 (recomendado) o Visual Studio Code

## 🔧 Instalación

### 1. Clonar el repositorio
```bash
git clone https://github.com/TuUsuario/starbucks-cafeteria.git
cd starbucks-cafeteria
```

### 2. Crear la base de datos
Ejecuta el script SQL ubicado en la raíz del proyecto:
```bash
BDCAFETERIA.sql
```

### 3. Configurar la cadena de conexión
Edita el archivo `appsettings.json` y ajusta la conexión a tu servidor SQL:
```json
{
  "ConnectionStrings": {
    "BDCAFETERIAConn": "Server=TU_SERVIDOR;Database=BDCAFETERIA;Integrated Security=True;TrustServerCertificate=True;"
  }
}
```

### 4. Ejecutar el proyecto
```bash
cd PryCafeteria/PryCafeteria
dotnet run
```

La aplicación estará disponible en: `https://localhost:5001`

## 📊 Estructura del Proyecto

```
PryCafeteria/
├── Controllers/         # Controladores MVC
├── Models/             # Modelos de datos (Entity Framework)
├── Views/              # Vistas Razor
│   ├── Productos/
│   ├── Usuarios/
│   ├── Categorias/
│   └── ...
├── wwwroot/           # Archivos estáticos
│   ├── css/
│   ├── js/
│   └── images/        # Imágenes de productos
└── Program.cs         # Configuración de la aplicación
```

## 🗄️ Modelo de Base de Datos

El sistema incluye las siguientes tablas:

- **Usuarios** - Gestión de usuarios del sistema
- **Roles** - Roles (Admin, Cliente)
- **Categorias** - Categorías de productos
- **Productos** - Catálogo de productos
- **Tamanios** - Tamaños disponibles
- **ProductosTamanios** - Precios y stock por tamaño
- **MetodosPago** - Métodos de pago
- **DireccionesEntrega** - Direcciones de clientes
- **Pedidos** - Órdenes de compra
- **DetallePedido** - Items de cada pedido
- **Cupones** - Sistema de descuentos

## 👤 Usuario por Defecto

```
Email: admin@gmail.com
Contraseña: admin123
Rol: Admin
```

## 📦 Productos Incluidos

El sistema viene con **16 productos** precargados:

### Bebidas Calientes
- Café Americano
- Café Latte
- Café Cappuccino
- Café Mocha
- Té Tarragon

### Bebidas Frías
- Iced Latte
- Cold Brew
- Iced Americano

### Frappe
- Frappe de Café
- Frappe de Caramel
- Frappe de Chocolate

### Repostería
- Croissant
- Muffin de Arándano
- Cookie de Chocolate

### Sándwiches
- Sándwich de Pollo
- Sándwich de Jamón

## 🎨 Diseño

El proyecto utiliza una paleta de colores inspirada en Starbucks:

- **Verde Starbucks:** #00704A
- **Verde Oscuro:** #005238
- **Crema:** #D4AF77
- **Light:** #F0EBE0

## 📝 Funcionalidades Principales

### Gestión de Productos
- Crear, editar y eliminar productos
- Asignar categorías
- Cargar imágenes
- Control de disponibilidad

### Sistema de Tamaños
- Pequeño, Mediano, Grande
- Precios diferenciados por tamaño
- Control de stock por tamaño

### Gestión de Usuarios
- Crear usuarios
- Asignar roles
- Gestión de direcciones de entrega

### Sistema de Cupones
- Descuentos por porcentaje o monto fijo
- Control de vigencia por fechas
- Activación/desactivación

## 🚧 Próximas Mejoras

- [ ] Sistema de autenticación con ASP.NET Identity
- [ ] Carrito de compras
- [ ] Proceso completo de checkout
- [ ] Reportes y estadísticas
- [ ] API REST
- [ ] Sistema de notificaciones por email
- [ ] Búsqueda y filtros avanzados
- [ ] Paginación

## 📄 Licencia

Este proyecto es de código abierto y está disponible bajo la licencia MIT.

## 👨‍💻 Autor

Desarrollado por **AguirreNM**

---

⭐ Si te gusta este proyecto, ¡dale una estrella en GitHub!
