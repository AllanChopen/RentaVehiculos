# RentaVehiculos

RentaVehiculos es una API REST construida con ASP.NET Core y Entity Framework Core para gestionar el proceso de alquiler de vehículos. El sistema modela y expone operaciones CRUD para vehículos, clientes, pagos y alquileres, además de lógica de negocio para calcular totales y devolver información enriquecida mediante DTOs. La persistencia se realiza en PostgreSQL (conectada mediante Npgsql), y el proyecto incluye Swagger para exploración de la API.

## Tecnologías y componentes principales
- ASP.NET Core Web API para exponer endpoints HTTP.
- Entity Framework Core (EF Core) como ORM para acceso a datos.
- PostgreSQL como base de datos (conexión Npgsql).
- Swagger/OpenAPI para documentación y prueba interactiva de endpoints.
- Dockerfile para contenedorización del servicio.

## Modelos de dominio
- Vehicle (`Models/Vehicle.cs`): representa un vehículo con atributos como `Plate`, `Model`, `Status` y `PricePerDay`.
- Cliente (`Models/Cliente.cs`): representa un cliente con `Nombre`, `Email` y `Telefono`.
- Alquiler (`Models/Alquiler.cs`): vincula un `Cliente` y un `Vehicle`, con `FechaInicio`, `FechaFin` y `Total`.
- Pago (`Models/Pago.cs`): asociado a un `Alquiler`, con `Monto`, `FechaPago` y `MetodoPago`.

Las relaciones principales:
- Un `Alquiler` referencia a un `Cliente` y a un `Vehicle`.
- Un `Pago` referencia a un `Alquiler`.

## Contexto de datos
`ApplicationDbContext` (en `Data/`) define los conjuntos (`DbSet`) para `Vehicles`, `Clientes`, `Alquileres` y `Pagos`, habilitando consultas y persistencia de estas entidades mediante EF Core.

## Controladores y responsabilidades
- `VehiclesController` (`Controllers/VehiclesController.cs`):
  - Lista todos los vehículos.
  - Recupera un vehículo por `id`.
  - Crea, actualiza y elimina vehículos.
- `ClientesController` (`Controllers/ClientesController.cs`):
  - Lista todos los clientes.
  - Recupera un cliente por `id`.
  - Crea, actualiza y elimina clientes.
- `PagosController` (`Controllers/PagosController.cs`):
  - Lista todos los pagos.
  - Recupera un pago por `id`.
  - Crea, actualiza y elimina pagos.
- `AlquileresController` (`Controllers/AlquileresController.cs`):
  - Lista alquileres incluyendo datos del cliente y del vehículo a través de un DTO (`AlquilerDto`), exponiendo campos como `ClienteNombre` y `VehiculoModelo`.
  - Recupera un alquiler por `id` con información enriquecida.
  - Crea alquileres a partir de un DTO de entrada (`CreateAlquilerDto`), validando la existencia de `Cliente` y `Vehiculo`, y calculando el `Total` como `PricePerDay * días`, donde los días se calculan de forma inclusiva entre `FechaInicio` y `FechaFin`.
  - Actualiza y elimina alquileres.

## DTOs
- `AlquilerDto` (en `Dtos/`): proyección de `Alquiler` que agrega datos del cliente y del vehículo (por ejemplo, `ClienteNombre`, `VehiculoModelo`), además de `FechaInicio`, `FechaFin` y `Total`.
- `CreateAlquilerDto` (en `Dtos/`): estructura de entrada para crear un alquiler, con `ClienteId`, `VehiculoId`, `FechaInicio` y `FechaFin`.

## Configuración de la aplicación
- `Program.cs` configura:
  - La conexión a PostgreSQL mediante Npgsql y registra `ApplicationDbContext`.
  - Los servicios de controladores y Swagger.
  - El pipeline HTTP con `UseHttpsRedirection`, `UseSwagger` y `UseSwaggerUI` en entorno de desarrollo.
- `appsettings.json` y `appsettings.Development.json` contienen configuración de la aplicación; la cadena de conexión a la base de datos se declara en el código de inicio.

## Contenedorización
- `Dockerfile` define los pasos para construir y ejecutar la API en un contenedor, facilitando despliegue y portabilidad.

## Organización del código
- `Controllers/`: controladores de la API.
- `Models/`: entidades del dominio.
- `Dtos/`: objetos de transferencia de datos para respuestas y entradas específicas.
- `Data/`: contexto de EF Core y configuración de acceso a datos.
- Archivos de configuración: `Program.cs`, `Renta.csproj`, `appsettings*.json`.
- Utilidades de desarrollo: Swagger, solución `.sln`, y archivos de registro de ejecución (`run*.log`).

En conjunto, el proyecto proporciona un backend completo para la gestión de alquileres de vehículos, cubriendo el ciclo de vida de clientes, vehículos, reservas y pagos, con reglas de cálculo del monto total del alquiler y respuestas enriquecidas para consumo por clientes o interfaces frontend.
