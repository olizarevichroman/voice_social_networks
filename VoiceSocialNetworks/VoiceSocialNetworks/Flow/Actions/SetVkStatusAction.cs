using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using VoiceSocialNetworks.ControllerModels;
using VoiceSocialNetworks.DataLayer.Models;
using VoiceSocialNetworks.SDK.Clients;

namespace VoiceSocialNetworks.Flow.Actions
{
    public class SetVkStatusAction : IAction
    {
        private readonly IVkClient _vkClient;
        private readonly VkUser _user;
        public SetVkStatusAction(IVkClient vkClient, VkUser user)
        {
            _vkClient = vkClient;
            _user = user;
        }
        public bool CanHandle(Request request)
        {
            return request.Command.StartsWith("установи статус", StringComparison.OrdinalIgnoreCase);
        }

        public async Task<Response> Handle(Request request)
        {
            Console.WriteLine($"In {nameof(GetVkStatusAction)} execution of {nameof(Handle)} method with vkUser {JsonConvert.SerializeObject(_user)}");
            var status = await _vkClient.SetStatus(_user.AccessToken, request.Command.Replace("установи статус", string.Empty).Trim());

            return new Response
            {
                Text = status
            };
        }
    }
}