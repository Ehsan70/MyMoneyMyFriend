using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MyMoneyMyFriend.Entities
{
    // Bellow is how we are telling identity framework the information we want to store. 
    public class MyMoneyMyFriendDbContext : IdentityDbContext<User>
    {
        /*
         You have to create a new migration because IdentityDbContext contains its own Db sets. I wants to create a schema to store all 
         the information about the entities like the user class. 
         In package Manager Console ran Add-Migration "Identity" and then Update-Database.
         This will go and update the data base that is stored in ConnectionStrings field of appsetting.json
         */

        // Bellow will take the options and pass them to base class constructor (DbContext constructor)
        public MyMoneyMyFriendDbContext(DbContextOptions options) : base (options)
        {

        }
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
