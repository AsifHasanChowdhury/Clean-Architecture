using Application.Todos.Commands;
using Application.Todos.Queries;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTodoDto dto, [FromServices] CreateTodoHandler h, CancellationToken ct)
    {
        var id = await h.Handle(new CreateTodoCommand(dto.Title), ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpGet]
    public async Task<IActionResult> List([FromServices] ListTodosHandler h, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
        => Ok(await h.Handle(new ListTodosQuery(page, pageSize), ct));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, [FromServices] GetTodoByIdHandler h, CancellationToken ct)
        => Ok(await h.Handle(new GetTodoByIdQuery(id), ct));

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTodoDto dto, [FromServices] UpdateTodoHandler h, CancellationToken ct)
    {
        await h.Handle(new UpdateTodoCommand(id, dto.Title, dto.IsDone), ct);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, [FromServices] DeleteTodoHandler h, CancellationToken ct)
    {
        await h.Handle(new DeleteTodoCommand(id), ct);
        return NoContent();
    }
}

public sealed record CreateTodoDto(string Title);
public sealed record UpdateTodoDto(string Title, bool IsDone);
