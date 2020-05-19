
using CardSystemMng.IRepository;
using CardSystemMng.IRepository.BASE;
using CardSystemMng.Model.Models;
using CardSystemMng.Repository.BASE;

namespace CardSystemMng.Repository
{
	/// <summary>
	/// operatelogRepository
	/// </summary>
    public class operatelogRepository : BaseRepository<operatelog>, IoperatelogRepository
    {
        public operatelogRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
                    