using TodoApi.Models;

namespace TodoApi.Services;

/// <summary>
/// Interfaz del servicio para gestionar tareas
/// </summary>
public interface ITodoService
{
    /// <summary>
    /// Obtiene todas las tareas
    /// </summary>
    Task<IEnumerable<TodoItem>> GetAllAsync();

    /// <summary>
    /// Obtiene una tarea por su ID
    /// </summary>
    Task<TodoItem?> GetByIdAsync(int id);

    /// <summary>
    /// Crea una nueva tarea
    /// </summary>
    Task<TodoItem> CreateAsync(TodoItem item);

    /// <summary>
    /// Actualiza una tarea existente
    /// </summary>
    Task<TodoItem?> UpdateAsync(int id, TodoItem item);

    /// <summary>
    /// Elimina una tarea
    /// </summary>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Marca una tarea como completada
    /// </summary>
    Task<TodoItem?> CompleteAsync(int id);
}
