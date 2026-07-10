using System.ComponentModel.DataAnnotations;

namespace Tproject.Web.Models;

public class CreateColumnViewModel
{
    public int BoardId { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
}
