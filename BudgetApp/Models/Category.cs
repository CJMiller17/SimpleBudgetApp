using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetApp.Models;

public class Category
{
    [Key]
    public int CategoryId { get; init; }

    [Column(TypeName = "nvarchar(50)")]
    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; set; } = "";
    
    [Column(TypeName = "nvarchar(30)")]
    public string Icon { get; set; } = "";
    
    [Column(TypeName = "nvarchar(10)")]
    public string Type { get; set; } = "Expense";

    [NotMapped]
    public string? TitleWithIcon => this.Icon + " " + this.Title;

}