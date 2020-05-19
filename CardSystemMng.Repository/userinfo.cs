
using CardSystemMng.IRepository;
using CardSystemMng.IRepository.BASE;
using CardSystemMng.Model.Models;
using CardSystemMng.Repository.BASE;

namespace CardSystemMng.Repository
{
	/// <summary>
	/// userinfoRepository
	/// </summary>
    public class userinfoRepository : BaseRepository<userinfo>, IuserinfoRepository
    {
        public userinfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
                    