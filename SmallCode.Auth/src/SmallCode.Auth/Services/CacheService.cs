using SmallCode.Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Auth.Services
{
    public class CacheService : ICacheService
    {
        private readonly AuthContext db;

        public CacheService(AuthContext _conetxt)
        {
            db = _conetxt;
        }

        public static Dictionary<Guid, Dictionary<Guid, string>> UserPrivileges = new Dictionary<Guid, Dictionary<Guid, string>>();

        public void SetUserPrivileges(Guid Id)
        {

            var data = (from f in db.Functions
                        join rf in db.RoleFunctions on f.Id equals rf.FunctionId
                        join r in db.Roles on rf.RoleId equals r.Id
                        join ur in db.UserRoles on r.Id equals ur.RoleId
                        join u in db.Users on ur.UserId equals u.Id
                        where u.Id == Id
                        select new
                        {
                            f.Url,
                            f.Id
                        }).ToList();
            Dictionary<Guid, string> urls = new Dictionary<Guid, string>();
            foreach (var item in data)
            {
                urls.Add(item.Id, item.Url);
            }
            if (UserPrivileges.Keys.Contains(Id))
            {
                UserPrivileges.Remove(Id);
                UserPrivileges.Add(Id, urls);
            }
            else
            {
                UserPrivileges.Add(Id, urls);
            }
        }

        public void SetUserPrivilegesByRole(Guid id)
        {
            List<Guid> userIds = db.UserRoles.Where(x => x.RoleId == id).Select(x => x.UserId).ToList();

            foreach (var item in userIds)
            {
                SetUserPrivileges(item);
            }
        }

        public void RemoveUserPrivilegesByUserId(Guid currenUserId)
        {
            if (UserPrivileges.Keys.Contains(currenUserId))
            {
                UserPrivileges.Remove(currenUserId);
            }
        }

    }
}
