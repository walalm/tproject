namespace Tproject.Web.Models;

public class MoveTaskRequest
{
    public int TaskId { get; set; }
    public int ColumnId { get; set; }
    public int NewPosition { get; set; }
}
