using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
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



            var apiUrl = "http://localhost:11793/api/products/";
            using (var client = new HttpClient())
            {
                using (var response = client.GetAsync(apiUrl))
                {
                    //object data = response.Result.Content.ReadAsStringAsync<Product>.Result;

                    //Product product =  JsonConvert.DeserializeObject<Product>(data);

                   
                    


                   // Assert.AreEqual(data, "acv");
                }
            }


        }
    }
}
