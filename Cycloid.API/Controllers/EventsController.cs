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
    /// The events controller
    /// </summary>
    [RoutePrefix("v1/events")]
    public class EventsController : BaseController
    {
        private readonly IEventsManager _eventsManager;
        private readonly IDeviceManager _deviceManager;
        /// <summary>
        /// The events controller constructor
        /// </summary>
        /// <param name="eventsManager">The events manager</param>
        public EventsController(IEventsManager eventsManager, IDeviceManager deviceManager)
        {
            _deviceManager = deviceManager;
            _eventsManager = eventsManager;
        }

        /// <summary>
        /// Gets the events by channel id
        /// </summary>
        /// <param name="sessionId">The session id</param>
        /// <param name="channelId">The channel id</param>
        /// <returns>The events</returns>
        [HttpGet]
        [ResponseType(typeof(List<Event>))]
        [Route("{channelId}")]
        public async Task<HttpResponseMessage> GetByChannel([FromHeader("session-id")]string sessionId, [FromUri]string channelId)
        {
            Operation<string> opDevice = _deviceManager.GetDeviceId(sessionId);

            if (!opDevice.IsValid)
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, opDevice.ErrorMessages);

            Operation<List<Event>> op = await _eventsManager.GetEventsAsync(opDevice.Payload, channelId);
            return CreateResponseFromOperation(op);
        }

        /// <summary>
        /// Gets the events playing at the moment
        /// </summary>
        /// <param name="sessionId">The session id</param>
        /// <returns>The events</returns>
        [HttpGet]
        [ResponseType(typeof(List<Event>))]
        [Route("now")]
        public async Task<HttpResponseMessage> GetPlaying([FromHeader("session-id")]string sessionId)
        {
            Operation<string> opDevice = _deviceManager.GetDeviceId(sessionId);
            if (!opDevice.IsValid)
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, opDevice.ErrorMessages);



            Operation<List<Event>> op = await _eventsManager.GetPlayingEventsAsync(opDevice.Payload);
            return CreateResponseFromOperation(op);
        }
    }
}
