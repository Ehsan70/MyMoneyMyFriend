using Microsoft.AspNetCore.Mvc;
using MyMoneyMyFriend.Entities;
using MyMoneyMyFriend.Services;
using MyMoneyMyFriend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMoneyMyFriend.Controllers
{
    public class HomeController : Controller
    {
        private IGreeter _greeter;
        private IRestaurantData _restaurantData;

        public HomeController(IRestaurantData restaurantData, IGreeter greeter)
        {
            /*
             Controller doesnt know about the concert service that is implementing IRestaurantData. 
             Constructor requires something that implements IRestaurantData. 
             MVC frameworks will use services that have been registered and figures out what should be passed in anytime there is an IRestaurantData.
             Because we mapped IRestaurantData service to InMemoryRestaurantData in the startup.cs. InMemoryRestaurantData class is used.
             */
            _restaurantData = restaurantData;
            _greeter = greeter;
        }

        public IActionResult Index()
        {
            var model = new HomePageViewModel();
            model.Restaurants = _restaurantData.GetAll();
            model.CurrentMessage = _greeter.GetGreeting();
            // The controller has constructed the view model using two services Greeter and estaurantData
            // The controller will take that model and pass it off to index view. 
            return View(model);
        }

        public IActionResult Details(int id)
        {
            // Remember our routing algorithm: routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            // if something else after the action appears we will treat as a parameter named ID. 
            // Mvc is able o find a parameter in the URL and it's gonna treat it as Id
            //return id.ToString();
            var model = _restaurantData.Get(id);
            if (model == null)
            {
                // Bellow will return a 404  page. User should not see it though 
                //return NotFound();

                // You could redirect the user to go to another action possibly on different controller even. I this case though in the same Home controller 
                // return RedirectToAction("Index");
                return RedirectToAction(nameof(Index)); // This will return the actual name of he function, in case it got changed. 
            }
            // Returning a view with that model 
            return View(model); 
        }


        public IActionResult Create()
        {
            return View();
        }
    }
}
