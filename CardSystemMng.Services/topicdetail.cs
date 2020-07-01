
using CardSystemMng.IRepository;
using CardSystemMng.IServices;
using CardSystemMng.Model.Models;
using CardSystemMng.Services.BASE;

namespace CardSystemMng.Services
{
    public partial class topicdetailServices : BaseServices<TopicDetail>, ItopicdetailServices
    {
        ItopicdetailRepository _dal;
        public topicdetailServices(ItopicdetailRepository dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }

    }
}
                    