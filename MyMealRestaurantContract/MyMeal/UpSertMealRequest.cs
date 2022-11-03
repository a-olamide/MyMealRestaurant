using System;
using System.Collections.Generic;
using System.Text;

namespace MyMealRestaurantContract.MyMeal
{
    public class UpSertMealRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public List<string> Savory { get; set; }
        public List<string> Sweet { get; set; }
    }
}
