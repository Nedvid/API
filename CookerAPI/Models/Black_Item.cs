using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookerAPI.Models
{
    public class Black_Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Black_Item { get; set; }

        public int Id_User { get; set; }
        public int Id_Product { get; set; }

        [ForeignKey("Id_User")]
        public UserDetail User { get; set; }
        [ForeignKey("Id_Product")]
        public Product Product { get; set; }
    }

    public class Black_Item_Detail
    {
        public int Id_Black_Item { get; set; }

        public int Id_User { get; set; }
        public int Id_Product { get; set; }
        public string Product_Name {get;set;}
    }
}