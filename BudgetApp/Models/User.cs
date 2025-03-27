using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetApp.Models;

public class User
{
    [Key]
    public int UserId { get; set; }
    
    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string Name { get; set; } = "";
    
    [Column(TypeName = "nvarchar(100)")]
    public string? ProfileImagePath { get; set; } = "profile.jpg";
    
}