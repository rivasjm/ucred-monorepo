using FluentAssertions;
using TodoApi.Models;
using TodoApi.Services;
using Xunit;

namespace TodoApi.Tests.Services;

/// <summary>
/// Pruebas unitarias del servicio TodoService
/// </summary>
public class TodoServiceTests
{
    [Fact]
    public async Task GetAllAsync_SinTareas_RetornaListaVacia()
    {
        // Arrange
        var service = new TodoService();

        // Act
        var result = await service.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task CreateAsync_ConTituloValido_CreaTarea()
    {
        // Arrange
        var service = new TodoService();
        var newItem = new TodoItem { Title = "Comprar leche" };

        // Act
        var created = await service.CreateAsync(newItem);

        // Assert
        created.Should().NotBeNull();
        created.Id.Should().BeGreaterThan(0);
        created.Title.Should().Be("Comprar leche");
        created.IsCompleted.Should().BeFalse();
        created.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public async Task CreateAsync_ConTituloInvalido_LanzaExcepcion(string? titulo)
    {
        // Arrange
        var service = new TodoService();
        var newItem = new TodoItem { Title = titulo! };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(newItem));
    }

    [Fact]
    public async Task GetByIdAsync_ConIdExistente_RetornaTarea()
    {
        // Arrange
        var service = new TodoService();
        var created = await service.CreateAsync(new TodoItem { Title = "Tarea 1" });

        // Act
        var result = await service.GetByIdAsync(created.Id);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(created.Id);
        result.Title.Should().Be("Tarea 1");
    }

    [Fact]
    public async Task GetByIdAsync_ConIdInexistente_RetornaNull()
    {
        // Arrange
        var service = new TodoService();

        // Act
        var result = await service.GetByIdAsync(999);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_ConIdExistente_ActualizaTarea()
    {
        // Arrange
        var service = new TodoService();
        var created = await service.CreateAsync(new TodoItem { Title = "Tarea Original" });
        var updateData = new TodoItem 
        { 
            Title = "Tarea Actualizada",
            Description = "Nueva descripción"
        };

        // Act
        var updated = await service.UpdateAsync(created.Id, updateData);

        // Assert
        updated.Should().NotBeNull();
        updated!.Title.Should().Be("Tarea Actualizada");
        updated.Description.Should().Be("Nueva descripción");
        updated.UpdatedAt.Should().NotBeNull();
        updated.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public async Task UpdateAsync_ConIdInexistente_RetornaNull()
    {
        // Arrange
        var service = new TodoService();
        var updateData = new TodoItem { Title = "Tarea Actualizada" };

        // Act
        var result = await service.UpdateAsync(999, updateData);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task DeleteAsync_ConIdExistente_EliminaTarea()
    {
        // Arrange
        var service = new TodoService();
        var created = await service.CreateAsync(new TodoItem { Title = "Tarea a eliminar" });

        // Act
        var deleted = await service.DeleteAsync(created.Id);

        // Assert
        deleted.Should().BeTrue();
        var result = await service.GetByIdAsync(created.Id);
        result.Should().BeNull();
    }

    [Fact]
    public async Task DeleteAsync_ConIdInexistente_RetornaFalse()
    {
        // Arrange
        var service = new TodoService();

        // Act
        var result = await service.DeleteAsync(999);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task CompleteAsync_ConIdExistente_MarcaComoCompletada()
    {
        // Arrange
        var service = new TodoService();
        var created = await service.CreateAsync(new TodoItem { Title = "Tarea pendiente" });

        // Act
        var completed = await service.CompleteAsync(created.Id);

        // Assert
        completed.Should().NotBeNull();
        completed!.IsCompleted.Should().BeTrue();
        completed.UpdatedAt.Should().NotBeNull();
    }

    [Fact]
    public async Task CompleteAsync_ConIdInexistente_RetornaNull()
    {
        // Arrange
        var service = new TodoService();

        // Act
        var result = await service.CompleteAsync(999);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetAllAsync_ConVariasTareas_RetornaTodasLasTareas()
    {
        // Arrange
        var service = new TodoService();
        await service.CreateAsync(new TodoItem { Title = "Tarea 1" });
        await service.CreateAsync(new TodoItem { Title = "Tarea 2" });
        await service.CreateAsync(new TodoItem { Title = "Tarea 3" });

        // Act
        var result = await service.GetAllAsync();

        // Assert
        result.Should().HaveCount(3);
        result.Should().Contain(t => t.Title == "Tarea 1");
        result.Should().Contain(t => t.Title == "Tarea 2");
        result.Should().Contain(t => t.Title == "Tarea 3");
    }

    [Fact]
    public async Task CreateAsync_IdsSecuenciales_AsignaIdsCorrectamente()
    {
        // Arrange
        var service = new TodoService();

        // Act
        var item1 = await service.CreateAsync(new TodoItem { Title = "Tarea 1" });
        var item2 = await service.CreateAsync(new TodoItem { Title = "Tarea 2" });
        var item3 = await service.CreateAsync(new TodoItem { Title = "Tarea 3" });

        // Assert
        item1.Id.Should().Be(1);
        item2.Id.Should().Be(2);
        item3.Id.Should().Be(3);
    }
}
