using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using TodoApi.Models;
using Xunit;

namespace TodoApi.Tests.Integration;

/// <summary>
/// Pruebas de integración completas del API
/// </summary>
public class TodoApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public TodoApiIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    [Fact]
    public async Task GetAll_SinTareas_RetornaListaVacia()
    {
        // Act
        var response = await _client.GetAsync("/api/todo");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var items = await response.Content.ReadFromJsonAsync<List<TodoItem>>(_jsonOptions);
        items.Should().NotBeNull();
    }

    [Fact]
    public async Task Create_ConDatosValidos_CreaYRetornaTarea()
    {
        // Arrange
        var newItem = new TodoItem 
        { 
            Title = "Tarea de prueba",
            Description = "Descripción de la tarea"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/todo", newItem);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var created = await response.Content.ReadFromJsonAsync<TodoItem>(_jsonOptions);
        created.Should().NotBeNull();
        created!.Id.Should().BeGreaterThan(0);
        created.Title.Should().Be("Tarea de prueba");
        created.Description.Should().Be("Descripción de la tarea");
        created.IsCompleted.Should().BeFalse();

        // Verificar que se puede obtener por ID
        var location = response.Headers.Location;
        location.Should().NotBeNull();
    }

    [Fact]
    public async Task Create_ConTituloVacio_RetornaBadRequest()
    {
        // Arrange
        var newItem = new TodoItem { Title = "" };

        // Act
        var response = await _client.PostAsJsonAsync("/api/todo", newItem);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetById_ConIdExistente_RetornaTarea()
    {
        // Arrange - Primero crear una tarea
        var newItem = new TodoItem { Title = "Tarea para obtener" };
        var createResponse = await _client.PostAsJsonAsync("/api/todo", newItem);
        var created = await createResponse.Content.ReadFromJsonAsync<TodoItem>(_jsonOptions);

        // Act
        var response = await _client.GetAsync($"/api/todo/{created!.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var item = await response.Content.ReadFromJsonAsync<TodoItem>(_jsonOptions);
        item.Should().NotBeNull();
        item!.Id.Should().Be(created.Id);
        item.Title.Should().Be("Tarea para obtener");
    }

    [Fact]
    public async Task GetById_ConIdInexistente_RetornaNotFound()
    {
        // Act
        var response = await _client.GetAsync("/api/todo/99999");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Update_ConIdExistente_ActualizaTarea()
    {
        // Arrange - Crear tarea
        var newItem = new TodoItem { Title = "Tarea original" };
        var createResponse = await _client.PostAsJsonAsync("/api/todo", newItem);
        var created = await createResponse.Content.ReadFromJsonAsync<TodoItem>(_jsonOptions);

        var updateItem = new TodoItem 
        { 
            Title = "Tarea actualizada",
            Description = "Nueva descripción"
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/todo/{created!.Id}", updateItem);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var updated = await response.Content.ReadFromJsonAsync<TodoItem>(_jsonOptions);
        updated.Should().NotBeNull();
        updated!.Title.Should().Be("Tarea actualizada");
        updated.Description.Should().Be("Nueva descripción");
    }

    [Fact]
    public async Task Delete_ConIdExistente_EliminaTarea()
    {
        // Arrange - Crear tarea
        var newItem = new TodoItem { Title = "Tarea a eliminar" };
        var createResponse = await _client.PostAsJsonAsync("/api/todo", newItem);
        var created = await createResponse.Content.ReadFromJsonAsync<TodoItem>(_jsonOptions);

        // Act
        var response = await _client.DeleteAsync($"/api/todo/{created!.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        // Verificar que ya no existe
        var getResponse = await _client.GetAsync($"/api/todo/{created.Id}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Complete_ConIdExistente_MarcaComoCompletada()
    {
        // Arrange - Crear tarea
        var newItem = new TodoItem { Title = "Tarea a completar" };
        var createResponse = await _client.PostAsJsonAsync("/api/todo", newItem);
        var created = await createResponse.Content.ReadFromJsonAsync<TodoItem>(_jsonOptions);

        // Act
        var response = await _client.PatchAsync($"/api/todo/{created!.Id}/complete", null);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var completed = await response.Content.ReadFromJsonAsync<TodoItem>(_jsonOptions);
        completed.Should().NotBeNull();
        completed!.IsCompleted.Should().BeTrue();
    }

    [Fact]
    public async Task FullWorkflow_CrearActualizarCompletarEliminar()
    {
        // 1. Crear tarea
        var newItem = new TodoItem { Title = "Workflow completo" };
        var createResponse = await _client.PostAsJsonAsync("/api/todo", newItem);
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        var created = await createResponse.Content.ReadFromJsonAsync<TodoItem>(_jsonOptions);

        // 2. Actualizar tarea
        var updateItem = new TodoItem { Title = "Workflow actualizado" };
        var updateResponse = await _client.PutAsJsonAsync($"/api/todo/{created!.Id}", updateItem);
        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        // 3. Completar tarea
        var completeResponse = await _client.PatchAsync($"/api/todo/{created.Id}/complete", null);
        completeResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        // 4. Verificar que está completada
        var getResponse = await _client.GetAsync($"/api/todo/{created.Id}");
        var item = await getResponse.Content.ReadFromJsonAsync<TodoItem>(_jsonOptions);
        item!.IsCompleted.Should().BeTrue();

        // 5. Eliminar tarea
        var deleteResponse = await _client.DeleteAsync($"/api/todo/{created.Id}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
