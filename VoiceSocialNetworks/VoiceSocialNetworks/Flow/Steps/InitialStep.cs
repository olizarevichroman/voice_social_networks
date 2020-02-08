using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceSocialNetworks.Flow.Actions;

namespace VoiceSocialNetworks.Flow.Steps
{
    public class InitialStep : IStep
    {
        public InitialStep()
        {
            Actions = new List<IAction>();
        }
        public IEnumerable<IAction> Actions { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IStep NextStep { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void InitializeSteps()
        {
            throw new NotImplementedException();
        }
    }
}
