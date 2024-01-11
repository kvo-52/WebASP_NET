using Microsoft.AspNetCore.Mvc;
using WebApp1_Product.Models;

namespace WebApp1_Product.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet("getProduct")]
        public IActionResult GetProducts()
        {
            try
            {
                using (var context = new StoreContext())
                {
                    var products = context.Products.Select(x => new Product()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description
                    });
                    return Ok(products);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("addProduct")]
        public IActionResult AddProduct([FromQuery] string name, string description, int groupId, int price)
        {
            try
            {
                using (var context = new StoreContext())
                {
                    if (!context.Products.Any(x => x.Name.ToLower().Equals(name)))
                    {
                        context.Add(new Product()
                        {
                            Name = name,
                            Description = description,
                            Price = price,
                            GroupID = groupId
                        });
                        context.SaveChanges();
                        return Ok();
                    }

                    return StatusCode(409);

                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("deleteProduct")]
        public IActionResult DeleteProduct([FromQuery] int id)
        {
            try
            {
                using (var context = new StoreContext())
                {
                    if (!context.Products.Any(x => x.Id == id))
                    {
                        return NotFound();
                    }

                    Product product = context.Products.FirstOrDefault(x => x.Id == id)!;
                    context.Products.Remove(product);
                    context.SaveChanges();

                    return Ok();

                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("addProductPrice")]
        public IActionResult AddProductPrice([FromQuery] int id, int price)
        {
            try
            {
                using (var context = new StoreContext())
                {
                    if (!context.Products.Any(x => x.Id == id))
                    {
                        return NotFound();
                    }

                    Product product = context.Products.FirstOrDefault(x => x.Id == id)!;
                    product.Price = price;
                    context.SaveChanges();

                    return Ok();
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
