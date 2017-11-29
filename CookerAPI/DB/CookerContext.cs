using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CookerAPI.DB
{
    public class CookerContext : DbContext
    {
        public CookerContext() : base("name=CookerContext")
        {

            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public System.Data.Entity.DbSet<CookerAPI.Models.Recipe> Recipes { get; set; }

        public System.Data.Entity.DbSet<CookerAPI.Models.Category_Main> Categories_Main { get; set; }

        public System.Data.Entity.DbSet<CookerAPI.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<CookerAPI.Models.Category_Recipe> Categories_Recipes { get; set; }

        public System.Data.Entity.DbSet<CookerAPI.Models.UserDetail> Users { get; set; }

        public System.Data.Entity.DbSet<CookerAPI.Models.Product> Products { get; set; }

        public System.Data.Entity.DbSet<CookerAPI.Models.Element> Elements { get; set; }

        public System.Data.Entity.DbSet<CookerAPI.Models.Comment> Comments { get; set; }

        public System.Data.Entity.DbSet<CookerAPI.Models.List> Lists { get; set; }

        public System.Data.Entity.DbSet<CookerAPI.Models.Black_Item> Black_Items { get; set; }

        public System.Data.Entity.DbSet<CookerAPI.Models.Rate> Rates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }


    }
}
