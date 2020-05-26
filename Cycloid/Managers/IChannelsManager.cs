using Cycloid.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cycloid.Managers
{
    public interface IChannelsManager
    {
        Task<Operation<List<Channel>>> GetAll();
        Task<Operation<List<Channel>>> GetSubscribedChannels(string deviceId);
    }
}
