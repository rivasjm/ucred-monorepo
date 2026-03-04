using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers;

/// <summary>
/// Controlador REST para gestionar tareas
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;
    private readonly ILogger<TodoController> _logger;

    public TodoController(ITodoService todoService, ILogger<TodoController> logger)
    {
        _todoService = todoService;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene todas las tareas
    /// </summary>
    /// <returns>Lista de tareas</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetAll()
    {
        _logger.LogInformation("Obteniendo todas las tareas");
        var items = await _todoService.GetAllAsync();
        return Ok(items);
    }

    /// <summary>
    /// Obtiene una tarea por su ID
    /// </summary>    /// <param name="id">ID de la tarea</param>
    /// <returns>La tarea encontrada</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TodoItem>> GetById(int id)
    {
        _logger.LogInformation("Obteniendo tarea con ID: {Id}", id);
        var item = await _todoService.GetByIdAsync(id);
        
        if (item == null)
        {
            _logger.LogWarning("Tarea con ID {Id} no encontrada", id);
            return NotFound(new { message = $"Tarea con ID {id} no encontrada" });
        }

        return Ok(item);
    }

    /// <summary>
    /// Crea una nueva tarea
    /// </summary>
    /// <param name="item">Datos de la tarea</param>
    /// <returns>La tarea creada</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TodoItem>> Create([FromBody] TodoItem item)
    {
        try
        {
            _logger.LogInformation("Creando nueva tarea: {Title}", item.Title);
            var created = await _todoService.CreateAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Error al crear tarea: {Message}", ex.Message);
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Actualiza una tarea existente
    /// </summary>
    /// <param name="id">ID de la tarea</param>
    /// <param name="item">Nuevos datos de la tarea</param>
    /// <returns>La tarea actualizada</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TodoItem>> Update(int id, [FromBody] TodoItem item)
    {
        try
        {
            _logger.LogInformation("Actualizando tarea con ID: {Id}", id);
            var updated = await _todoService.UpdateAsync(id, item);
            
            if (updated == null)
            {
                _logger.LogWarning("Tarea con ID {Id} no encontrada para actualizar", id);
                return NotFound(new { message = $"Tarea con ID {id} no encontrada" });
            }

            return Ok(updated);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Error al actualizar tarea: {Message}", ex.Message);
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Elimina una tarea
    /// </summary>
    /// <param name="id">ID de la tarea</param>
    /// <returns>Resultado de la operación</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Eliminando tarea con ID: {Id}", id);
        var deleted = await _todoService.DeleteAsync(id);
        
        if (!deleted)
        {
            _logger.LogWarning("Tarea con ID {Id} no encontrada para eliminar", id);
            return NotFound(new { message = $"Tarea con ID {id} no encontrada" });
        }

        return NoContent();
    }

    /// <summary>
    /// Marca una tarea como completada
    /// </summary>
    /// <param name="id">ID de la tarea</param>
    /// <returns>La tarea actualizada</returns>
    [HttpPatch("{id}/complete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TodoItem>> Complete(int id)
    {
        _logger.LogInformation("Marcando tarea {Id} como completada", id);
        var item = await _todoService.CompleteAsync(id);
        
        if (item == null)
        {
            _logger.LogWarning("Tarea con ID {Id} no encontrada para marcar como completada", id);
            return NotFound(new { message = $"Tarea con ID {id} no encontrada" });
        }

        return Ok(item);
    }
}
