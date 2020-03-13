using System;
using System.Collections.Generic;
using System.Text;

namespace CardSystemMng.Common.Attributes
{
    /// <summary>
    /// 这个过滤器就是使用的时候进行验证，把它添加到要缓存数据的方法中，即可完成缓存的有效时间的操作。
    /// </summary>
    [AttributeUsage(AttributeTargets.Method,Inherited =true)]
   public class CachingAttribute:Attribute
    {
        /// <summary>
        /// 缓存绝对过期时间（分钟）
        /// </summary>
        public int AbsoluteExpiration { get; set; } = 30;
    }
}
