
using CardSystemMng.IRepository;
using CardSystemMng.IRepository.BASE;
using CardSystemMng.Model.Models;
using CardSystemMng.Repository.BASE;

namespace CardSystemMng.Repository
{
	/// <summary>
	/// passwordlibRepository
	/// </summary>
    public class passwordlibRepository : BaseRepository<PasswordLib>, IpasswordlibRepository
    {
        public passwordlibRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
                    