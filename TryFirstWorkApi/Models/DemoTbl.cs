using System.ComponentModel.DataAnnotations;

namespace TryFirstWorkApi.Models
{
    public class DemoTbl
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
