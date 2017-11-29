using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookerAPI.Models
{
    public class List
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_List { get; set; }

        public string Items { get; set; }

        public ICollection<UserDetail> Users { get; set; }
    }
}