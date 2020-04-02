using System.Collections.Generic;
using VoiceSocialNetworks.Flow.Actions;
using VoiceSocialNetworks.Flow.Steps.PublicActions;

namespace VoiceSocialNetworks.Flow.Layers
{
    public class PublicActionsLayer : ILayer
    {
        public PublicActionsLayer()
        {
            var actions = new List<IAction>
            {
                new HelpAction(),
                new SkillDescriptionAction(),
                new StupidQuestionsAction()
            };
            Actions = actions;
        }
        public IReadOnlyList<IAction> Actions { get; set; }
    }
}
