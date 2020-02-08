using System.Threading.Tasks;
using VoiceSocialNetworks.ControllerModels;

namespace VoiceSocialNetworks.Flow.Actions
{
    public interface IAction
    {
        bool CanHandle(RequestWrapper request);

        Task<ResponseWrapper> Handle(RequestWrapper request);
    }
}
