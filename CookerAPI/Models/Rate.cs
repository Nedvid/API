using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookerAPI.Models
{
    public class Rate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Rate { get; set; }
        public int Id_User { get; set; }
        public int Id_Recipe { get; set; }
        public int Value_Rate { get; set; } //1-5

        [ForeignKey("Id_User")]
        public UserDetail User { get; set; }
        [ForeignKey("Id_Recipe")]
        public Recipe Recipe { get; set; }
    }
}