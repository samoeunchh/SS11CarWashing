using System.ComponentModel.DataAnnotations;

namespace SS11CarWashing.Models
{
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; }
        [MaxLength(50)]
        [Display(Name ="Customer Name")]
        public string CustomerName { get; set; }
        [MaxLength(20)]
        [Required]
        [Phone]
        [Display(Name ="Phone Number")]
        public string PhoneNumber { get; set; }
        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }


    }
}
