
using CardSystemMng.IRepository;
using CardSystemMng.IServices;
using CardSystemMng.Model.Models;
using CardSystemMng.Services.BASE;

namespace CardSystemMng.Services
{
    public partial class roleinfoServices : BaseServices<roleinfo>, IroleinfoServices
    {
        IroleinfoRepository _dal;
        public roleinfoServices(IroleinfoRepository dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }

    }
}
                    