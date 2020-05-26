using Cycloid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cycloid.Services.Mapper
{
    public static class ToCycloidModel
    {
        public static List<Channel> ToChannelList(this ChannelWebReference.Channel[] channels) 
        {
            return channels.Select(c => c.ToChannel()).ToList();
        }

        public static Channel ToChannel(this ChannelWebReference.Channel channel)
        {
            return new Channel
            {
                Id = channel.Id,
                Name = channel.Name,
                Position = channel.Position,
                Category = channel.Category
            };
        }
    }
}
