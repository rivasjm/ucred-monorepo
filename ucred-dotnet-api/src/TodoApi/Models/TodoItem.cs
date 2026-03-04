namespace TodoApi.Models;

/// <summary>
/// Representa una tarea en la lista de To-Do
/// </summary>
public class TodoItem
{
    /// <summary>
    /// Identificador único de la tarea
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Título de la tarea
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Descripción detallada de la tarea
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Indica si la tarea está completada
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// Fecha de creación de la tarea
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Fecha de última modificación
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}
