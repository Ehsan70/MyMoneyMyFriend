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
