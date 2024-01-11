using Microsoft.AspNetCore.Mvc;

using WebApp1_Product.Models;

namespace WebApp1_Product.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupController : ControllerBase
    {
        [HttpPost("addGroup")]
        public IActionResult AddGroup([FromQuery] string name)
        {
            try
            {
                using (var context = new StoreContext())
                {
                    if (!context.Groups.Any(x => x.Name.ToLower().Equals(name)))
                    {
                        context.Add(new Group()
                        {
                            Name = name

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

        [HttpDelete("deleteGroup")]
        public IActionResult DeleteGroup([FromQuery] int id)
        {
            try
            {
                using (var context = new StoreContext())
                {
                    if (!context.Groups.Any(x => x.Id == id))
                    {
                        return NotFound();
                    }

                    Group product = context.Groups.FirstOrDefault(x => x.Id == id)!;
                    context.Groups.Remove(product);
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
