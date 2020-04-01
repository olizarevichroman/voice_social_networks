using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceSocialNetworks.Flow.Actions;

namespace VoiceSocialNetworks.Flow.Layers
{
    public interface ILayer
    {
        public IReadOnlyList<IAction> Actions { get; set; }

    }
}
