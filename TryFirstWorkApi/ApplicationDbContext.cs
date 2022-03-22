using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using TryFirstWorkApi.Models;

namespace TryFirstWorkApi
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
                
        }
        //DESKTOP-B823G98\SQLEXPRESS
        public DbSet<Product> Products { get; set; }
        public DbSet<Picture> Pictures { get; set; }

        
    }
}
