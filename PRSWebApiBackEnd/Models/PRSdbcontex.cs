using Microsoft.EntityFrameworkCore;
using PRSWebApiBackEnd.Models;

namespace PRSWebApiBackEnd.Models
{
    public class PRSdbcontex : DbContext
    {
        public DbSet<User> Users { get; set; } //sets user table will add more later 
        public DbSet<Products> Products { get; set; } //sets  Products table will add more later 
        public DbSet<Vendors> Vendors { get; set; } //sets Vendors table will add more later 
        public DbSet<Requests> Requests { get; set; } //sets Requests table will add more later  
        public DbSet<RequestsLine> RequestsLine { get; set; } //sets RequestsLine table will add more later 
        
        public PRSdbcontex()
        {

        }

        public PRSdbcontex(DbContextOptions<PRSdbcontex> options) : base(options) //Example of DB Class
        {
        
        }


      

        
     


    }



}
