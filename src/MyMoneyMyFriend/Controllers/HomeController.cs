using Microsoft.AspNetCore.Mvc;
using MyMoneyMyFriend.Models;
using MyMoneyMyFriend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMoneyMyFriend.Controllers
{
    public class HomeController : Controller
    {
        private IRestaurantData _restaurantData;

        public HomeController(IRestaurantData restaurantData)
        {
            /*
             Controller doesnt know about the concert service that is implementing IRestaurantData. 
             Constructor requires something that implements IRestaurantData. 
             MVC frameworks will use services that have been registered and figures out what should be passed in anytime there is an IRestaurantData.
             Because we mapped IRestaurantData service to InMemoryRestaurantData in the startup.cs. InMemoryRestaurantData class is used.
             */
            _restaurantData = restaurantData;
        }

        public IActionResult Index()
        {
            var model = _restaurantData.GetAll();
            // All the views by default will go under /views folder then name of the controller and then the action. 
            // e.g. /Views/<controller>/<action>.cshtml -> /Views/Home/Index.cshtml
            return View(model);
        }
    }
}
