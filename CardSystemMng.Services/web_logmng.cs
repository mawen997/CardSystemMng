
using CardSystemMng.IRepository;
using CardSystemMng.IServices;
using CardSystemMng.Model.Models;
using CardSystemMng.Services.BASE;

namespace CardSystemMng.Services
{
    public partial class web_logmngServices : BaseServices<web_logmng>, Iweb_logmngServices
    {
        Iweb_logmngRepository _dal;
        public web_logmngServices(Iweb_logmngRepository dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }

    }
}
                    