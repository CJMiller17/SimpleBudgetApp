using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetApp.Models;

public class Transaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TransactionId { get; set; }
    
    [Range(1,int.MaxValue, ErrorMessage = "Please select a category.")]
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount should be greater than 0.")]
    [Column(TypeName = "decimal(10,2)")] // Number of digits before and after the decimal point
    public decimal Amount { get; set; }
    
    [Column(TypeName = "nvarchar(75)")]
    public string? Note { get; set; }
    
    public DateTime Date { get; set; } = DateTime.Now;
    
    [Required]
    public int UserId { get; set; }
    // public User? User { get; set; }

    [NotMapped]
    public string? CategoryTitleWithIcon => Category == null ? "" : Category.Icon + " " + Category.Title;
    
    [NotMapped]
    public string? FormattedAmount => ((Category == null || Category.Type == "Expense") ? "- " : "+ ") + Amount.ToString("C2");
    
}