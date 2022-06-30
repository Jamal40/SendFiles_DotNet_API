using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace SendFiles.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilesController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        return Ok(new { Name = "Hello" });
    }

    [HttpPost("{id:int}")]
    public ActionResult UploadImage(int id)
    {
        if (!Request.ContentType?.Contains("multipart/form-data") ?? true)
        {
            return BadRequest(new { ErrorCode = 1 });
        };

        var filesInRequest = Request.Form.Files;
        if (!filesInRequest.Any())
        {
            return BadRequest(new { ErrorCode = 10 });
        }

        var file = filesInRequest[0];

        var allowedExtensions = new string[] { ".jpg", ".svg", ".png" };
        if (!allowedExtensions.Any(ext => file.FileName.EndsWith(ext, StringComparison.InvariantCultureIgnoreCase)))
        {
            return BadRequest();
        }

        var relativeServerPath = Path.Combine("Assets", "Images");
        var fullFolderPath = Path.Combine(Directory.GetCurrentDirectory(),
            relativeServerPath);

        if (file.Length <= 0)
        {
            return BadRequest(new { ErrorCode = 2 });
        }

        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        //Insert Guid with Id into database
        var fullPathToSave = Path.Combine(fullFolderPath, fileName);

        using (var stream = new FileStream(fullPathToSave, FileMode.Create))
            file.CopyTo(stream);

        return Ok(new { ErrorCode = 0 });
    }
}
