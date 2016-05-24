using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Auth.Models
{
    /// <summary>
    /// 方法
    /// </summary>
    public class Function
    {
        public Guid Id { set; get; }

        /// <summary>
        /// 功能编号
        /// </summary>
        public string FunctionCode { set; get; }

        /// <summary>
        /// 方法名称
        /// </summary>
        public string FunctionName { set; get; }

        /// <summary>
        ///　对应的URL
        /// </summary>
        public string Url { set; get; }

        /// <summary>
        ///父方法的名称
        /// </summary>
        public Guid? FatherId { set; get; }

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

        public FunctionType FunctionType { set; get; }
    }
}
