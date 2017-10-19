using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MyMoneyMyFriend.Entities
{
    // Bellow is how we are telling identity framework the information we want to store. 
    public class MyMoneyMyFriendDbContext : IdentityDbContext<User>
    {
        // Bellow will take the options and pass them to base class constructor (DbContext constructor)
        public MyMoneyMyFriendDbContext(DbContextOptions options) : base (options)
        {

        }
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
