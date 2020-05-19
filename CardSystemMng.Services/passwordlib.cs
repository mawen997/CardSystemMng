
using CardSystemMng.IRepository;
using CardSystemMng.IServices;
using CardSystemMng.Model.Models;
using CardSystemMng.Services.BASE;

namespace CardSystemMng.Services
{
    public partial class passwordlibServices : BaseServices<passwordlib>, IpasswordlibServices
    {
        IpasswordlibRepository _dal;
        public passwordlibServices(IpasswordlibRepository dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }

    }
}
                    