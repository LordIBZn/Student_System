using Student_System.Providers;

namespace Student_System.Services
{
    public class UploadFilesHelper
    {
        private PathProvider PathProvider;

        public UploadFilesHelper(PathProvider pathProvider)
        {
            PathProvider = pathProvider;
        }

        public async Task<string> UploadFileAsync(IFormFile formFile, string nombreArchivo, Folders folder)
        {
            string path = this.PathProvider.MapPath(nombreArchivo, folder);

            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
            return path;
        }
    }
}
