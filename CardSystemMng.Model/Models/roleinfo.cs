
using System;
using System.Linq;
using System.Text;
using SqlSugar;


namespace CardSystemMng.Model.Models
{
    
        ///<summary>
    ///
    ///</summary>
    [SugarTable("roleinfo")]
    public class roleinfo
    {
        public roleinfo()
        {
        }
        
		   /// <summary>
           /// 注释:
           /// 默认值:
           /// 值是否可以为空:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public long RoleInfoID {get;set;}


		   /// <summary>
           /// 注释:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public long? RoleMngID {get;set;}


		   /// <summary>
           /// 注释:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public long? UserInfoID {get;set;}


		   /// <summary>
           /// 注释:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public string RoleName {get;set;}


		   /// <summary>
           /// 注释:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public string RoleIcon {get;set;}

    }
}
                    