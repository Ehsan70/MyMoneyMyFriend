using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMoneyMyFriend.Controllers
{
    public class HomeController
    {
        // Both the HomeController Class and Index function follow some conventions that MVC framework will use.
        public string Index()
        {
            return "Hello from HomeContrller.";
        }
    }
}
