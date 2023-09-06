using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Fileuloding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        [Route("/home")]
        [HttpPost]
        public IActionResult Index(IFormFile file)
        {
            try
            {
                var folderName = Path.Combine("Images", "Userprofile");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string renameFile = Convert.ToString(Guid.NewGuid()) + "." + fileName.Split('.').Last();
                    var fullPath = Path.Combine(pathToSave, renameFile);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { renameFile });
                }
                else
                {
                    return BadRequest();
                }
            }catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
