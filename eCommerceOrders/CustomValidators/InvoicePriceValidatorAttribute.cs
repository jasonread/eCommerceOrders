using eCommerceOrders.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace eCommerceOrders.CustomValidators
{
    public class InvoicePriceValidatorAttribute : ValidationAttribute
    {
        public string DefaultErrorMessage { get; set; } = "Invoice Price should be equal to the total cost of all products in the order (e.g. {0}).";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                PropertyInfo? ProductsProperty = validationContext.ObjectType.GetProperty(nameof(Order.Products));
                if (ProductsProperty != null)
                {
                    List<Product> products = (List<Product>)ProductsProperty.GetValue(validationContext.ObjectInstance)!;

                    double totalPrice = 0;
                    foreach (Product product in products)
                    {
                        totalPrice += (product.Price * product.Quantity);
                    }

                    double actualPrice = (double)value;

                    if (totalPrice > 0)
                    {
                        if (totalPrice != actualPrice)
                        {
                            return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, totalPrice), new string[] { nameof(validationContext.MemberName) });
                        }
                    }
                    else
                    {
                        return new ValidationResult("No products found to validate invoice price", new string[] { nameof(validationContext.MemberName) });
                    }

                    return ValidationResult.Success;
                }

                return null;
            }

            return null;
        }
    }
}
