using TodoApi.Models;

namespace TodoApi.Services;

/// <summary>
/// Implementación del servicio de gestión de tareas (en memoria)
/// </summary>
public class TodoService : ITodoService
{
    private readonly List<TodoItem> _items = new();
    private int _nextId = 1;

    public Task<IEnumerable<TodoItem>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<TodoItem>>(_items.ToList());
    }

    public Task<TodoItem?> GetByIdAsync(int id)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);
        return Task.FromResult(item);
    }

    public Task<TodoItem> CreateAsync(TodoItem item)
    {
        if (string.IsNullOrWhiteSpace(item.Title))
        {
            throw new ArgumentException("El título no puede estar vacío", nameof(item.Title));
        }

        item.Id = _nextId++;
        item.CreatedAt = DateTime.UtcNow;
        item.IsCompleted = false;
        
        _items.Add(item);
        
        return Task.FromResult(item);
    }

    public Task<TodoItem?> UpdateAsync(int id, TodoItem item)
    {
        var existing = _items.FirstOrDefault(x => x.Id == id);
        if (existing == null)
        {
            return Task.FromResult<TodoItem?>(null);
        }

        if (string.IsNullOrWhiteSpace(item.Title))
        {
            throw new ArgumentException("El título no puede estar vacío", nameof(item.Title));
        }

        existing.Title = item.Title;
        existing.Description = item.Description;
        existing.IsCompleted = item.IsCompleted;
        existing.UpdatedAt = DateTime.UtcNow;

        return Task.FromResult<TodoItem?>(existing);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);
        if (item == null)
        {
            return Task.FromResult(false);
        }

        _items.Remove(item);
        return Task.FromResult(true);
    }

    public Task<TodoItem?> CompleteAsync(int id)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);
        if (item == null)
        {
            return Task.FromResult<TodoItem?>(null);
        }

        item.IsCompleted = true;
        item.UpdatedAt = DateTime.UtcNow;

        return Task.FromResult<TodoItem?>(item);
    }
}
