using Microsoft.AspNetCore.Mvc;

namespace MyMoneyMyFriend.Controllers
{
    // [Route("about")] // Because the route and the controller class have the same name, you could use tokens (Below)
    // [Route("[controller]")] // Because the route for the controller class and actions have the same name, you could remove the routes for actions and use below
    [Route("company/[controller]/[action]")] // This will make the controller to respond when URL has company 
    //[Route("[controller]/[action]")]

    public class AboutController
    // Name of the Controller class always has the world controller
    {
        //[Route("")] // This would be a default action. If a urls with /about endpoint would reach this method 
        //[Route("phone")] // phone has to be in the URLs for this method to be called. The endpoint /about will not call this method and only /about/phone does 
        public string Phone()
        {
            return "1 111 111 1111";
        }

        //[Route("address")] // Because the route and the method have the same name, you could use tokens (Below)
        //[Route("[]")] // This way, if the method name is changes you doesnt have to change the route rules 
        public string Address()
        {
            return "Canada";
        }
    }
}
