using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace LD_4_Interneto_tech.Interfaces
{
    public interface IPhotoService
    {
         Task<ImageUploadResult> UploadPhotoAsync(IFormFile photo);
         Task<DeletionResult> DeletePhotoAsync(string publicId);
        
    }
}