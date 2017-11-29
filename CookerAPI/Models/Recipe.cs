using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookerAPI.Models
{
    public class Recipe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Recipe { get; set; }
        public int Id_User { get; set; }
        public int Id_Category_Main { get; set; }

        public string Name_Recipe { get; set; }
        public int Rate { get; set; } //rate 0-5
        public string Level { get; set; }
        public DateTime Date_Recipe { get; set; } //date of create
        public string URL_Photo { get; set; } //URL of thumbnail
        public int Time { get; set; } // in minutes
        public int Number_Person { get; set; } // recipe for number of people
        public int Steps { get; set; }
        public string Instruction { get; set; }
        public Boolean Visible { get; set; }

        [ForeignKey("Id_User")]
        public UserDetail User { get;set;}
        [ForeignKey("Id_Category_Main")]
        public Category_Main Category_Main { get; set; }

        public ICollection<Category_Recipe> Categories_Recipes { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Element> Elements { get; set; }
        public ICollection<Rate> Rates { get; set; }

    }

    public class Recipe_Details
    {
        public int Id_Recipe { get; set; }

        public string Name_Recipe { get; set; }
        public int Rate { get; set; } //rate 0-5
        public string Level { get; set; }
        public DateTime Date_Recipe { get; set; } //date of create
        public string URL_Photo { get; set; } //URL of thumbnail
        public int Time { get; set; } // in minutes
        public int Number_Person { get; set; } // recipe for number of people
        public int Steps { get; set; }
        public string Instruction { get; set; }

        public string Name_User { get; set; }
        public string Category_Main { get; set; }

        public ICollection<string> Categories { get; set; }
        public ICollection<Comment_Details> Comments_Details { get; set; }
        public ICollection<Element_Details> Elements_Details { get; set; }
    }
}