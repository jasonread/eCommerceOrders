using eCommerceOrders.CustomValidators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace eCommerceOrders.Models
{
    public class Order
    {
        [Display(Name = "Order Number")]
        public int? OrderNo { get; set; }
        
        [Display(Name = "Order Date")]
        [Required(ErrorMessage = "Order date should be greater or equal to 2000")]
        [MinimumDateValidator("2000-01-01", ErrorMessage = "Order date must be greater than or equal to 2000-01-01")]
        public DateTime OrderDate { get; set; }
        
        [Display(Name = "Invoice Price")]
        [InvoicePriceValidator]
        public double InvoicePrice { get; set; }
        
        [Required]
        [MinLength(1)]
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
