
using CardSystemMng.IRepository;
using CardSystemMng.IRepository.BASE;
using CardSystemMng.Model.Models;
using CardSystemMng.Repository.BASE;

namespace CardSystemMng.Repository
{
	/// <summary>
	/// p_in_menuRepository
	/// </summary>
    public class p_in_menuRepository : BaseRepository<p_in_menu>, Ip_in_menuRepository
    {
        public p_in_menuRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
                    