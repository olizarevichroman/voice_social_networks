using System;
using System.Threading.Tasks;
using VoiceSocialNetworks.ControllerModels;
using VoiceSocialNetworks.DataLayer.Models;
using VoiceSocialNetworks.SDK.Clients;

namespace VoiceSocialNetworks.Flow.Actions
{
    public class GetVkStatusAction : IAction
    {
        private readonly IVkClient _vkClient;
        private readonly VkUser _user;
        public GetVkStatusAction(IVkClient vkClient, VkUser user)
        {
            _vkClient = vkClient;
            _user = user;
        }
        public bool CanHandle(Request request)
        {
            return string.Equals(request.Command, "прочитай статус", StringComparison.OrdinalIgnoreCase);
        }

        public async Task<Response> Handle(Request request)
        {
            var status = await _vkClient.GetStatus(_user.AccessToken);

            return new Response
            {
                Text = status
            };
        }
    }
}
