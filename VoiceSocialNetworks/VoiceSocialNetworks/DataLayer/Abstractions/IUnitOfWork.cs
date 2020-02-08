using System.Threading.Tasks;
using VoiceSocialNetworks.DataLayer.Implementations;

namespace VoiceSocialNetworks.DataLayer.Abstractions
{
    public interface IUnitOfWork 
    {
        UserRepository UserRepository { get; }

        VkUserRepository VkUserRepository { get; }
        Task<int> CommitChanges();
    }
}
