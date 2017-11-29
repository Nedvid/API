using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CookerAPI.Models
{
    public class Category_Recipe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Category_Recipe{ get; set; }

        public int Id_Category { get; set; }

        public int Id_Recipe { get; set;}

        [ForeignKey("Id_Category")]
        public Category Category { get; set; }
        [ForeignKey("Id_Recipe")]
        public Recipe Recipe { get; set; }

    }
}