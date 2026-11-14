using GCMS.Web.Data.Context;
using GCMS.Web.Models.Entities.Users;
using System.Runtime.CompilerServices;

namespace GCMS.Web.Utilities
{
    public class SaveProfileImage
    {
        private readonly IWebHostEnvironment _iwebHostEnvironment;
        public SaveProfileImage(IWebHostEnvironment iwebHostEnvironment)
        {
            _iwebHostEnvironment = iwebHostEnvironment;
        }
        public async Task<string> SaveProfileImages(IFormFile Image)
        {
            string filename = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
            var path = Path.Combine(_iwebHostEnvironment.WebRootPath, "Image", "Profile", filename);
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            using (var stream = new FileStream(path,FileMode.Create))
            {
                await Image.CopyToAsync(stream);
            }
            return filename;
        }

    }
}
