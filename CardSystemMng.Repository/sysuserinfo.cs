
using CardSystemMng.IRepository;
using CardSystemMng.IRepository.BASE;
using CardSystemMng.Model.Models;
using CardSystemMng.Repository.BASE;

namespace CardSystemMng.Repository
{
	/// <summary>
	/// sysuserinfoRepository
	/// </summary>
    public class sysuserinfoRepository : BaseRepository<sysUserInfo>, IsysuserinfoRepository
    {
        public sysuserinfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
                    