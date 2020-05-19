
using CardSystemMng.IRepository;
using CardSystemMng.IRepository.BASE;
using CardSystemMng.Model.Models;
using CardSystemMng.Repository.BASE;

namespace CardSystemMng.Repository
{
	/// <summary>
	/// roleinfoRepository
	/// </summary>
    public class roleinfoRepository : BaseRepository<roleinfo>, IroleinfoRepository
    {
        public roleinfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
                    