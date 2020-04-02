using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceSocialNetworks.ControllerModels;
using VoiceSocialNetworks.Flow.Actions;

namespace VoiceSocialNetworks.Flow.Steps.PublicActions
{
    public class StupidQuestionsAction : IAction
    {
        private readonly Dictionary<string, string> _questionToAnswer = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["как зовут парня алены"] = "парня вашей сестры зовут Андрей",
            ["как зовут парня алены?"] = "парня вашей сестры зовут Андрей",
            ["сколько алене лет"] = "Алене Гавриш 21 год, ещё молодая",
            ["сколько алене лет?"] = "Алене Гавриш 21 год, ещё молодая",
            ["где я учусь"] = "Ты учишься в Минском Государственном Колледже Исскуств",
            ["где я учусь?"] = "Ты учишься в Минском Государственном Колледже Исскуств"
        };
        public bool CanHandle(Request request)
        {
            return _questionToAnswer.ContainsKey(request.Command);
        }

        public Task<Response> Handle(Request request)
        {
            var response = new Response();
            if (_questionToAnswer.TryGetValue(request.Command, out string responseText))
            {
                response.Text = responseText;
            }
            else
            {
                response.Text = "Меня ещё не обучили этому вопросу";
            }

            return Task.FromResult(response);
        }
    }
}
