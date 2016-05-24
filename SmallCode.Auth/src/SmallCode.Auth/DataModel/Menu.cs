using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Auth.DataModel
{
    public class Menu
    {
        public string id { set; get; }

        public string text { set; get; }

        /// <summary>
        /// 父亲节点
        /// </summary>
        public string pid { set; get; }

        /// <summary>
        /// 地址
        /// </summary>
        public string url { set; get; }
    }
}
