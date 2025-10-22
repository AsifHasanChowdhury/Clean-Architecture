// Domain/TodoItem.cs
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity;
public sealed class TodoItem
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public bool IsDone { get; private set; }

    public TodoItem(Guid id, string title)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ValidationException("Title is required.");
        Id = id; Title = title.Trim(); IsDone = false;
    }

    public void Rename(string title)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ValidationException("Title is required.");
        Title = title.Trim();
    }

    public void Mark(bool done) => IsDone = done;
}
