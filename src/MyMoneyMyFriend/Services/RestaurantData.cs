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
    }


    public class InMemoryRestaurantData : IRestaurantData
    {
        // Note that this list collection is not thread save.
        List<Restaurant> _restaurants;

        public InMemoryRestaurantData()
        {
            _restaurants = new List<Restaurant>
            {
                new Restaurant { Id = 1, Name = "House of Kobe"},
                new Restaurant { Id = 2, Name = "Hapa Sushi"},
                new Restaurant { Id = 3, Name = "Zeitoon"}
            };
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
