using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMoneyMyFriend.Entities
{
    // This is stored in Entity folder because its information is saved in a database. 
    public class User : IdentityUser
    {
        public User()
        {
            // By default basic properties like user name email phone number are inherited from the IdentityUser class. 
            // Any customization for this class can be done here. 
        }
    }
}
