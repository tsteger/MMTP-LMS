using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MMTP_LMS.Data;
using MMTP_LMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MMTP_LMS.Utilities
{
    public class File
    {
        public async Task UploadFileAsync(IFormFile file, string txt,IHostingEnvironment _hostingEnvironment)
        {
            if (file == null || file.Length == 0)
            {
                return;
            }
            string webRootPath = _hostingEnvironment.WebRootPath;
            var path = Path.Combine(
                        webRootPath, "Documents",
                        file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
          
        }
    }
}
