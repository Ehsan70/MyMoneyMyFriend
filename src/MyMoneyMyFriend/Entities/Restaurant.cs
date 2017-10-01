using System.ComponentModel.DataAnnotations;

namespace MyMoneyMyFriend.Entities
{
    public enum CuisineType
    {
        None,
        Italian,
        French,
        Japanese,
        American
    }

    public class Restaurant
    {
        public int Id { get; set;}

        /*
         Enforce validation rule for a property. These are client side validation and not server side validation. Bellow two line are shown:
            <label for="Name">Restaurant Name</label>
            <input class="text-box single-line" data-val="true" data-val-maxlength="The field Restaurant Name must be a string or array type with a maximum length of '80'." data-val-maxlength-max="80" data-val-required="The Restaurant Name field is required." id="Name" name="Name" type="text" value="">
        Never trust client side validation
        */
        [Required, MaxLength(80)]
        /* If Password is chosen as a DataType bellow lines would appear in HTML page. When user starts typing, black circles where showed in the browser
               <label for="Name">Restaurant Name</label>
               <input class="text-box single-line password" id="Name" name="Name" type="password" value="">
           If text was chosen as a DataType bellow lines would appear in HTML page
               <label for="Name">Restaurant Name</label>
               <input class="text-box single-line password" id="Name" name="Name" type="password" value="">
         */
       // [DataType(DataType.Text)]
        /* If the Display attribute was not inserted, bellow two lines would have appeared on the HTML page: 
               <label for="Name">Name</label>
               <input class="text-box single-line" id="Name" name="Name" type="text" value="">
           Bellow will use the text 'Restaurant Name' instead of using the actual name. Bellow two lines is appearing on the HTML page
               <label for="Name">Restaurant Name</label>
               <input class="text-box single-line" id="Name" name="Name" type="text" value="">
         */
        [Display(Name="Restaurant Name")]
        public string Name { get; set; }
        public CuisineType Cuisine { get; set; }
    }
}
