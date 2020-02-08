using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceSocialNetworks.Flow.Actions;

namespace VoiceSocialNetworks.Flow.Steps
{
    public interface IStep
    {
        IEnumerable<IAction> Actions { get; set; }

        IStep NextStep { get; set; }

        void InitializeSteps();
    }
}
