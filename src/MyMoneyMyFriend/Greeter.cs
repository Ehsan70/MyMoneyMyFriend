using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMoneyMyFriend
{
    public interface IGreeter
    {
        string GetGreeting();
    }
    public class Greeter : IGreeter
    {
        private string _greeting;

        public Greeter(IConfiguration configuration )
        {
            //ASP.NET does not know any services that implements IConfiguration. The configuration was created in the startup and is stored in Configuration property. So another service has to be register.  
            _greeting = configuration["Greeting"];
        }
        public string GetGreeting()
        {
            return _greeting;
        }
    }
}
