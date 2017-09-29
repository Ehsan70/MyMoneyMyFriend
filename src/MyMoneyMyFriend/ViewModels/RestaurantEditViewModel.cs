using MyMoneyMyFriend.Entities;

namespace MyMoneyMyFriend.ViewModels
{
    public class RestaurantEditViewModel
    {
        // In the form we have two things that the user can set. Name and Cuisine of the restaurant 
        public string Name { get; set; }
        public CuisineType Cuisine { get; set; }

    }
}
