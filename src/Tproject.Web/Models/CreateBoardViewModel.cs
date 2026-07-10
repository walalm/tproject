using System.ComponentModel.DataAnnotations;

namespace Tproject.Web.Models;

public class CreateBoardViewModel
{
    public int ProjectId { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
}
