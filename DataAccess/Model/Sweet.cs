using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Model
{
    public class Sweet
    {
        [Key]
        public int SweetId { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        [Required(ErrorMessage = "Sweet Name is required")]
        public string Name { get; set; }

        public int MealId { get; set; }
    }
}
