using FastKala.Application.ViewModels.Global;
using Microsoft.AspNetCore.Http;

namespace FastKala.Application.Interfaces;
public interface IUploadService
{
    Task<OperationResult> UploadMultipleImages(IList<IFormFile> files, string path);
    Task<OperationResult> UploadSingleImages(IFormFile file, string path);
    string GetImagePath(ImageType typeFolder);
    string GetImageUrl(ImageType typeFolder);
}