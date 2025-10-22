// Application/Abstractions/ITodoReadDb.cs
using Domain;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Application.Abstractions;
public interface ITodoReadDb
{
    Task<TodoItem?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<TodoItem>> ListAsync(int page, int pageSize, CancellationToken ct);
}

