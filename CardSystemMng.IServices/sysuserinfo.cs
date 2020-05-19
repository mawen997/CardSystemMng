
using CardSystemMng.IServices.BASE;
using CardSystemMng.Model.Models;
using System.Threading.Tasks;

namespace CardSystemMng.IServices
{
    /// <summary>
    /// IsysuserinfoServices
    /// </summary>	
    public interface IsysuserinfoServices : IBaseServices<sysUserInfo>
    {
        Task<sysUserInfo> SaveUserInfo(string loginName, string loginPwd);
        Task<string> GetUserRoleNameStr(string loginName, string loginPwd);
    }
}
                    