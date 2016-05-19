using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Auth.Models
{
    /// <summary>
    /// 日志类
    /// </summary>
    public class Log
    {
        public Guid Id { set; get; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { set; get; }

        /// <summary>
        ///日志类型
        /// </summary>
        public LogType LogType { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { set; get; }

        /// <summary>
        /// 客户机Ip
        /// </summary>
        public string Ip { set; get; }
    }
}
