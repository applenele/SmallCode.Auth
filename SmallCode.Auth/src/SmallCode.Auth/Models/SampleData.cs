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

                Role role = new Role
                {
                    CreatedBy = user.Id,
                    CreatedDate = DateTime.Now,
                    IsDelete = false,
                    RoleName = "root",
                };

                db.Roles.Add(role);
                await db.SaveChangesAsync();

                UserRole userRole = new UserRole
                {
                    RoleId = role.Id,
                    UserId = user.Id,
                    CreatedBy = user.Id,
                    CreatedDate = DateTime.Now,
                };

                db.UserRoles.Add(userRole);
                await db.SaveChangesAsync();

                List<Function> functions = new List<Function>();
                Guid SysId = Guid.NewGuid();
                Guid FuncId = Guid.NewGuid();
                Function function1 = new Function
                {
                    FunctionName = "系统管理",
                    CreatedBy = user.Id,
                    CreatedDate = DateTime.Now,
                    FunctionType = FunctionType.View,
                    FunctionCode = "100",
                    Id = SysId,
                    Url = "#",
                    IsDelete = false,
                };

                Function function2 = new Function
                {
                    IsDelete = false,
                    Url = "/Function/Index",
                    FunctionCode = "40001",
                    FatherId = FuncId,
                    FunctionType = FunctionType.View,
                    CreatedBy = user.Id,
                    CreatedDate = DateTime.Now,
                    FunctionName = "功能管理",
                };

                Function function3 = new Function
                {
                    IsDelete = false,
                    Url = "/Function/Edit",
                    FunctionCode = "40002",
                    FatherId = FuncId,
                    FunctionType = FunctionType.View,
                    CreatedBy = user.Id,
                    CreatedDate = DateTime.Now,
                    FunctionName = "功能编辑",
                };

                Function function4 = new Function
                {
                    IsDelete = false,
                    FunctionCode = "40000",
                    FatherId = SysId,
                    Id = FuncId,
                    FunctionType = FunctionType.View,
                    CreatedBy = user.Id,
                    CreatedDate = DateTime.Now,
                    FunctionName = "功能管理",
                    Url = "#"
                };


                functions.Add(function1);
                functions.Add(function2);
                functions.Add(function3);
                functions.Add(function4);

                db.Functions.AddRange(functions);
                await db.SaveChangesAsync();

            }
        }
    }
}
