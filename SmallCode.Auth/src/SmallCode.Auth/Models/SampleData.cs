using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SmallCode.Auth.Helper;

namespace SmallCode.Auth.Models
{
    public class SampleData
    {
        public async static Task InitDB(IServiceProvider service)
        {
            var db = service.GetService<AuthContext>();

            if (db.Database != null && db.Database.EnsureCreated())
            {
                User user = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "admin",
                    Password = "123456".ToMD5Hash(),
                    CreatedDate = DateTime.Now
                };

                db.Users.Add(user);
                await db.SaveChangesAsync();
            }
        }
    }
}
