using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Auth.Models
{
    /// <summary>
    /// 角色名称对应表
    /// </summary>
    public class RoleFunction
    {
        public Guid Id { get; set; }

        public Guid RoleId { get; set; }

        public Guid FunctionId { get; set; }

        /// 创建时间
        /// </summary>
        public DateTime? CreatedDate { set; get; }

        /// <summary>
        /// 创建者
        /// </summary>
        public Guid? CreatedBy { set; get; }
    }
}
