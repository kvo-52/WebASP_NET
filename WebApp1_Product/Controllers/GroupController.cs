using Microsoft.AspNetCore.Mvc;
using WebApp1_Product.Abstraction;
using WebApp1_Product.Models;
using WebApp1_Product.Models.DTO;

namespace WebApp1_Product.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;

        public GroupController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        [HttpPost("addGroup")]
        public IActionResult AddGroup([FromBody] DTOGroup group)


        {
            var result = _groupRepository.AddGroup(group);
            return Ok(result);
        }

        [HttpGet("getGroups")]
        public IActionResult GetGroups()
        {
            var result = _groupRepository.GetGroups;
            return Ok(result);
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

        [HttpGet("GetGroupsCSV")]
        public FileContentResult GetCSV()
        {
            var result = _groupRepository.GetGroups;
            var content = _groupRepository.GetGroupsCSV();

            return File(new System.Text.UTF8Encoding().GetBytes(content), "text/csv", "Groups.csv");
        }

        [HttpGet("GetCacheCSV")]
        public ActionResult<string> GetCacheCSV()
        {
            string result = _groupRepository.GetСacheStatCSV();

            if (result is not null)
            {
                var fileName = $"groups{DateTime.Now.ToBinary()}.csv";

                System.IO.File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", fileName), result);

                return "https://" + Request.Host.ToString() + "/static/" + fileName;
            }
            return StatusCode(500);
        }
    }
}
