using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CookerAPI.Models
{
    public class Element
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Element{ get; set; }

        public int Id_Product { get; set; }

        public int Id_Recipe { get; set; }

        public string Quantity { get; set; }

        [ForeignKey("Id_Product")]
        public Product Product { get; set; }
        [ForeignKey("Id_Recipe")]
        public Recipe Recipe { get; set; }
    }

    public class Element_Details
    {
        public string Name_Product { get; set; }
        public string Quantity { get; set; }
    }
}