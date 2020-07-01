
using CardSystemMng.IRepository;
using CardSystemMng.IServices;
using CardSystemMng.Model.Models;
using CardSystemMng.Services.BASE;

namespace CardSystemMng.Services
{
    public partial class topicServices : BaseServices<Topic>, ItopicServices
    {
        ItopicRepository _dal;
        public topicServices(ItopicRepository dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }

    }
}
                    