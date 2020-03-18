using CardSystemMng.IRepository;
using CardSystemMng.IServices;
using CardSystemMng.Model.Models;
using CardSystemMng.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardSystemMng.Services
{
    /// <summary>
    /// ModulePermissionServices
    /// </summary>	
    public class ModulePermissionServices : BaseServices<ModulePermission>, IModulePermissionServices
    {

        IModulePermissionRepository _dal;
        public ModulePermissionServices(IModulePermissionRepository dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }

    }
}
