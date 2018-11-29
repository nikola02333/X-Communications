using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XCommunications.Business.Models.Data
{
    public class SeedDataBase
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userMenager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                ApplicationUser user = new ApplicationUser()
                {
                    Email = "aaaa@bbbb.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "nikola"
                    
            };
                userMenager.CreateAsync(user, "Password@123");

            }
        }


    }
}
