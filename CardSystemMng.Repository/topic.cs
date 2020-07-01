
using CardSystemMng.IRepository;
using CardSystemMng.IRepository.BASE;
using CardSystemMng.Model.Models;
using CardSystemMng.Repository.BASE;

namespace CardSystemMng.Repository
{
	/// <summary>
	/// topicRepository
	/// </summary>
    public class topicRepository : BaseRepository<Topic>, ItopicRepository
    {
        public topicRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
                    