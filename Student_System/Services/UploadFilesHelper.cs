using Student_System.Providers;

namespace Student_System.Services
{
    public class UploadFilesHelper
    {
        private PathProvider pathProvider;

        public UploadFilesHelper(PathProvider pathProvider)
        {
            this.pathProvider = pathProvider;
        }

        public async Task<string> UploadFileAsync(IFormFile formFile, string nombreArchivo, Folders folder)
        {
            string path = this.pathProvider.MapPath(nombreArchivo, folder);

            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
            return path;
        }
    }
}
