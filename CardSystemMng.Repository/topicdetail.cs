
using CardSystemMng.IRepository;
using CardSystemMng.IRepository.BASE;
using CardSystemMng.Model.Models;
using CardSystemMng.Repository.BASE;

namespace CardSystemMng.Repository
{
	/// <summary>
	/// topicdetailRepository
	/// </summary>
    public class topicdetailRepository : BaseRepository<topicdetail>, ItopicdetailRepository
    {
        public topicdetailRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
                    