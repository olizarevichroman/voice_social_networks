using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }
    }
}
