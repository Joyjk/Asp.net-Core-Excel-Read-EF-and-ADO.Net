using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TryFirstWorkApi.Models
{
    public class Picture
    {
        //[Key]
        //public int Id { get; set; }
        //[Required]
        public string Title { get; set; }
        [Required]
        public string Pic { get; set; }
        //public string BaseSixtyFour { get; set; }
        //[NotMapped]
        //public List<IFormFile> files { get; set; }

    }
}
