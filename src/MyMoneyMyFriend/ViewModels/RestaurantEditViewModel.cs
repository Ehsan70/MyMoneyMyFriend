using MyMoneyMyFriend.Entities;
using System.ComponentModel.DataAnnotations;

namespace MyMoneyMyFriend.ViewModels
{
    public class RestaurantEditViewModel
    {
        /*
         When MVC framework is binding to an input model, the MVC framework can apply the validation 
         rules that are expressed by data annotation attributes and tell us if the model state is valid or not. 
        */
        // In the form we have two things that the user can set. Name and Cuisine of the restaurant 
        [Required, MaxLength(80)]
        public string Name { get; set; }
        public CuisineType Cuisine { get; set; }

    }
}
