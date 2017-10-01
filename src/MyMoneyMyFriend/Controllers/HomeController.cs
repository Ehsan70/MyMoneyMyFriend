using Microsoft.AspNetCore.Mvc;
using MyMoneyMyFriend.Entities;
using MyMoneyMyFriend.Services;
using MyMoneyMyFriend.ViewModels;

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

        // Restricts this version of Create to only respond to HTTP Get request. 
        // That is the type of the request that will be issues with the user goes to /home/create URL
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // This should only respond to post request.
        // That is case when user clicks the save button. 
        [HttpPost]
        /*
         <input name="__RequestVerificationToken" type="hidden" value="CfDJ8KsTxBQdYPRLtVIjIzOf5JVnlcjiUdkvzQ5zxOufyf8AdifRUjBqZ77rPtC6kbeqpiiMYqbrm_0m-MSZbk8qDoeJUWK_fuwghUamhusk1By0PxuRkCOL5e6JpQ_PoXaXJ81pOFGaDmVlCP-dbncZqSk">
         Request verification token ensures that posted form that a user sends to us is from a form that we gave to use. Helps prevents cross site request forgery.
         Make sure that MVC framework verifies this token against the cookie that the framework sets in the browser. This will avoid cross-Site request forgery.    
         You should use ValidateAntiForgeryToken when use form post. 
         */
        [ValidateAntiForgeryToken]
        public IActionResult Create(RestaurantEditViewModel Model)
        {
            /*
             When MVC framework is binding to an input model, the MVC framework can apply the validation 
             rules that are expressed by data annotation attributes and tell us if the model state is valid or not. 
            */
            /*
             When MVC framework creates this view model, and populates the properties on this model object, will executes the 
             validation rules to see if this model is valid or not. You can check the state of your model by checking the 
             property which is inherited from base controller class.
             */
            if (ModelState.IsValid)
            {
                // if the IsValid flag is true, all of the data annotations have passed and the Restaurant is in a good state.
                var newRestaurant = new Restaurant();
                newRestaurant.Name = Model.Name;
                newRestaurant.Cuisine = Model.Cuisine;
                newRestaurant = _restaurantData.Add(newRestaurant);
                // Telling the browser to go to some other URL and issue a get request from there.
                // Goes to Details action of Home controller which tells the browser to issue a get request from that URL
                // The second argument is the Route values that are passed to that Action. Bellow, would be similar to /Home/Details/4
                return RedirectToAction("Details", new { id = newRestaurant.Id });
            }
             // if validation fails, return that Create View again and represent that form and allow the user to fix any user that might have occurred.   
            return View();
        }
    }
}
