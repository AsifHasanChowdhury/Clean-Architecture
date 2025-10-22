// Infrastructure/Todos/TodoReadDb.cs
using Application.Abstractions;
using Dapper;
using Domain;
using Domain.Entity;
using Infrastructure.Db;
using System.Data;

namespace Infrastructure.Todos;

public sealed class TodoReadDb(IConnectionFactory factory) : ITodoReadDb
{
    public async Task<TodoItem?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        using var conn = factory.Create();
        var cmd = new CommandDefinition(
            @"SELECT Id, Title, IsDone FROM dbo.Todos WHERE Id=@Id;",
            new { Id = id }, cancellationToken: ct);
        var row = await conn.QueryFirstOrDefaultAsync(cmd);
        if (row is null) return null;
        return new TodoItem((Guid)row.Id, (string)row.Title) { /* set IsDone via Mark */ };
    }

    public async Task<IReadOnlyList<TodoItem>> ListAsync(int page, int pageSize, CancellationToken ct)
    {
        using var conn = factory.Create();
        var cmd = new CommandDefinition(
            @"SELECT Id, Title, IsDone
              FROM dbo.Todos
              ORDER BY CreatedUtc DESC
              OFFSET (@Offset) ROWS FETCH NEXT (@Take) ROWS ONLY;",
            new { Offset = (page - 1) * pageSize, Take = pageSize }, cancellationToken: ct);

        var rows = await conn.QueryAsync(cmd);
        var list = new List<TodoItem>();
        foreach (var r in rows)
        {
            var todo = new TodoItem((Guid)r.Id, (string)r.Title);
            if ((bool)r.IsDone) todo.Mark(true);
            list.Add(todo);
        }
        return list;
    }
}
