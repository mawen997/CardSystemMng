
using CardSystemMng.IRepository;
using CardSystemMng.IRepository.BASE;
using CardSystemMng.Model.Models;
using CardSystemMng.Repository.BASE;

namespace CardSystemMng.Repository
{
	/// <summary>
	/// web_logmngRepository
	/// </summary>
    public class web_logmngRepository : BaseRepository<web_logmng>, Iweb_logmngRepository
    {
        public web_logmngRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
                    