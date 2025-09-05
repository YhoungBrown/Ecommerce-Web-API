using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackBuldTechnicalAssessment.Models;
using StackBuldTechnicalAssessment.Services;

namespace StackBuldTechnicalAssessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ProductsController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpPost]
        public IActionResult CreateProduct(ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                StockQuantity = productDto.StockQuantity,
            };

            context.Products.Add(product);
            context.SaveChanges();

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }


        [HttpGet]
        public IActionResult GetProducts(string? search, decimal? price, string? sort, string? order, int? page)
        {
            IQueryable<Product> query = context.Products;

            // Filtering
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.Name.Contains(search) || p.Description.Contains(search));
            }

            if (price.HasValue)
            {
                query = query.Where(p => p.Price == price);
            }

            // Sorting
            sort = sort?.ToLower() ?? "id";
            order = order?.ToLower() == "asc" ? "asc" : "desc";

            query = sort switch
            {
                "name" => order == "asc" ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name),
                "price" => order == "asc" ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price),
                "date" => order == "asc" ? query.OrderBy(p => p.CreatedAt) : query.OrderByDescending(p => p.CreatedAt),
                _ => order == "asc" ? query.OrderBy(p => p.Id) : query.OrderByDescending(p => p.Id),
            };

            // Pagination
            page ??= 1;
            int pageSize = 5;
            int totalItems = query.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var products = query.Skip(((int)page - 1) * pageSize).Take(pageSize).ToList();

            var response = new
            {
                Products = products,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
            };

            return Ok(response);
        }



        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
                return NotFound($"Product with id: {id} not found");
            }
            return Ok(product);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, ProductDto productDto)
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
                return NotFound($"Product with id: {id} not found");
            }

            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.StockQuantity = productDto.StockQuantity;

            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("The product was updated by someone else. Please reload and try again.");
            }

            return Ok(product);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = context.Products.Find(id);

            if (product == null)
            {
                return NotFound($"Product with id: {id} not found");
            }

            context.Products.Remove(product);
            context.SaveChanges();

            return Ok($"Product with id: {id} deleted successfully");
        }


    }
}
