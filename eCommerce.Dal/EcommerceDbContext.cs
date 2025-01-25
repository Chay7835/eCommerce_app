using Microsoft.EntityFrameworkCore;
using eCommerce.Models;

namespace eCommerce.Dal
{
    public class EcommerceDbContext : DbContext
    {
        public EcommerceDbContext()
        {
            
        }
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                // only for Migration
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=ByteJanECommerceDb24;Trusted_Connection=true;TrustServerCertificate=True;");
            }
        }

        // Creating the DbSet (which makes the data into Tables and place it into database
        // through a mediator named DbContext
        public DbSet<Category> categories { get; set; }
        public DbSet<Product> products { get; set; }    
        public DbSet<Customer> customers { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<CartDetail> cartDetails { get; set; }
        public DbSet<Invoice> invoices { get; set; }
    }
}