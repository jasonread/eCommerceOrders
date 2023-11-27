using eCommerceOrders.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceOrders.Controllers
{
    public class OrderController : Controller
    {
        [Route("order")]
        [HttpPost]
        public IActionResult Index([Bind(nameof(Order.OrderDate), nameof(Order.InvoicePrice), nameof(Order.Products))] Order order)
        {
            if (ModelState.IsValid)
            {
                order.OrderNo = new Random().Next(0, 999999);
                return Json(new { order.OrderNo});
            }

            var errors = string.Join("\n", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return BadRequest(errors);

        }
    }
}
