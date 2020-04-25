
using CardSystemMng.IRepository;
using CardSystemMng.IRepository.BASE;
using CardSystemMng.Model.Models;
using CardSystemMng.Repository.BASE;

namespace CardSystemMng.Repository
{
	/// <summary>
	/// moduleRepository
	/// </summary>
    public class moduleRepository : BaseRepository<Module>, ImoduleRepository
    {
        public moduleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
                    