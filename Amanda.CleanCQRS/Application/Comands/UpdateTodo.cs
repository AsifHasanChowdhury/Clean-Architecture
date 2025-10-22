// Application/Todos/Commands/UpdateTodo.cs
using Application.Abstractions;
using Domain;
using Domain.Exceptions;

namespace Application.Todos.Commands;
public sealed record UpdateTodoCommand(Guid Id, string Title, bool IsDone);

public sealed class UpdateTodoHandler(ITodoReadDb rdb, ITodoWriteDb wdb)
{
    public async Task Handle(UpdateTodoCommand cmd, CancellationToken ct)
    {
        var todo = await rdb.GetByIdAsync(cmd.Id, ct) ?? throw new NotFoundException($"Todo {cmd.Id} not found.");
        todo.Rename(cmd.Title);
        todo.Mark(cmd.IsDone);
        await wdb.UpdateAsync(todo, ct);
    }
}
