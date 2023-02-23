using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SS11CarWashing.Models;

	public class SaleDetailDTO
	{
    [Key]
    public Guid SaleDetailId { get; set; }
    [ForeignKey("Sale")]
    public Guid SaleId { get; set; }
    public string ItemName { get; set; }
    public double Price { get; set; }
    public int Qty { get; set; }
    public double Amount { get; set; }
}

