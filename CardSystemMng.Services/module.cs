
using CardSystemMng.IRepository;
using CardSystemMng.IServices;
using CardSystemMng.Model.Models;
using CardSystemMng.Services.BASE;

namespace CardSystemMng.Services
{
    public partial class moduleServices : BaseServices<Module>, ImoduleServices
    {
        ImoduleRepository _dal;
        public moduleServices(ImoduleRepository dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }

    }
}
                    