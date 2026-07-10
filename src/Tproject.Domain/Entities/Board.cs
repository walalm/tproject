namespace Tproject.Domain.Entities;

public class Board
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Position { get; set; }

    public Project Project { get; set; } = null!;
    public ICollection<Column> Columns { get; set; } = new List<Column>();
}
