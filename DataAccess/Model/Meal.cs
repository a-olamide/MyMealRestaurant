using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    internal class Meal
    {
        [Key]
        public int MealId { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string Description { get; set; } = "";//default to empty str
        [Column(TypeName = "nvarchar(10)")]
        [Required(ErrorMessage = "Type is required")]
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public DateTime LastModifiesDateTime { get; set; } =  DateTime.UtcNow;
        public List<Savory> Savories { get; set; }
        public List<Sweet> Sweets { get; set; }

    }
}

