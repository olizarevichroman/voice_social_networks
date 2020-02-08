using VoiceSocialNetworks.DataLayer.Models;

namespace VoiceSocialNetworks.DataLayer.Implementations
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
