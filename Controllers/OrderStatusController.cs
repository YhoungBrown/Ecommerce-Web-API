using Microsoft.AspNetCore.Mvc;
using StackBuldTechnicalAssessment.Services;

namespace StackBuldTechnicalAssessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class OrderStatusController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public OrderStatusController(ApplicationDbContext context) 
        { 
            this.context = context;
        }


        [HttpGet]
        public IActionResult GetOrderStatus()
        {
            var orderstatus = context.OrderStatuses.ToList();

            return Ok(orderstatus);
        }
    }
}
