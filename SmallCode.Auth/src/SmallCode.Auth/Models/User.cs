using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Auth.Models
{

    /// <summary>
    /// 用户类
    /// </summary>
    public class User
    {
        public Guid Id { set; get; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { set; get; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { set; get; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserNum { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public SexEnum Sex { set; get; }

        /// <summary>
        /// 职位
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 正式姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { set; get; }

        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { set; get; }

        /// <summary>
        /// 部门 
        /// </summary>
        public Guid DepartmentId { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatedDate { set; get; }

        /// <summary>
        /// 创建者
        /// </summary>
        public Guid? CreatedBy { set; get; }

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

        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime? LastLoginDate { set; get; }

        /// <summary>
        /// 上次登录IP
        /// </summary>
        public string LastLoginIP { set; get; }

    }
}
