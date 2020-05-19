
using CardSystemMng.IRepository;
using CardSystemMng.IServices;
using CardSystemMng.Model.Models;
using CardSystemMng.Services.BASE;

namespace CardSystemMng.Services
{
    public partial class p_in_menuServices : BaseServices<p_in_menu>, Ip_in_menuServices
    {
        Ip_in_menuRepository _dal;
        public p_in_menuServices(Ip_in_menuRepository dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }

    }
}
                    