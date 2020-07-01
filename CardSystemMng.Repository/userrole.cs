
using CardSystemMng.IRepository;
using CardSystemMng.IRepository.BASE;
using CardSystemMng.Model.Models;
using CardSystemMng.Repository.BASE;

namespace CardSystemMng.Repository
{
	/// <summary>
	/// userroleRepository
	/// </summary>
    public class userroleRepository : BaseRepository<UserRole>, IuserroleRepository
    {
        public userroleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
                    