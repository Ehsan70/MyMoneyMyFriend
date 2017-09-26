using MyMoneyMyFriend.Entities;
using System.Collections.Generic;

namespace MyMoneyMyFriend.ViewModels
{
    public class HomePageViewModel
    {
        public string CurrentMessage { get; set; }
        public IEnumerable<Restaurant> Restaurants { get; set; }
    }
}
