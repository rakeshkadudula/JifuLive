using DirectScale.Disco.Extension.Services;

namespace JifuLive.Repositories
{
    public interface IUserRepository
    {
    }
    public class UserRepository
    {
        private readonly IDataService _dataService;

        public UserRepository(IDataService dataService)
        {
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        }
    }
}
