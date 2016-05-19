using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Auth.Models
{
    /// <summary>
    /// 部门
    /// </summary>
    public class Department
    {
        public Guid Id { set; get; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { set; get; }

        /// <summary>
        /// 创建者的Id
        /// </summary>
        public Guid CreatedBy { set; get; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyDate { set; get; }

        /// <summary>
        /// 修改者
        /// </summary>
        public Guid? ModifyBy { set; get; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { set; get; }
    }
}
