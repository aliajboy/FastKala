using FastKala.Application.ViewModels.Global;
using FastKala.Domain.Models.Product;
using Microsoft.AspNetCore.Http;

namespace FastKala.Application.Interfaces.Global;
public interface IUploadService
{
    Task<List<ProductImage>> UploadMultipleImages(IList<IFormFile> files, ImageType type, ImageSize sizeLimit, int productId);

    /// <summary>
    /// Upload Single File to Server Specified Folder
    /// </summary>
    /// <param name="file">IFormFile of the uploaded file</param>
    /// <param name="type">the type of your file. based on this type, the directory will specified</param>
    /// <param name="sizeLimit">size limit of the image. If grather than this, returns an error and doesn't save image.</param>
    /// <returns>Operation result Success if Image Saved. return false if image is bigger than size limit or when file is null.</returns>
    Task<OperationResult> UploadSingleImages(IFormFile file, ImageType type, ImageSize sizeLimit);
    string GetImagePath(ImageType typeFolder);
    string GetImageUrl(ImageType imageType, string imageName);
}