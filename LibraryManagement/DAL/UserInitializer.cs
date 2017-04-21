using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using LibraryManagement.Models;

namespace LibraryManagement.DAL
{
    public class UserInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            context.Configuration.LazyLoadingEnabled = true;
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Administrator"))
            {
                roleManager.Create(new IdentityRole("Administrator"));
            }
            if (!roleManager.RoleExists("Manager"))
            {
                roleManager.Create(new IdentityRole("Manager"));
            }
            if (!roleManager.RoleExists("Assistant"))
            {
                roleManager.Create(new IdentityRole("Assistant"));
            }

            var user1 = new ApplicationUser { UserName = "dilipraya", FirstName = "Dilip", LastName = "Raya" };
            var user2 = new ApplicationUser { UserName = "sahilgiri", FirstName = "Sahil", LastName = "Giri" };
            var user3 = new ApplicationUser { UserName = "diwakar", FirstName = "Diwakar", LastName = "Pandey" };
            var user4 = new ApplicationUser { UserName = "rajendra", FirstName = "Rajendra", LastName = "Sapkota" };


            if (userManager.FindByName("dilipraya") == null)
            {
                var result = userManager.Create(user1, "password");

                if (result.Succeeded)
                {
                    userManager.AddToRole(user1.Id, "Administrator");
                    userManager.AddToRole(user1.Id, "Manager");
                }
            }

            if (userManager.FindByName("sahilgiri") == null)
            {
                var result = userManager.Create(user2, "password");

                if (result.Succeeded)
                {
                    userManager.AddToRole(user2.Id, "Manager");
                }
            }

            if (userManager.FindByName("diwakar") == null)
            {
                var result = userManager.Create(user3, "password");

                if (result.Succeeded)
                {
                    userManager.AddToRole(user3.Id, "Assistant");
                }
            }

            if (userManager.FindByName("rajendra") == null)
            {
                var result = userManager.Create(user4, "password");

                if (result.Succeeded)
                {
                    userManager.AddToRole(user4.Id, "Assistant");
                }
            }
            context.SaveChanges();
        }
    }
}