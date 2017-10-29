using Microsoft.AspNetCore.Mvc;

namespace MyMoneyMyFriend.ViewComponents
{
    public class LoginLogoutViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
