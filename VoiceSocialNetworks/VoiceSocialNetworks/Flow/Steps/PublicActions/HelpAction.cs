using System;
using System.Linq;
using System.Threading.Tasks;
using VoiceSocialNetworks.ControllerModels;
using VoiceSocialNetworks.Flow.Actions;

namespace VoiceSocialNetworks.Flow.Steps.PublicActions
{
    public class HelpAction : IAction
    {
        private readonly string[] _activationPhrases = new string[]
        {
            "Помощь"
        };

        public bool CanHandle(Request request)
        {
            return _activationPhrases.Contains(request.Command, StringComparer.OrdinalIgnoreCase);
        }

        public Task<Response> Handle(Request request)
        {
            var response = new Response
            {
                Text = "Чтобы управлять социальными сетями сначала следует привязать аккаунт на сайте" +
                " https://voicesocialnetworks.xyz. После этого можно выполнять вход в социальную сеть через меня"
            };

            return Task.FromResult(response);
        }
    }
}
