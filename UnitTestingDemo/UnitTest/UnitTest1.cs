using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using TryFirstWorkApi.Models;
using TryFirstWorkApi.Testing;

namespace UnitTestingDemo
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //preparation 

            Account origin = new Account() { Funds=0};
            Account destination = new Account() { Funds=0};
            decimal amountToTransefer = 5m;

            var mockValidateWireTransfer = new Mock<IValidateWireTransfer>();
            mockValidateWireTransfer.Setup(x => x.validate(origin, destination, amountToTransefer))
                .Returns(new OperationResult(false, "custom error message"));

            var service =  new TransferService(mockValidateWireTransfer.Object);


            //var service = new TransferService(new WireTransferValidator());

            Exception expectedException = null;

            //Testing
            try
            {
                service.WireTransfer(origin, destination, amountToTransefer);

            }
            catch (Exception ex)
            {

                expectedException = ex;
            }


            //Verification

            if (expectedException == null)
            {
                Assert.Fail("An exception was expected");
            }

            Assert.IsTrue(expectedException is ApplicationException);
            Assert.AreEqual(expectedException.Message, "custom error message");



        }

        [TestMethod]
        public void TestMethod2()
        {

            #region Other:
            //Account origin = new Account() { Funds =10};
            //Account destination = new Account() { Funds =5};
            //decimal amountToTransefer = 7m;



            //var mockValidateWireTransfer = new Mock<IValidateWireTransfer>();
            //mockValidateWireTransfer.Setup(x => x.validate(origin, destination, amountToTransefer))
            //    .Returns(new OperationResult(true));

            //var service = new TransferService(mockValidateWireTransfer.Object);

            ////Testing
            //service.WireTransfer(origin,destination, amountToTransefer);
            ////verification
            //Assert.AreEqual(3,origin.Funds);
            //Assert.AreEqual(12,destination.Funds);
            #endregion


            var apiUrl = "http://localhost:11793/api/products/";
            using (var client = new HttpClient())
            {
                using (var response = client.GetAsync(apiUrl))
                {
                    string data = response.Result.Content.ReadAsStringAsync().Result;
                    var data2 = response.Result.Content.ReadAsStringAsync().Result;
                    
                    var statusCode = response.Result.StatusCode;

                    var products =  JsonConvert.DeserializeObject<List<Product>>(data);

                   // Assert.AreEqual(5008, product.Count);

                    int count = 0;
                    foreach (var item in products)
                    {
                        if(item.BarCode == "n1ihil")
                        {
                            count = 1;
                        }
                    }
                    // Assert.AreEqual(1, count);

                    Assert.AreEqual(statusCode, System.Net.HttpStatusCode.OK);

                    //Assert.AreEqual(product, "acv");
                }
            }


        }

        //[TestMethod]
        //public void getInvItemsCount()
        //{
        //    int recCount = 0;
        //   string uri = "http://localhost:11793/api/products/";
        //    var webRequest = (HttpWebRequest)WebRequest.Create(uri);
        //    webRequest.Method = "GET";
        //    using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
        //    {
        //        if (webResponse.StatusCode == HttpStatusCode.OK)
        //        {
        //            var reader = new StreamReader(webResponse.GetResponseStream());
        //            string s = reader.ReadToEnd();
        //            Int32.TryParse(s, out recCount);
        //        }
        //    }
             
        //    Assert.AreEqual(recCount, 5);
        //}
    }
}
