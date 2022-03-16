using System;
using System.ComponentModel.DataAnnotations;

namespace TryFirstWorkApi.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Chalan { get; set; }
        public string BarCode { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string VendorCode { get; set; }
        public string StoreCode { get; set; }
    }
}
