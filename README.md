# 📝 Todo API - API REST para Gestión de Tareas

API REST completa desarrollada con ASP.NET Core que demuestra las mejores prácticas de desarrollo de APIs y testing.

## 🎯 Características

- ✅ CRUD completo de tareas (Create, Read, Update, Delete)
- ✅ Arquitectura limpia con separación de responsabilidades
- ✅ Inyección de dependencias
- ✅ Documentación automática con OpenAPI/Swagger
- ✅ Tests unitarios y de integración
- ✅ Logging estructurado
- ✅ Manejo de errores apropiado

## 🚀 Inicio Rápido

### Prerrequisitos
- .NET 10.0 SDK o superior
- Editor de código (VS Code, Visual Studio, Rider)

### Ejecutar la API

```bash
# Clonar y navegar al directorio
cd dotnet-api

# Restaurar dependencias
dotnet restore

# Ejecutar la aplicación
dotnet run --project src/TodoApi/TodoApi.csproj
```

La API estará disponible en:
- HTTPS: `https://localhost:5001`
- HTTP: `http://localhost:5000`

### Explorar la API

Abre tu navegador en `https://localhost:5001` para acceder a la interfaz Swagger y probar los endpoints interactivamente.

## 📚 Endpoints de la API

### Obtener todas las tareas
```http
GET /api/todo
```

**Respuesta 200 OK:**
```json
[
  {
    "id": 1,
    "title": "Comprar leche",
    "description": "Ir al supermercado",
    "isCompleted": false,
    "createdAt": "2026-02-05T14:30:00Z",
    "updatedAt": null
  }
]
```

### Obtener una tarea por ID
```http
GET /api/todo/{id}
```

**Respuesta 200 OK:**
```json
{
  "id": 1,
  "title": "Comprar leche",
  "description": "Ir al supermercado",
  "isCompleted": false,
  "createdAt": "2026-02-05T14:30:00Z",
  "updatedAt": null
}
```

**Respuesta 404 Not Found:**
```json
{
  "message": "Tarea con ID 999 no encontrada"
}
```

### Crear una nueva tarea
```http
POST /api/todo
Content-Type: application/json

{
  "title": "Nueva tarea",
  "description": "Descripción opcional"
}
```

**Respuesta 201 Created:**
```json
{
  "id": 2,
  "title": "Nueva tarea",
  "description": "Descripción opcional",
  "isCompleted": false,
  "createdAt": "2026-02-05T14:35:00Z",
  "updatedAt": null
}
```

### Actualizar una tarea
```http
PUT /api/todo/{id}
Content-Type: application/json

{
  "title": "Tarea actualizada",
  "description": "Nueva descripción",
  "isCompleted": true
}
```

**Respuesta 200 OK:**
```json
{
  "id": 1,
  "title": "Tarea actualizada",
  "description": "Nueva descripción",
  "isCompleted": true,
  "createdAt": "2026-02-05T14:30:00Z",
  "updatedAt": "2026-02-05T14:40:00Z"
}
```

### Marcar tarea como completada
```http
PATCH /api/todo/{id}/complete
```

**Respuesta 200 OK:**
```json
{
  "id": 1,
  "title": "Comprar leche",
  "isCompleted": true,
  "updatedAt": "2026-02-05T14:45:00Z"
}
```

### Eliminar una tarea
```http
DELETE /api/todo/{id}
```

**Respuesta 204 No Content** (sin cuerpo)

## 🧪 Tests

El proyecto incluye 24 tests que cubren:

### Tests Unitarios (14 tests)
Ubicados en `tests/TodoApi.Tests/Services/TodoServiceTests.cs`

- ✅ Crear tareas con título válido
- ✅ Validar título obligatorio
- ✅ Obtener todas las tareas
- ✅ Obtener tarea por ID
- ✅ Actualizar tareas existentes
- ✅ Eliminar tareas
- ✅ Marcar como completada
- ✅ IDs secuenciales

### Tests de Integración (10 tests)
Ubicados en `tests/TodoApi.Tests/Integration/TodoApiIntegrationTests.cs`

- ✅ Flujo completo (crear, actualizar, completar, eliminar)
- ✅ Códigos de estado HTTP correctos
- ✅ Validación de datos
- ✅ Manejo de errores

### Ejecutar los Tests

```bash
# Todos los tests
dotnet test

# Con más detalle
dotnet test --logger "console;verbosity=detailed"

# Solo tests unitarios
dotnet test --filter "FullyQualifiedName~TodoServiceTests"

# Solo tests de integración
dotnet test --filter "FullyQualifiedName~TodoApiIntegrationTests"
```

