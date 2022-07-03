using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendFiles.DTOs;

namespace SendFiles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpPost]
        public ActionResult AddProduct(ProductWriteDTO product)
        {
            return Ok(new { Status = "Success" });
        }
    }
}
