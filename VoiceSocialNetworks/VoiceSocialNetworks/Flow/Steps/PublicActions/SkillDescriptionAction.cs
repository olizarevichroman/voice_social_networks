using System;
using System.Linq;
using System.Threading.Tasks;
using VoiceSocialNetworks.ControllerModels;
using VoiceSocialNetworks.Flow.Actions;

namespace VoiceSocialNetworks.Flow.Steps.PublicActions
{
    public class SkillDescriptionAction : IAction
    {
        private readonly string[] _activationPhrases = new string[]
        {
            "Что ты умеешь",
            "Что ты умеешь?",
            "Как пользоваться",
            "Как пользоваться?"
        };
        public bool CanHandle(Request request)
        {
            return _activationPhrases.Contains(request.Command, StringComparer.OrdinalIgnoreCase);
        }

        public Task<Response> Handle(Request request)
        {
            return Task.FromResult(new Response
            {
                Text = "С моей помощью можно управлять социальными сетями с помощью голоса, " +
                "подробности о всех возможных сценариях есть на сайте https://voicesocialnetworks.xyz"
            });
        }
    }
}
