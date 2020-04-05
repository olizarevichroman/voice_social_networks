using System.Threading.Tasks;

namespace VoiceSocialNetworks.SDK.Clients
{
    public interface IVkClient
    {
        public Task<string> GetStatus(string accessToken);

        public Task<string> SetStatus(string accessToken);
    }
}
