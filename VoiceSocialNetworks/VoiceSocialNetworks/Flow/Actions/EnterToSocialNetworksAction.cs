using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceSocialNetworks.ControllerModels;

namespace VoiceSocialNetworks.Flow.Actions
{
    public class EnterToSocialNetworksAction : IAction
    {
        private readonly List<string> _socialNetworks = new List<string>
        {
            "Slack", "Слэк", "Слак"
        };

        private readonly List<string> _activationPhrases = new List<string>
        {
            "Зайти в", "Перейди в", "Войди в"
        };
        public bool CanHandle(RequestWrapper request)
        {
            return _activationPhrases.Any(phrase =>
            {
                return request.Request.Command.StartsWith(phrase, StringComparison.OrdinalIgnoreCase);
            });
        }

        public Task<ResponseWrapper> Handle(RequestWrapper request)
        {
            throw new NotImplementedException();
        }
    }
}
