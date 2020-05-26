using Cycloid.Common.ParameterBinding;
using Cycloid.Managers;
using Cycloid.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Cycloid.API.Controllers
{
    /// <summary>
    /// The channels controller
    /// </summary>
    [RoutePrefix("v1/channels")]
    public class ChannelsController : BaseController 
    {
        private readonly IChannelsManager _channelsManager;
        private readonly IDeviceManager _deviceManager;

        /// <summary>
        /// The channels controller constructor
        /// </summary>
        /// <param name="channelsManager">The channels manager</param>
        public ChannelsController(IChannelsManager channelsManager, IDeviceManager deviceManager)
        {
            _channelsManager = channelsManager;
            _deviceManager = deviceManager;
        }

        /// <summary>
        /// Gets all channels
        /// </summary>
        /// <returns>The channels</returns>
        [HttpGet]
        [ResponseType(typeof(List<Channel>))]
        [Route("")]
        public async Task<HttpResponseMessage> Get()
        {
            Operation<List<Channel>> op = await _channelsManager.GetAll();
            return CreateResponseFromOperation(op);
        }

        /// <summary>
        /// Gets the subscribed channels
        /// </summary>
        /// <param name="sessionId">The session id</param>
        /// <returns>The subscribed channel ids</returns>
        [HttpGet]
        [ResponseType(typeof(List<Channel>))]
        [Route("subscribed")]
        public async Task<HttpResponseMessage> GetSubscribedChannels([FromHeader("session-id")]string sessionId)
        {
            Operation<string> opDevice = _deviceManager.GetDeviceId(sessionId);

            if (!opDevice.IsValid)
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, opDevice.ErrorMessages);


            Operation<List<Channel>> op = await _channelsManager.GetSubscribedChannels(opDevice.Payload);
            return CreateResponseFromOperation(op);
        }
    }
}
