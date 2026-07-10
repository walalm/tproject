using System.ComponentModel.DataAnnotations;

namespace Tproject.Web.Models;

public class CreateTaskViewModel
{
    public int BoardId { get; set; }
    public int ColumnId { get; set; }

    [Required]
    [StringLength(300)]
    public string Title { get; set; } = string.Empty;

    [StringLength(2000)]
    public string? Description { get; set; }
}
