using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackBuldTechnicalAssessment.Services;

namespace StackBuldTechnicalAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public PaymentController(ApplicationDbContext context) 
        { 
            this.context = context;
        }


        [HttpPut("{orderid}/confirm-payment")]
        public IActionResult ConfirmPayment(int orderid)
        {
            var order = context.Orders.Find(orderid);
            if (order == null)
            {
                return NotFound($"Order with id {orderid} not found.");
            }

            if (order.IsPaid)
            {
                return BadRequest("Payment has been made for this Order.");
            }

           
            order.IsPaid = true;
            order.OrderStatusId = 2; 

            context.Orders.Update(order);
            context.SaveChanges();

            return Ok(new { Message = "Payment confirmed", OrderId = order.Id });
        }

    }
}
