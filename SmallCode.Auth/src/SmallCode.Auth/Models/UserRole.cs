using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Auth.Models
{
    /// <summary>
    /// 用户角色表
    /// </summary>
    public class UserRole
    {
        public Guid Id { set; get; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public Guid RoleId { set; get; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatedDate { set; get; }

        /// <summary>
        /// 创建者
        /// </summary>
        public Guid? CreatedBy { set; get; }
    }
}
