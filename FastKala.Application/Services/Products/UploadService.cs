using FastKala.Application.Interfaces;
using FastKala.Application.ViewModels.Global;
using Microsoft.AspNetCore.Http;

namespace FastKala.Application.Services.Products;
public class UploadService : IUploadService
{
    public async Task<OperationResult> UploadMultipleImages(IList<IFormFile> files, string path)
    {
        try
        {
            if (files.Any(x => x.Length > 1024000))
            {
                return new OperationResult() { OperationStatus = Domain.Enums.OperationStatus.Fail, Message = "حجم فایل بیشتر از 2 مگابایت می‌باشد." };
            }
            foreach (IFormFile file in files)
            {
                var fileName = Path.GetRandomFileName();
                FileInfo fileInfo = new FileInfo(file.FileName);
                string fileNameWithPath = Path.Combine(path, fileName + fileInfo.Extension);
                if (fileInfo.Extension == ".png" || fileInfo.Extension == ".jpeg" || fileInfo.Extension == ".jpg")
                {
                    using (var stream = File.Create(fileNameWithPath))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                else
                {
                    return new OperationResult() { OperationStatus = Domain.Enums.OperationStatus.Fail, Message = "فرمت فایل اشتباه است" };
                }
            }
            return new OperationResult() { OperationStatus = Domain.Enums.OperationStatus.Success };
        }
        catch
        {
            return new OperationResult() { OperationStatus = Domain.Enums.OperationStatus.Exception };
        }
    }

    public async Task<OperationResult> UploadSingleImages(IFormFile file, string path, ImageSize sizeLimit)
    {
        try
        {
            if (file != null)
            {
                if (file.Length > (long)sizeLimit)
                {
                    return new OperationResult() { OperationStatus = Domain.Enums.OperationStatus.Fail, Message = $"حجم فایل بیشتر از {sizeLimit.ToString().Substring(0, 1)} مگابایت می‌باشد." };
                }
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                DirectoryInfo directory = new DirectoryInfo(path);
                FileInfo fileInfo = new FileInfo(file.FileName);
                string fileNameWithPath = Path.Combine(path, fileName + fileInfo.Extension);
                if (directory.GetFiles().Where(x => x.Name.StartsWith(fileName)).ToList().Count > 1)
                {
                    if (File.Exists(fileNameWithPath))
                    {
                        fileNameWithPath = Path.Combine(path, fileName + $"-({directory.GetFiles().Where(x => x.Name.StartsWith(fileName)).ToList().Count + 1})" + fileInfo.Extension);
                    }
                }
                if (fileInfo.Extension == ".png" || fileInfo.Extension == ".jpeg" || fileInfo.Extension == ".jpg")
                {
                    using (var stream = File.Create(fileNameWithPath))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                else
                {
                    return new OperationResult() { OperationStatus = Domain.Enums.OperationStatus.Fail, Message = "فرمت فایل ارسالی اشتباه است" };
                }

                return new OperationResult() { OperationStatus = Domain.Enums.OperationStatus.Success, Message = fileName + fileInfo.Extension };
            }
            return new OperationResult() { OperationStatus = Domain.Enums.OperationStatus.Fail };
        }
        catch
        {
            return new OperationResult() { OperationStatus = Domain.Enums.OperationStatus.Exception };
        }
    }

    public string GetImagePath(ImageType typeFolder)
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", typeFolder.ToString());

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        return path;
    }

    public string GetImageUrl(ImageType typeFolder)
    {
        string path = $"/Uploads/{typeFolder.ToString()}";
        return path;
    }
}