using Microsoft.AspNetCore.Mvc;
using MyMoneyMyFriend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMoneyMyFriend.Controllers
{
    public class HomeController : Controller
    {
        // Both the HomeController Class and Index function follow some conventions that MVC framework will use.
        public IActionResult Index()
        {

            //// Story of a request ////
            /*
             This controller receives HTTP requests that get routed to this controller and this action. The goal of this method/action is to produce a result.
             A result that represents the decision of the what to do next. In this case it's producing view. 
             In order to produce this view, the controller action has to put together a model.
             A model carries the information the view needs. The view doesnt have to do any hard work. All the data access is done by controller, view takes that model produces HTML
             */

            var model = new Restaurant { Id = 1, Name = "Hapa Sushi" };
            // All the views by default will go under /views folder then name of the controller and then the action. e.g. /Views/<controller>/<action>.cshtml -> /Views/Home/Index.cshtml
            // produces view result
            return View(model);
        }


        public IActionResult IndexOld()
        {
            // Contextual information 
            string controllerName = this.ControllerContext.ActionDescriptor.ControllerName; // Gets the controller that im inside of 
            string actionName = this.ControllerContext.ActionDescriptor.ActionName; //gets the action name for this action that is executing

            //HTTP context // It's a good practice to avoid accessing HTTP context inside Controller 
            string resHeaders = this.HttpContext.Response.Headers.ToString();
            string reqHeaders = this.HttpContext.Request.Headers.ToString();

            //this.File("Vitualpath/to/file"); // Produces FileCOntetResult. Client can download a file 

            //this.BadRequest(); // Produces IActionResult with 400 result code

            // Content() return ContentResult which implements IActionResult
            // IActionResult is formal way to encapsulate the decision of the controller. So the Controller then decides what to do next. Do I return string, content, HTML or model object? 
            // CONTROLLER MAKES THAT DECISION -> Controller never writes directly the results of its decisions. MVC framework has to figure out how to take this content result and put it in response. 
            // However, Middlewares write directly into the response.  
            // return Content(string.Format("Url: {0}/{1}", controllerName, actionName));

            ///////// Using Models ///////// 
            var model = new Restaurant { Id = 1, Name = "The Kobe" };
            // When ObjectResult is returned, MVC framework looks at the object and the model inside, and decides whether to serialize the object into XML, Json. 
            // This decision by the framework is based on the configuration information.
            // You can tell the framework to look at certain header and if specific headers exists produce Json and if a different header exists produce XML. 
            // But remember Controller doest care about that model
            return new ObjectResult(model);
        }
    }
}
