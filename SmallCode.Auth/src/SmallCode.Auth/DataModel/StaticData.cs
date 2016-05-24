using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SmallCode.Auth.Models;

namespace SmallCode.Auth.DataModel
{
    public class StaticData
    {
        public static Dictionary<Guid, Dictionary<Guid, string>> UserPrivileges = new Dictionary<Guid, Dictionary<Guid, string>>();

        public static void SetUserPrivileges(Guid Id, AuthContext db)
        {

            var data = from f in db.Functions
                       join rf in db.RoleFunctions on f.Id equals rf.FunctionId
                       join r in db.Roles on rf.RoleId equals r.Id
                       join ur in db.UserRoles on r.Id equals ur.RoleId
                       join u in db.Users on ur.UserId equals u.Id
                       where u.Id == Id
                       select new
                       {
                           f.Url,
                           f.Id
                       };
            Dictionary<Guid, string> urls = new Dictionary<Guid, string>();
            foreach (var item in data)
            {
                urls.Add(item.Id, item.Url);
            }
            UserPrivileges.Add(Id, urls);

        }


    }

}
