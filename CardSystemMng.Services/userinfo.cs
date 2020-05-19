
using CardSystemMng.IRepository;
using CardSystemMng.IServices;
using CardSystemMng.Model.Models;
using CardSystemMng.Services.BASE;

namespace CardSystemMng.Services
{
    public partial class userinfoServices : BaseServices<userinfo>, IuserinfoServices
    {
        IuserinfoRepository _dal;
        public userinfoServices(IuserinfoRepository dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }

    }
}
                    