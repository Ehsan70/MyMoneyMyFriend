using MyMoneyMyFriend.Entities;
using System.Collections.Generic;

namespace MyMoneyMyFriend.Services
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
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


        public IEnumerable<Restaurant> GetAll()
        {
            return _restaurants;
        }
    }
}
