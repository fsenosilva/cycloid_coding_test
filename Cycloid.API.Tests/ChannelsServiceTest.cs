using System;
using System.Linq;
using Cycloid.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cycloid.API.Tests
{
    [TestClass]
    public class ChannelsServiceTest
    {
        private IChannelsService _channelsService;

        [TestInitialize]
        public void Initialize()
        {
            _channelsService = new ChannelsWcfService();
        }

        [TestMethod]
        public void GetChannels_ShouldRetrieveChannels()
        {
            var channels = _channelsService.GetChannelsAsync().Result;

            Assert.IsTrue(channels.Any());
        }

        [TestMethod]
        public void GetChannels_ShouldRetrieveChannelsByDeviceId()
        {
            var channelIds = _channelsService.GetSubscribedChannelIdsAsync("device-001").Result;

            Assert.IsTrue(channelIds.Any());
        }

        [TestMethod]
        public void GetChannels_ShouldNotRetrieveAnyChannelByDeviceId()
        {
            bool exception = false;

            try 
            {
                _channelsService.GetSubscribedChannelIds("Invalid"); 
            }
            catch
            {
                exception = true;
            }
            

            Assert.IsTrue(exception);
        }
    }
}
