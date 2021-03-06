﻿
using System;
using System.Linq;
using System.Text;
using SqlSugar;


namespace CardSystemMng.Model.Models
{
    
        ///<summary>
    ///
    ///</summary>
    [SugarTable("rolemodulepermission")]
    public class RoleModulePermission
    {
        public RoleModulePermission()
        {
        }
                   /// <summary>
           /// 注释1:
           /// 默认值:
           /// 值是否可以为空:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// 注释1:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public bool? IsDeleted {get;set;}

           /// <summary>
           /// 注释1:
           /// 默认值:
           /// 值是否可以为空:False
           /// </summary>           
           public int RoleId {get;set;}

           /// <summary>
           /// 注释1:
           /// 默认值:
           /// 值是否可以为空:False
           /// </summary>           
           public int ModuleId {get;set;}

           /// <summary>
           /// 注释1:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public int? PermissionId {get;set;}

           /// <summary>
           /// 注释1:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public int? CreateId {get;set;}

           /// <summary>
           /// 注释1:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public string CreateBy {get;set;}

           /// <summary>
           /// 注释1:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public DateTime? CreateTime {get;set;}

           /// <summary>
           /// 注释1:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public int? ModifyId {get;set;}

           /// <summary>
           /// 注释1:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public string ModifyBy {get;set;}

           /// <summary>
           /// 注释1:
           /// 默认值:
           /// 值是否可以为空:True
           /// </summary>           
           public DateTime? ModifyTime {get;set;}
        // 下边三个实体参数，只是做传参作用，所以忽略下
        [SugarColumn(IsIgnore = true)]
        public Role Role { get; set; }
        [SugarColumn(IsIgnore = true)]
        public Module Module { get; set; }
        [SugarColumn(IsIgnore = true)]
        public Permission Permission { get; set; }
    }
}
                    