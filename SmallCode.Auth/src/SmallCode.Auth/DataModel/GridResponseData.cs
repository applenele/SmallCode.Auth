using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Auth.DataModel
{
    public class GridResponseData<T>
    {
        public int total { set; get; }

        public List<T> data { set; get; }
    }
}
