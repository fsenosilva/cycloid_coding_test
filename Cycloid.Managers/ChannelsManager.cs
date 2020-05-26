using Cycloid.Models;
using Cycloid.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cycloid.Managers
{
    public class ChannelsManager : IChannelsManager
    {
        private readonly IChannelsService _channelsService;
        public ChannelsManager(IChannelsService channelsService)
        {
            _channelsService = channelsService;
        }

        public async Task<Operation<List<Channel>>> GetAll()
        {
            Operation<List<Channel>> op = new Operation<List<Channel>>()
            {
                ErrorMessages = new List<string>(),
                IsValid = true
            };

            var channels = await _channelsService.GetChannelsAsync();

            if (channels == null || !channels.Any())
            {
                op.ErrorMessages.Add("No channels found");
                return op;
            }


            op.Payload = channels.ToList();

            return op;
        }

        public async Task<Operation<List<Channel>>> GetSubscribedChannels(string deviceId)
        {
            Operation<List<Channel>> op = new Operation<List<Channel>>()
            {
                IsValid = true
            };

            var channelIds = await _channelsService.GetSubscribedChannelIdsAsync(deviceId);

            if(channelIds == null || !channelIds.Any())
            {
                op.ErrorMessages.Add("No channels found");
                return op;
            }

            var channels = await _channelsService.GetChannelsAsync();

            var channelsSubscribed =   from a in channels
                                       join b in channelIds
                                       on a.Id equals b
                                       select a;

            op.Payload = channelsSubscribed.ToList();

            return op;
        }
    }
}
