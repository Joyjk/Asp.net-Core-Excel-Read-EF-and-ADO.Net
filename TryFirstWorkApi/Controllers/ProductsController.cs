using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TryFirstWorkApi.Models;

using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace TryFirstWorkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly ApplicationDbContext dbContext;
        private readonly IConfiguration configuration;

        public ProductsController(ILogger<Product> logger, ApplicationDbContext dbContext, IConfiguration configuration)
        {
            this.logger = logger;
            this.dbContext = dbContext;
            this.configuration = configuration;
        }


        [HttpGet]
        public IActionResult Get()
        {

            return Ok(dbContext.Products.ToList());
            //return Ok(LoadFromAdoDb());
        }
        [HttpGet("ado")]
        public IActionResult GetByAdo()
        {

            //return Ok(dbContext.Products.ToList());
            return Ok(LoadFromAdoDb());
        }

        private bool getBarCodeByID(string id)
        {
           // var result = dbContext.Products.Where(x => x.BarCode.Equals(id));
            var result1 = dbContext.Products.Any(c => c.BarCode.ToLower().Contains((id.ToLower())));
            return result1;
        }

        private List<Product> LoadFromAdoDb()
        {
            List<Product> products = new List<Product>();

            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            SqlCommand command = new SqlCommand("Select * from Products", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Product product = new Product();
                product.Id = Convert.ToInt32(dt.Rows[i]["ID"]);
                product.Chalan = dt.Rows[i]["Chalan"].ToString();
                product.BarCode = dt.Rows[i]["BarCode"].ToString();
                product.Date =  DateTime.Parse(dt.Rows[i]["date"].ToString());
                product.Price = decimal.Parse(dt.Rows[i]["Price"].ToString());
                product.Quantity = Convert.ToInt32(dt.Rows[i]["Quantity"]);
                product.VendorCode = dt.Rows[i]["VendorCode"].ToString();
                product.StoreCode = dt.Rows[i]["StoreCode"].ToString();

                products.Add(product);
            }

            return products;
        }

        //[HttpPost("useado")]
        //public async Task<IActionResult> PostAdo([FromBody] Product product)
        //{

        //}

        [HttpPost]
        public async Task <IActionResult> Post([FromBody] Product product)
        {
            var products =  dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();
            return NoContent(); 
        }


        [HttpPost("Importado")]
        public async Task<List<Product>> ImportAdo(IFormFile file)
        {
            var list = new List<Product>();

            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowcount = worksheet.Dimension.Rows;
                    for (int i = 2; i <= rowcount; i++)
                    {
                        if (!getBarCodeByID(worksheet.Cells[i, 2].Value.ToString().Trim()))
                        {
                            list.Add(new Product
                            {
                                Chalan = worksheet.Cells[i, 1].Value.ToString().Trim(),
                                BarCode = worksheet.Cells[i, 2].Value.ToString().Trim(),

                                //Date = Convert.ToDateTime( worksheet.Cells[i, 3].Value.ToString()),
                                //Date = DateTime.Now.ToString(),
                                //Date = DateTime.FromOADate( worksheet.Cells[i, 3].Value.ToString().ToString()),
                                Date = DateTime.FromOADate(double.Parse((worksheet.Cells[i, 3] as ExcelRange).Value.ToString())),

                                Price = Convert.ToDecimal(worksheet.Cells[i, 4].Value.ToString().Trim()),
                                Quantity = Convert.ToInt32((worksheet.Cells[i, 5].Value.ToString()).Trim()),
                                VendorCode = worksheet.Cells[i, 6].Value.ToString().Trim(),
                                StoreCode = worksheet.Cells[i, 7].Value.ToString().Trim()


                            });

                            SqlCommand sqlCommand = new SqlCommand("Insert into Products (Chalan,BarCode,Date,Price,Quantity,VendorCode,StoreCode) " +
                                "values ('" + worksheet.Cells[i, 1].Value.ToString().Trim() + "','" + worksheet.Cells[i, 2].Value.ToString().Trim()
                                + "','" + DateTime.FromOADate(double.Parse((worksheet.Cells[i, 3] as ExcelRange).Value.ToString())) + "'," +
                                Convert.ToDecimal(worksheet.Cells[i, 4].Value.ToString().Trim()) + ", " + Convert.ToInt32((worksheet.Cells[i, 5].Value.ToString()).Trim()) + " ,'"
                                + worksheet.Cells[i, 6].Value.ToString().Trim() + "','" + worksheet.Cells[i, 7].Value.ToString().Trim() + "')", connection);

                            sqlCommand.ExecuteNonQuery();
                        }
                       
                    }

                }

               
            }
            connection.Close();
            return list;
        }

        

        [HttpPost("Import")]
        public async Task<List<Product>> Import (IFormFile file)
        {
            var list = new List<Product>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowcount = worksheet.Dimension.Rows;
                    for (int i = 2; i <= rowcount; i++)
                    {
                        if (!getBarCodeByID(worksheet.Cells[i, 2].Value.ToString().Trim()))
                        {
                            list.Add(new Product
                            {
                                Chalan = worksheet.Cells[i, 1].Value.ToString().Trim(),
                                BarCode = worksheet.Cells[i, 2].Value.ToString().Trim(),

                                //Date = Convert.ToDateTime( worksheet.Cells[i, 3].Value.ToString()),
                                //Date = DateTime.Now.ToString(),
                                //Date = DateTime.FromOADate( worksheet.Cells[i, 3].Value.ToString().ToString()),
                                Date = DateTime.FromOADate(double.Parse((worksheet.Cells[i, 3] as ExcelRange).Value.ToString())),

                                Price = Convert.ToDecimal(worksheet.Cells[i, 4].Value.ToString().Trim()),
                                Quantity = Convert.ToInt32((worksheet.Cells[i, 5].Value.ToString()).Trim()),
                                VendorCode = worksheet.Cells[i, 6].Value.ToString().Trim(),
                                StoreCode = worksheet.Cells[i, 7].Value.ToString().Trim()


                            });
                        }

                    }

                }
                }
             dbContext.Products.AddRange(list);
            await dbContext.SaveChangesAsync();


            return list;

        }
    }
}
