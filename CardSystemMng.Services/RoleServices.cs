using CardSystemMng.Common.Attributes;
using CardSystemMng.IRepository;
using CardSystemMng.IServices;
using CardSystemMng.Model.Models;
using CardSystemMng.Services.BASE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSystemMng.Services
{
    /// <summary>
    /// RoleServices
    /// </summary>	
    public class RoleServices : BaseServices<Role>, IRoleServices
    {

        IRoleRepository _dal;
        public RoleServices(IRoleRepository dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<Role> SaveRole(string roleName)
        {
            Role Role = new Role(roleName);
            Role model = new Role();
            var userList = await base.Query(a => a.Name == Role.Name && a.Enabled);
            if (userList.Count > 0)
            {
                model = userList.FirstOrDefault();
            }
            else
            {
                var id = await base.Add(Role);
                model = await base.QueryById(id);
            }

            return model;

        }

        [Caching(AbsoluteExpiration = 30)]
        public async Task<string> GetRoleNameByRid(int rid)
        {
            return ((await base.QueryById(rid))?.Name);
        }
    }
}
