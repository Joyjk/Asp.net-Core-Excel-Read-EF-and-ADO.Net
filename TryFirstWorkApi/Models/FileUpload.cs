using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace TryFirstWorkApi.Models
{
    public class FileUpload
    {
        public List<IFormFile> files { get; set; }
    }
}
