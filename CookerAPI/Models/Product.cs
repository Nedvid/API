using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookerAPI.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Product { get; set; }
        public string Name_Product { get; set; }
        public Boolean Visible { get; set; }

        public ICollection<Black_Item> Black_Items;
        public ICollection<Element> Elements { get; set;}
    }
}