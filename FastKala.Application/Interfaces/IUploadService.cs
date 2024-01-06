using FastKala.Application.ViewModels.Global;
using Microsoft.AspNetCore.Http;

namespace FastKala.Application.Interfaces;
public interface IUploadService
{
    Task<OperationResult> UploadMultipleImages(IList<IFormFile> files, string path);

    /// <summary>
    /// Upload Single File to Server Specified Folder
    /// </summary>
    /// <param name="file">IFormFile of the uploaded file</param>
    /// <param name="path">the path you want to save your file</param>
    /// <param name="sizeLimit">size limit of the image. If grather than this, returns an error and doesn't save image.</param>
    /// <returns>Operation result Success if Image Saved. return false if image is bigger than size limit or when file is null.</returns>
    Task<OperationResult> UploadSingleImages(IFormFile file, string path, ImageSize sizeLimit);
    string GetImagePath(ImageType typeFolder);
    string GetImageUrl(ImageType typeFolder);
}