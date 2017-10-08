using MyMoneyMyFriend.Entities;
using System.Collections.Generic;
using System;
using System.Linq;

namespace MyMoneyMyFriend.Services
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
        Restaurant Get(int id);
        /// <summary>
        ///  Returns a restaurant and takes the new restaurant to add to the data  
        /// </summary>
        Restaurant Add(Restaurant newRestaurant);
        void Commit();
    }

    public class SqlRestaurantData : IRestaurantData
    {
        private MyMoneyMyFriendDbContext _context;

        // 
        public SqlRestaurantData(MyMoneyMyFriendDbContext context)
        {
            _context = context;
        }

        public Restaurant Add(Restaurant newRestaurant)
        { 
            // DbContext is smart enough to figure out that this is Restaurant object and figure out which table it goes to.
            _context.Add(newRestaurant);
            /*
             * Note that with the InMemoryRestaurantData, we had to generate ID ourself. But with the SQL restaurant data 
             * SQL server will take care of generating Id, and Entity framework will grab that Id and assigning it restaurant.
             */
            return newRestaurant;
        }

        public void Commit()
        {
            // Now saving can be done for aggregated list pf changes 
            _context.SaveChanges();
        } 

        public Restaurant Get(int id)
        {
            // Select the restaurant that its id matches the incoming Id parameter. Otherwise return null.
            return _context.Restaurants.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            // Below gives back all the restaurants in the database. 
            // The type of _context.Restaurants is DbSet<Restaurant>, which implements IEnumerable<Restaurants>
            return _context.Restaurants;
        } 
    }



    public class InMemoryRestaurantData : IRestaurantData
    {
        // Note that this list collection is not thread save.
        // static means that there will be only one instance of this list for the entire application
        static List<Restaurant> _restaurants;

        // The constructor is static so it initializes the list of restaurant the first time we use InMemoryRestaurantData
        static InMemoryRestaurantData()
        {
            _restaurants = new List<Restaurant>
            {
                new Restaurant { Id = 1, Name = "House of Kobe"},
                new Restaurant { Id = 2, Name = "Hapa Sushi"},
                new Restaurant { Id = 3, Name = "Zeitoon"}
            };
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            // Below says find the max of Ids in the restaurant list  
            newRestaurant.Id = _restaurants.Max(r => r.Id) + 1;
            _restaurants.Add(newRestaurant);

            return newRestaurant;
        }

        public void Commit()
        {
            // ... No op
        }

        public Restaurant Get(int id)
        {
            // if r.Id equals id that is the restaurant I want to return 
            // FirstOrDefault If itt doesnt find anything that matches the id it will return the default value which is a null reference. 
            return _restaurants.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _restaurants;
        }
    }
}
