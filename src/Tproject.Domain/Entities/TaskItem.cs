namespace Tproject.Domain.Entities;

public class TaskItem
{
    public int Id { get; set; }
    public int ColumnId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Position { get; set; }

    public Column Column { get; set; } = null!;
}
