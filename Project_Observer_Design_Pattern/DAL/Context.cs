using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Project_Observer_Design_Pattern.DAL
{
    public class Context : IdentityDbContext<AppUser, AppRole, int>

    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-0LTDDDI\\SQLEXPRESS01;initial catalog=ObserverDb;integrated security=true");
        }

        public DbSet<Discount> Discounts { get; set; }
    }
}