## 🏗️ Arquitectura del Proyecto

```
dotnet-api/
├── src/
│   └── TodoApi/
│       ├── Controllers/
│       │   └── TodoController.cs      # Controlador REST
│       ├── Models/
│       │   └── TodoItem.cs            # Modelo de datos
│       ├── Services/
│       │   ├── ITodoService.cs        # Interfaz del servicio
│       │   └── TodoService.cs         # Implementación del servicio
│       └── Program.cs                  # Configuración y startup
└── tests/
    └── TodoApi.Tests/
        ├── Services/
        │   └── TodoServiceTests.cs     # Tests unitarios
        └── Integration/
            └── TodoApiIntegrationTests.cs  # Tests de integración
```

### Capas de la Aplicación

1. **Controllers**: Maneja las peticiones HTTP y las respuestas
2. **Services**: Contiene la lógica de negocio
3. **Models**: Define las entidades del dominio

## 🔧 Tecnologías Utilizadas

- **ASP.NET Core 10.0** - Framework web
- **xUnit** - Framework de testing
- **FluentAssertions** - Assertions expresivas para tests
- **Microsoft.AspNetCore.Mvc.Testing** - Testing de integración

## 📖 Guía de Desarrollo

### Agregar un Nuevo Endpoint

1. Agregar método en `ITodoService.cs`
```csharp
Task<IEnumerable<TodoItem>> GetCompletedAsync();
```

2. Implementar en `TodoService.cs`
```csharp
public Task<IEnumerable<TodoItem>> GetCompletedAsync()
{
    var completed = _items.Where(x => x.IsCompleted).ToList();
    return Task.FromResult<IEnumerable<TodoItem>>(completed);
}
```

3. Agregar acción en `TodoController.cs`
```csharp
[HttpGet("completed")]
public async Task<ActionResult<IEnumerable<TodoItem>>> GetCompleted()
{
    var items = await _todoService.GetCompletedAsync();
    return Ok(items);
}
```

4. Crear tests
```csharp
[Fact]
public async Task GetCompletedAsync_RetornaTodasLasCompletadas()
{
    // Arrange, Act, Assert...
}
```

## 🎓 Conceptos de Aprendizaje

Este proyecto demuestra:

### 1. **Principios SOLID**
- **S**ingle Responsibility: Cada clase tiene una responsabilidad única
- **D**ependency Inversion: Uso de interfaces y DI

### 2. **Clean Architecture**
- Separación de capas (Controllers, Services, Models)
- Independencia de frameworks en la lógica de negocio

### 3. **RESTful API Best Practices**
- Uso correcto de verbos HTTP (GET, POST, PUT, PATCH, DELETE)
- Códigos de estado apropiados
- Rutas semánticas

### 4. **Testing**
- Pirámide de testing (unitarios + integración)
- Uso de mocks e inyección de dependencias
- Tests de integración con servidor en memoria

### 5. **Logging**
- Logging estructurado
- Diferentes niveles (Information, Warning, Error)

## 🤝 Contribuir

Para contribuir al proyecto:

1. Seguir el estilo de código existente
2. Agregar tests para nueva funcionalidad
3. Actualizar la documentación
4. Verificar que todos los tests pasan

## 📝 Ejercicios Propuestos

1. **Agregar filtrado**: Implementar `GET /api/todo?completed=true`
2. **Agregar paginación**: Implementar `GET /api/todo?page=1&pageSize=10`
3. **Agregar búsqueda**: Implementar `GET /api/todo/search?query=leche`
4. **Agregar prioridad**: Agregar campo `priority` al modelo
5. **Agregar fechas de vencimiento**: Agregar `dueDate` y lógica de vencimiento

## 🐛 Troubleshooting

### El puerto ya está en uso
```bash
# Cambiar el puerto en launchSettings.json
# O matar el proceso que usa el puerto
lsof -ti:5001 | xargs kill -9
```

### Error de certificado HTTPS en desarrollo
```bash
dotnet dev-certs https --trust
```

## 📚 Recursos Adicionales

- [Documentación oficial ASP.NET Core](https://docs.microsoft.com/aspnet/core/)
- [Tutorial de API REST](https://docs.microsoft.com/aspnet/core/tutorials/first-web-api)
- [Mejores prácticas de API](https://docs.microsoft.com/azure/architecture/best-practices/api-design)

---

🔙 **[Volver al README principal](../README.md)**
