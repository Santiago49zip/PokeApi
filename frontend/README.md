Arquitectura

El proyecto sigue una arquitectura por capas:

Controllers: Exponen los endpoints HTTP al frontend.

Services: Contienen la lógica de negocio, consumen el cliente HTTP y llaman al repositorio.

HttpClients: Consumo de la PokeAPI, manejo de requests y parsing.

Repositories: Acceso a la base de datos MySQL mediante Entity Framework Core.

Models:

Entities: Tablas persistidas en MySQL (Pokemon).

DTOs: Objetos de transferencia de datos hacia el frontend.

Diagrama simplificado:

Frontend Angular <--> Backend ASP.NET Core <--> PokeAPI
                                    |
                                    v
                                 MySQL

🔌 Endpoints
listado paginado
GET /api/pokemon?limit=20&offset=0


Retorna un listado de Pokémon con id y name.

Parámetros:

limit (opcional): número de Pokémon por página (default: 20)

offset (opcional): índice de inicio (default: 0)

Búsqueda por nombre
GET /api/pokemon?limit=20&offset=0&search=pikachu


Retorna Pokémon cuyo nombre coincida parcial o totalmente con search.

Listado desde base de datos
GET /api/pokemon/db


Retorna Pokémon previamente guardados en MySQL.

Evita duplicados usando el PokemonId de PokeAPI.

Paginación

Implementada en backend usando parámetros limit y offset.

Se refleja en la llamada a PokeAPI y en la respuesta final al frontend.

Botones Anterior/Siguiente controlan el offset y permiten navegación por páginas.

Persistencia

La tabla Pokemons se crea automáticamente al ejecutar el backend gracias a Entity Framework Core.

No es necesario ejecutar scripts manuales, ya que el método EnsureCreated() o las migraciones crean la tabla si no existe.

La entidad principal es Pokemon con las siguientes propiedades:

Campo	Tipo	Descripción
PokemonId	int	ID único del Pokémon (clave primaria)
Name	string	Nombre del Pokémon
CreatedAt	datetime	Fecha y hora en que se guardó el registro

Frontend Angular

Listado de Pokémon en cards minimalistas con estilo futurista (Pokédex).

Input de búsqueda con botón explícito.

Estados visuales:

Cargando…

Error

No hay Pokémon

Paginación con botones Anterior/Siguiente, deshabilitados cuando corresponde.

Tecnologías

Backend: ASP.NET Core (.NET 8)

Frontend: Angular 16 (Standalone Components)

Base de datos: MySQL

ORM: Entity Framework Core + Pomelo MySQL

API externa: PokeAPI

HTTP: HttpClient

Documentación: Swagger

▶️ Ejecución

Configurar la cadena de conexión MySQL en Program.cs:

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(
        "server=localhost;database=pokedb;user=root;password=admin",
        ServerVersion.AutoDetect("server=localhost;database=pokedb;user=root;password=admin")
    );
});


Levantar backend:
cd Backend
dotnet run


Levantar frontend:
cd frontend
npm install
ng serve --open


Angular correrá en http://localhost:4200

Backend correrá en http://localhost:5049

Pruebas

Consultar endpoints en Swagger: http://localhost:5049/swagger

Insomnia / Postman: GET /api/pokemon?limit=10&offset=0

Frontend: listar, buscar, paginar

 Mejoras implementadas

Persistencia en MySQL para evitar múltiples llamadas a PokeAPI.

Manejo de errores centralizado con middleware.

UI minimalista, futurista y responsive.

Standalone components en Angular.