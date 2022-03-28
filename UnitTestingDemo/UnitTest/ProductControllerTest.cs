using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TryFirstWorkApi.Controllers;
using TryFirstWorkApi.Models;

namespace UnitTestingDemo.UnitTest
{
    //[TestClass]
    public class ProductControllerTest : BaseTests
    {
        [TestMethod]
        public async void GetAllProducts()
        {
            //Preparation
            //var databaseName = Guid.NewGuid().ToString();
            //var context =  BuildContext(databaseName);
            //context.Products.Add(new Product() 
            //{ BarCode = "abc", Chalan = "asd", Date = DateTime.Parse("01-12-2022"), Price = 23.12m, Id = 1, Quantity = 22, StoreCode = "232as", VendorCode = "asdwq" });
            //context.Products.Add(new Product() 
            //{ BarCode = "def", Chalan = "rer", Date = DateTime.Parse("05-09-2020"), Price = 734.12m, Id = 2, Quantity = 21, StoreCode = "65sd31", VendorCode = "a23sdwq" });

            //context.Products.Add(new Product()
            //{ BarCode = "ghfd", Chalan = "rsdfer", Date = DateTime.Parse("08-02-2021"), Price = 4343.12m, Id = 3, Quantity = 65, StoreCode = "fsf23", VendorCode = "sdf234" });

            //context.SaveChanges();
            //var context2 = BuildContext(databaseName);


            //IConfiguration configuration;
            //configuration.GetConnectionString("DefaultConnection");



            Assert.IsTrue(true);

            //Testing
            //var controller = new ProductsController(context2);

            //var response = await controller.Get();

            var apiUrl = "http://localhost:11793/api/products/";
            using (var client = new HttpClient())
            {
                using (var response = client.GetAsync(apiUrl))
                {
                    string data = response.Result.Content.ReadAsStringAsync().Result;

                    Assert.AreEqual(data, "acv");
                }
            }


        }
    }
}
