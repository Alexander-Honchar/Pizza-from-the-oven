using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pizza_WebAPI.Models;

namespace Pizza_WebAPI.Data
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        

        public DbSet<Pizza>? Pizzas { get; set; }
        public DbSet<SizePizza>? SizePizzas { get; set;}
        public DbSet<OrderHeader>? OrderHeaders { get; set; }
        public DbSet<OrderDetails>? OrderDetailes { get; set; }
        public DbSet<MenuItem>? MenuItems { get; set; }
        public DbSet<CategoryPizza>? CategoryPizzas { get; set;}

        public DbSet<CategoryMenu>? CategoryMenus { get; set; }
        public DbSet<PizzaKingSize>? PizzaKingSizes { get; set; }

        public DbSet<Workers>? Workers { get; set; }
        public DbSet<Client>? Clients { get; set; }

    }
}
