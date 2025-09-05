using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackBuldTechnicalAssessment.Dtos;
using StackBuldTechnicalAssessment.Models;
using StackBuldTechnicalAssessment.Services;
using System.Transactions;

namespace StackBuldTechnicalAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public OrdersController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpPost]
        public IActionResult CreateOrder([FromBody]CreateOrderParameterDto createOrderParameterDto)
        {

            using var transaction = context.Database.BeginTransaction();

            try
            {
                    var order = new Order
                    {
                        CustomerName = createOrderParameterDto.customerName,
                        CustomerEmail = createOrderParameterDto.customerEmail,
                        CustomerAddress = createOrderParameterDto.customerAddress,  
                        OrderItems = new()
                    };

                    foreach (var item in createOrderParameterDto.OrderItems)
                    {
                        var product = context.Products.Find(item.ProductId);
                        if (product == null)
                        { 
                            return NotFound($"Product with id: {item.ProductId} not Found");
                        }

                        if (product.StockQuantity < item.Quantity)
                        {
                            return BadRequest($"Product {product.Name} does not have enough stock available");
                        }

                        product.StockQuantity -= item.Quantity;

                        order.OrderItems.Add(new OrderItem
                        {
                            ProductId = product.Id,
                            Quantity = item.Quantity,
                            UnitPrice = product.Price
                        });

                        context.Products.Update(product);
                    }       

                    context.Orders.Add(order);
                    context.SaveChanges();

                foreach (var item in order.OrderItems)
                {
                    item.Order = null;
                }

                transaction.Commit();

                return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
                                 
                
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }


        [HttpGet]
        public IActionResult GetOrders() {

            var orders = context.Orders
               .Include(o => o.OrderItems)
               .ThenInclude(oi => oi.Product)
               .Include(o => o.OrderStatus)
               .ToList();

            foreach (var order in orders)
            {
                foreach (var item in order.OrderItems)
                {
                    item.Order = null;
                }
            }

            return Ok(orders);
        }


        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            var order = context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.OrderStatus)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return NotFound($"Order with id: {id} not found");
            }

            foreach (var item in order.OrderItems)
            {
              item.Order = null; 
            }

            return Ok(order);
        }



        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            
            if (order.OrderItems.Any())
            {
                context.OrderItems.RemoveRange(order.OrderItems);
            }

            context.Orders.Remove(order);
            context.SaveChanges();

            return Ok($"Product with id: {id} deleted successfully");
        }


    }
}
