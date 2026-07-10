using System.ComponentModel.DataAnnotations;

namespace Tproject.Web.Models;

public class CreateProjectViewModel
{
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }
}
