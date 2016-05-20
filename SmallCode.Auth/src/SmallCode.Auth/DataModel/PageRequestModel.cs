using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Auth.DataModel
{
    public class PageRequestModel
    {
        public int pageIndex { set; get; }

        public int pageSize { set; get; }

        public string key { set; get; }

        public string sortField { get; set; }

        public string sortOrder { get; set; }
    }
}
