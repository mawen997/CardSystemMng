
using CardSystemMng.IRepository;
using CardSystemMng.IServices;
using CardSystemMng.Model.Models;
using CardSystemMng.Services.BASE;

namespace CardSystemMng.Services
{
    public partial class userroleServices : BaseServices<userrole>, IuserroleServices
    {
        IuserroleRepository _dal;
        public userroleServices(IuserroleRepository dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }

    }
}
                    