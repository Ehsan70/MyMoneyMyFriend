using Microsoft.AspNetCore.Mvc;
using MyMoneyMyFriend.Services;
using System.Threading.Tasks;

namespace MyMoneyMyFriend.ViewComponents
{
    public class GreetingViewComponent : ViewComponent
    {
        private IGreeter _greeter;

        // Framework will inject IGreeter service for us
        public GreetingViewComponent(IGreeter greeter)
        {
            _greeter = greeter;
        }
        /* 
         * ViewComponent is very similar to a controller. But instead of actions it have a single method called invoke and will return 
         * IViewComponent result (very much like IActionResult)
        */
        public IViewComponentResult Invoke()
        {

            // Building a model for our viewComponent
            var model = _greeter.GetGreeting();
            /*
            * The idea of this invoke is for this view component to build his model and render a view. So let's return a view here using the model
            */
            /*
             * Note that this model is just a string. When an string passed to a view method, MVC framework will think that you are specifying the 
             * name of the view that you want to render. So if your model is an string the safest is to explicitly specify the name of the view and then pass
             * the model as a second parameter.  
             * The default view for a ViewComponent is a view called default.
             */
            return View("Default", model);
            /*
             * Logic to locating a view for a particular component is very similar to the logic for locating view for controller action.
             * MVC will search through a predetermined set of directories. The folder /Shared/Components/Greeting has to be greeting for 
             * it be findable for MVC.  
             */
        }

        /*
         View Component is like a MVC request inside of a MVC request. I was able to instantiate a class that can inject its own dependencies (constructor). 
         Execute a method that can build its own model and its own views (Invoke) 
         */
    }
}
