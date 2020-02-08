using VoiceSocialNetworks.DataLayer.Models;

namespace VoiceSocialNetworks.DataLayer.Implementations
{
    public class VkUserRepository : Repository<VkUser>
    {
        public VkUserRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
