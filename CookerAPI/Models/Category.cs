using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookerAPI.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Category_Recipe { get; set; }

        public string Name_Category_Recipe { get; set;}

        public ICollection<Category_Recipe> Categories_Recipes { get; set; }
    }
}