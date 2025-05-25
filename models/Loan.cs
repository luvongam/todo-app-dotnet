
using System.ComponentModel.DataAnnotations;
namespace LoanApp.Models;

public class Loan
{
    [key]
    public long Id { get; set; }

    [Required]
    public string BorrowerName { get; set; } = string.Empty;

    [Required]
    [Range(1000, 1000000)]
    public decimal Amount { get; set; }

    [Required]
    [Range(0.01, 20.0)]
    public decimal InterestRate { get; set; }


    public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;


    public String Status { get; set; } = "Pending";
}