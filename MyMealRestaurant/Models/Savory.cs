using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyMealRestaurant.Models
{
    public class Savory
    {
        [Key]
        public int SavoryId { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        [Required(ErrorMessage = "Savour Name is required")]
        public string Name { get; set; }

        public int MealId { get; set; }
    }
}
