using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Auth.Filters
{
    /// <summary>
    /// 通过特性注入
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class InjectAttribute : Attribute, IBindingSourceMetadata
    {
        public BindingSource BindingSource => BindingSource.Services;
    }
}
