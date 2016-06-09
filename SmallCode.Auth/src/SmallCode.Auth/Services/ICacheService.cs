using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Auth.Services
{
    public interface ICacheService
    {
        void SetUserPrivileges(Guid id);

        void SetUserPrivilegesByRole(Guid id);

        void RemoveUserPrivilegesByUserId(Guid id);
    }
}
