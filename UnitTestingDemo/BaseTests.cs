using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TryFirstWorkApi;

namespace UnitTestingDemo
{
    //[TestClass]
    public class BaseTests
    {
        protected ApplicationDbContext BuildContext(string databaseName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName).Options;

            var dbContext = new ApplicationDbContext(options);
            return dbContext;
        }

        //protected IMapper BuildMap()
        //{
        //    var config = new MapperConfiguration(options =>
        //    {
        //        options.AddProfile(new AutoMapperProfile());
        //    });

        //    return config.CreateMapper();
        //}
       
    }
}
