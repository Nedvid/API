using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CookerAPI.DB
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext() : base("AuthContext")
        {
           
        }
    }
}