namespace SendFiles.DTOs;
public enum UploadFileResponseCodes
{
    Success,
    WrongContentType,
    NoFilesFound,
    WrongExtension,
    EmptyFile,
}
public record UploadFileResponse(UploadFileResponseCodes ResponseCode, string Url = "");