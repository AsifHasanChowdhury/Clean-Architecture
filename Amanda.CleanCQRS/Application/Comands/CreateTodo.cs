// Application/Todos/Commands/CreateTodo.cs
using Application.Abstractions;
using Domain;
using Domain.Entity;

namespace Application.Todos.Commands;
public sealed record CreateTodoCommand(string Title);

public sealed class CreateTodoHandler(ITodoWriteDb db)
{
    public async Task<Guid> Handle(CreateTodoCommand cmd, CancellationToken ct)
    {
        var todo = new TodoItem(Guid.NewGuid(), cmd.Title);
        await db.CreateAsync(todo, ct);
        return todo.Id;
    }
}
