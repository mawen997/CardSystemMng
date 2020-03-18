using CardSystemMng.IServices.BASE;
using CardSystemMng.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CardSystemMng.IServices
{
    /// <summary>
    /// RoleServices
    /// </summary>	
    public interface IRoleServices : IBaseServices<Role>
    {
        Task<Role> SaveRole(string roleName);
        Task<string> GetRoleNameByRid(int rid);

    }
}
