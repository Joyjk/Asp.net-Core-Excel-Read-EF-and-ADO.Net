using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TryFirstWorkApi.Models;
using TryFirstWorkApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.IO;

namespace TryFirstWorkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    
        {  
        private readonly IDapper _dapper;
        public HomeController(IDapper dapper)
        {
            _dapper = dapper;
        }
        #region Other
        //[HttpPost(nameof(Create))]
        //public async Task<int> Create(Product data)
        //{
        //    var dbparams = new DynamicParameters();
        //    dbparams.Add("Id", data.Id, DbType.Int32);
        //    var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_Add_Article]"
        //        , dbparams,
        //        commandType: CommandType.StoredProcedure));
        //    return result;
        //}
        #endregion
        [HttpGet(nameof(GetById))]
        public async Task<Product> GetById(int Id)
        {
            var result = await Task.FromResult(_dapper.Get<Product>($"Select * from [Products] where Id = {Id}", null, commandType: CommandType.Text));
            return result;
        }
        #region Other2
        //[HttpDelete] //(nameof(Delete))
        //public async Task<int> Delete()
        //{
        //    var result = await Task.FromResult(_dapper.Execute($"[sp_deleteProduct]", null, commandType: CommandType.StoredProcedure));
        //    return result;
        //}
        //[HttpGet(nameof(Count))]
        //public Task<int> Count(int num)
        //{
        //    var totalcount = Task.FromResult(_dapper.Get<int>($"select COUNT(*) from [Products] WHERE Age like '%{num}%'", null,
        //            commandType: CommandType.Text));
        //    return totalcount;
        //}
        //[HttpPatch(nameof(Update))]
        //public Task<int> Update(Product data)
        //{
        //    var dbPara = new DynamicParameters();
        //    dbPara.Add("Id", data.Id);
        //    dbPara.Add("Name", data.BarCode, DbType.String);

        //    var updateArticle = Task.FromResult(_dapper.Update<int>("[dbo].[SP_Update_Article]",
        //                    dbPara,
        //                    commandType: CommandType.StoredProcedure));
        //    return updateArticle;
        //}
        #endregion


        [HttpPost("ImportDapper")]
        public async Task<List<Product>> ImportAdo(IFormFile file)
        {
            var list = new List<Product>();

           // SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            //connection.Open();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                   
                    

                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowcount = worksheet.Dimension.Rows;
                    for (int i = 2; i <= rowcount; i++)
                    {
                        #region OLD
                        //if (!getBarCodeByID(worksheet.Cells[i, 2].Value.ToString().Trim()))
                        {
                            //list.Add(new Product
                            //{
                            //    Chalan = worksheet.Cells[i, 1].Value.ToString().Trim(),
                            //    BarCode = worksheet.Cells[i, 2].Value.ToString().Trim(),

                            //    //Date = Convert.ToDateTime( worksheet.Cells[i, 3].Value.ToString()),
                            //    Date = DateTime.Now,
                            //    //Date = DateTime.FromOADate( worksheet.Cells[i, 3].Value.ToString().ToString()),
                            //    // Date = DateTime.FromOADate(double.Parse((worksheet.Cells[i, 3] as ExcelRange).Value.ToString())),

                            //    Price = Convert.ToDecimal(worksheet.Cells[i, 4].Value.ToString().Trim()),
                            //    Quantity = Convert.ToInt32((worksheet.Cells[i, 5].Value.ToString()).Trim()),
                            //    VendorCode = worksheet.Cells[i, 6].Value.ToString().Trim(),
                            //    StoreCode = worksheet.Cells[i, 7].Value.ToString().Trim()


                            //});

                            //SqlCommand sqlCommand = new SqlCommand("Insert into Products (Chalan,BarCode,Date,Price,Quantity,VendorCode,StoreCode) " +
                            //    "values ('" + worksheet.Cells[i, 1].Value.ToString().Trim() + "','" + worksheet.Cells[i, 2].Value.ToString().Trim()
                            //    + "','" +

                            //  //DateTime.FromOADate(double.Parse((worksheet.Cells[i, 3] as ExcelRange).Value.ToString()))
                            //  DateTime.Now
                            //    + "'," +
                            //    Convert.ToDecimal(worksheet.Cells[i, 4].Value.ToString().Trim()) + ", " + Convert.ToInt32((worksheet.Cells[i, 5].Value.ToString()).Trim()) + " ,'"
                            //    + worksheet.Cells[i, 6].Value.ToString().Trim() + "','" + worksheet.Cells[i, 7].Value.ToString().Trim() + "')", connection);

                            //sqlCommand.ExecuteNonQuery();
                            
                        }
                        #endregion
                        var dbPara = new DynamicParameters();

                        //dbPara.Add("Chalan", data.Id);
                        dbPara.Add("Chalan", worksheet.Cells[i, 1].Value.ToString().Trim(), DbType.String);
                        dbPara.Add("BarCode", worksheet.Cells[i, 2].Value.ToString().Trim(),DbType.String);
                        dbPara.Add("Date", DateTime.Now, DbType.DateTime);
                        dbPara.Add("Price", Convert.ToDecimal(worksheet.Cells[i, 4].Value.ToString().Trim()), DbType.Decimal);
                        dbPara.Add("Quantity", Convert.ToInt32((worksheet.Cells[i, 5].Value.ToString()).Trim()), DbType.Int32);
                        dbPara.Add("VendorCode", worksheet.Cells[i, 6].Value.ToString().Trim(), DbType.String);
                        dbPara.Add("StoreCode", worksheet.Cells[i, 7].Value.ToString().Trim(), DbType.String);


                        var result = await Task.FromResult(_dapper.Insert<int>("Insert into Products (Chalan, BarCode, Date, Price, Quantity, VendorCode, StoreCode)" +
                            " values (@Chalan, @BarCode, @Date, @Price, @Quantity, @VendorCode, @StoreCode)"
                                                                , dbPara, commandType: CommandType.Text));


                    }

                }


            }
            //connection.Close();
            return list;
        }


        //public IActionResult PostData()
        //{
        //    return Ok();
        //}
       

    }
}
