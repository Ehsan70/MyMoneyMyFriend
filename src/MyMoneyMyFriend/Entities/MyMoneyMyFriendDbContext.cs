using Microsoft.EntityFrameworkCore;

namespace MyMoneyMyFriend.Entities
{
    public class MyMoneyMyFriendDbContext : DbContext
    {
        // Bellow will take the options and pass them to base class constructor (DbContext constructor)
        public MyMoneyMyFriendDbContext(DbContextOptions options) : base (options)
        {

        }
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
