
using System;
using System.Linq;
using System.Text;
using SqlSugar;


namespace CardSystemMng.Model.Models
{
    
        ///<summary>
    ///
    ///</summary>
    [SugarTable("userinfo")]
    public class userinfo
    {
        public userinfo()
        {
        }
                   /// <summary>
           /// 注释:
           /// 默认值:
           /// 值是否可以为空:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public long UserInfoID {get;set;}

           /// <summary>
           /// 注释:登录的用户名
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public string LogUserName {get;set;}

           /// <summary>
           /// 注释:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public string LogUserPassWord {get;set;}

           /// <summary>
           /// 注释:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public string UserName {get;set;}

           /// <summary>
           /// 注释:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public int? Sex {get;set;}

           /// <summary>
           /// 注释:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public string CardID {get;set;}

           /// <summary>
           /// 注释:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public string Birthday {get;set;}

           /// <summary>
           /// 注释:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public string QQ {get;set;}

           /// <summary>
           /// 注释:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public string mail {get;set;}

           /// <summary>
           /// 注释:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public long? RoleInfoID {get;set;}

           /// <summary>
           /// 注释:
           /// 默认值:b'0'
           /// 值是否可以为空:False
           /// </summary>           
           public bool IsEnable {get;set;}

    }
}
                    