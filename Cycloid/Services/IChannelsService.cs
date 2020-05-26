using Cycloid.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cycloid.Services
{
    public interface IChannelsService
    {
        Task<string[]> GetSubscribedChannelIdsAsync(string deviceId);
        string[]  GetSubscribedChannelIds(string deviceId);
        Task<List<Channel>> GetChannelsAsync();

    }
}
