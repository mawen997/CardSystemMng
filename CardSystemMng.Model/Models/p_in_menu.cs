
using System;
using System.Linq;
using System.Text;
using SqlSugar;


namespace CardSystemMng.Model.Models
{
    
        ///<summary>
    ///
    ///</summary>
    [SugarTable("p_in_menu")]
    public class p_in_menu
    {
        public p_in_menu()
        {
        }
                   /// <summary>
           /// 注释1:
           /// 默认值:
           /// 值是否可以为空:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int In_MenuID {get;set;}

           /// <summary>
           /// 注释1:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public string MenName {get;set;}

           /// <summary>
           /// 注释1:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public int? MenuLevel {get;set;}

           /// <summary>
           /// 注释1:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public string MenAction {get;set;}

           /// <summary>
           /// 注释1:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public string MenController {get;set;}

           /// <summary>
           /// 注释1:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public int? TLmenuID {get;set;}

           /// <summary>
           /// 注释1:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public string MenIco {get;set;}

           /// <summary>
           /// 注释1:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public bool? IsDelete {get;set;}

           /// <summary>
           /// 注释1:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public int? Sort {get;set;}

           /// <summary>
           /// 注释1:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public bool? IsDisplay {get;set;}

           /// <summary>
           /// 注释1:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public string Description {get;set;}

           /// <summary>
           /// 注释1:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public string LinkUrl {get;set;}

           /// <summary>
           /// 注释1:
           /// 默认值:0
           /// 值是否可以为空:False
           /// </summary>           
           public long MenuTypeNameID {get;set;}

    }
}
                    