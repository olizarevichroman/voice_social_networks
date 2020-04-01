using System.Threading.Tasks;
using VoiceSocialNetworks.ControllerModels;

namespace VoiceSocialNetworks.Flow.Actions
{
    public interface IAction
    {
        bool CanHandle(Request request);

        Task<Response> Handle(Request request);
    }
}
