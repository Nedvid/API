using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CookerAPI.Models
{
    public class Favorites
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Favorite { get; set; }

        public int Id_User { get; set; }

        public int Id_Recipe { get; set; }

        [ForeignKey("Id_User")]
        public Product User { get; set; }
        [ForeignKey("Id_Recipe")]
        public Recipe Recipe { get; set; }
    }
}