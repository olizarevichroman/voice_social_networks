using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceSocialNetworks.DataLayer.Models;

namespace VoiceSocialNetworks.SDK.Clients
{
    public interface IYandexClient
    {
        Task<User> GetUser(string oauthToken);
    }
}
