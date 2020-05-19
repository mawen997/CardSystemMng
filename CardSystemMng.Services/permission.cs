
using CardSystemMng.IRepository;
using CardSystemMng.IServices;
using CardSystemMng.Model.Models;
using CardSystemMng.Services.BASE;

namespace CardSystemMng.Services
{
    public partial class permissionServices : BaseServices<permission>, IpermissionServices
    {
        IpermissionRepository _dal;
        public permissionServices(IpermissionRepository dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }

    }
}
                    