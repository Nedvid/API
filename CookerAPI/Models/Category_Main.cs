using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookerAPI.Models
{
    public class Category_Main
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Category_Main { get; set; }

        public string Name_Category_Main { get; set; }

        public ICollection<Recipe> Recipes { get; set; }
    }
}