using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookerAPI.Models
{
    public class UserDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_User { get; set; }

        public string Login { get; set; }
        public string Email { get; set; }
        public int Id_List { get; set; }
        public Boolean Social_Account { get; set; } // 1 - social account, 0 - normal account
        public string URL_Avatar { get; set; } //URL of avatar thumbnail

        [ForeignKey("Id_List")]
        public List List { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
        public ICollection<Black_Item> Black_Items { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Rate> Rates { get; set; }
    }
}