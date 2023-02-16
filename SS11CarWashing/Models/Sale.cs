using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SS11CarWashing.Models;

public class Sale
{
    [Key]
    public Guid SaleId { get; set; }
    [DataType(DataType.Date)]
    [Display(Name = "Issue Date")]
    public DateTime IssueDate { get; set; }
    [MaxLength(20)]
    public string InvoiceNumber { get; set; }
    [ForeignKey("Customer")]
    [Display(Name ="Customer Name")]
    public Guid CustomerId { get; set; }
    public double Total { get; set; }
    public int Discount { get; set; }
    public int Vat { get; set; }
    public double GrandTotal { get; set; }

    public Customer Customer { get; set; }
}
