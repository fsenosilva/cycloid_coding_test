

using Cycloid.Models;
using Cycloid.Services.ChannelWebReference;
using Cycloid.Services.Mapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cycloid.Services
{
    public class ChannelsWcfService : IChannelsService
    {
        private readonly ChannelWebReference.Service1 _service;

        public ChannelsWcfService()
        {

            _service = new ChannelWebReference.Service1();
        
        }

        public async Task<string[]> GetSubscribedChannelIdsAsync(string deviceId)
        {
            var tcs = new TaskCompletionSource<string[]>();
           
            _service.GetSubscribedChannelIdsCompleted += (s, e) =>
            {
                if (e.Error != null)
                    tcs.TrySetException(e.Error);
                else if (e.Cancelled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult(e.Result);
            };

            _service.GetSubscribedChannelIdsAsync(deviceId);

            string[] channelIds = await tcs.Task;

            return channelIds;
        }

        public string[] GetSubscribedChannelIds(string deviceId)
        {


            string[] channelIds = _service.GetSubscribedChannelIds(deviceId);

            return channelIds;
        }

        public async Task<List<Models.Channel>> GetChannelsAsync()
        {
            var tcs = new TaskCompletionSource<ChannelWebReference.Channel[]>();

            _service.GetChannelsCompleted += (s, e) =>
            {
                if (e.Error != null)
                    tcs.TrySetException(e.Error);
                else if (e.Cancelled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult(e.Result);
            };

            _service.GetChannelsAsync();

            ChannelWebReference.Channel[] channels = await tcs.Task;

            return channels.ToChannelList();
        }


    }
}
