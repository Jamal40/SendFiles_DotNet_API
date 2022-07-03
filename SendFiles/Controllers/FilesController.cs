using Microsoft.AspNetCore.Mvc;
using SendFiles.DTOs;

namespace SendFiles.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilesController : ControllerBase
{
    [HttpPost]
    public ActionResult UploadImage()
    {
        if (!Request.ContentType?.Contains("multipart/form-data") ?? true)
        {
            return BadRequest(new UploadFileResponse(UploadFileResponseCodes.WrongContentType));
        };

        var filesInRequest = Request.Form.Files;
        if (!filesInRequest.Any())
        {
            return BadRequest(new UploadFileResponse(UploadFileResponseCodes.NoFilesFound));
        }

        var file = filesInRequest[0];

        var allowedExtensions = new string[] { ".jpg", ".svg", ".png" };
        if (!allowedExtensions.Any(ext => file.FileName.EndsWith(ext, StringComparison.InvariantCultureIgnoreCase)))
        {
            return BadRequest(new UploadFileResponse(UploadFileResponseCodes.WrongExtension));
        }

        var relativeServerPath = Path.Combine("Assets", "Images");
        var fullFolderPath = Path.Combine(Directory.GetCurrentDirectory(),
            relativeServerPath);

        if (file.Length <= 0)
        {
            return BadRequest(new UploadFileResponse(UploadFileResponseCodes.EmptyFile));
        }

        var fileName = $"{Guid.NewGuid()}_{file.FileName}";

        var fullPathToSave = Path.Combine(fullFolderPath, fileName);

        using (var stream = new FileStream(fullPathToSave, FileMode.Create))
            file.CopyTo(stream);

        var fullAccessibleImageURL = $"{Request.Scheme}://{Request.Host}/Assets/Images/{fileName}";
        return Ok(new UploadFileResponse(UploadFileResponseCodes.Success, fullAccessibleImageURL));
    }
}
