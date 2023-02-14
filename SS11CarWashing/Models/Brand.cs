using System.ComponentModel.DataAnnotations;

namespace SS11CarWashing.Models;

public class Brand
{
    [Key]
    public Guid BrandId { get; set; }
    [Required]
    [MaxLength(50)]
    [Display(Name ="Brand Name")]
    public string BrandName { get; set; }
}
