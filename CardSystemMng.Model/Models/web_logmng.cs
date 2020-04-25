
using System;
using System.Linq;
using System.Text;
using SqlSugar;


namespace CardSystemMng.Model.Models
{
        ///<summary>
    ///网站日志管理
    ///</summary>
    [SugarTable("web_logmng")]
    public class web_logmng
    {
        public web_logmng()
        {
        }
                   /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
        
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
        public long WwebLogID { get; set; }

            
           /// <summary>
           /// Desc:请求的页面地址
           /// Default:0
           /// Nullable:False
           /// </summary>
        
        public string requestUrl { get; set; }

            
           /// <summary>
           /// Desc:请求的页面名称
           /// Default:0
           /// Nullable:False
           /// </summary>
        
        public string requestName { get; set; }

            
           /// <summary>
           /// Desc:1是新制卡，2是补卡，3是邮寄提交和支付，4是注册和身份认证
           /// Default:0
           /// Nullable:True
           /// </summary>
        
        public int? webtype { get; set; }

            
           /// <summary>
           /// Desc:来源IP
           /// Default:
           /// Nullable:True
           /// </summary>
        
        public string FormIP { get; set; }

            
           /// <summary>
           /// Desc:请求时间
           /// Default:
           /// Nullable:True
           /// </summary>
        
        public DateTime? CreateTime { get; set; }

            
           /// <summary>
           /// Desc:请求的内容
           /// Default:
           /// Nullable:True
           /// </summary>
        
        public string requestData { get; set; }

            
           /// <summary>
           /// Desc:返回的内容
           /// Default:
           /// Nullable:True
           /// </summary>
        
        public string responseData { get; set; }

            
    }
}
                    