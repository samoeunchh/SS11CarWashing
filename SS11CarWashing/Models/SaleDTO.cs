using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SS11CarWashing.Models;

public class SaleDTO
{
    [Key]
    public Guid SaleId { get; set; }
    [DataType(DataType.Date)]
    [Display(Name = "Issue Date")]
    public DateTime IssueDate { get; set; }
    [MaxLength(20)]
    public string InvoiceNumber { get; set; }
    [Display(Name = "Customer Name")]
    public string CustomerName { get; set; }
    public double Total { get; set; }
    public int Discount { get; set; }
    public int Vat { get; set; }
    public double GrandTotal { get; set; }

    public Customer Customer { get; set; }
    public List<SaleDetailDTO> SaleDetails { get; set; }
}

