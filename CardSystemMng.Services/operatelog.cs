
using CardSystemMng.IRepository;
using CardSystemMng.IServices;
using CardSystemMng.Model.Models;
using CardSystemMng.Services.BASE;

namespace CardSystemMng.Services
{
    public partial class operatelogServices : BaseServices<operatelog>, IoperatelogServices
    {
        IoperatelogRepository _dal;
        public operatelogServices(IoperatelogRepository dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }

    }
}
                    