using Cycloid.Models;
using Cycloid.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cycloid.Managers
{
    public class EventsManager : IEventsManager
    {
        private readonly IProgramsService _programsService;
        private readonly IChannelsService _channelsService;
        public EventsManager(IChannelsService channelsService, IProgramsService programsService)
        {
            _channelsService = channelsService;
            _programsService = programsService;
        }

        public async Task<Operation<List<Event>>> GetEventsAsync(string deviceId, string channelId, CancellationToken ct = default(CancellationToken))
        {
            Operation<List<Event>> op = new Operation<List<Event>>()
            {
                IsValid = true,
                ErrorMessages = new List<string>()
            };

            DateTime dtNow = DateTime.Now;

            DateTime start = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 0, 0, 0);
            DateTime end = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 23, 59, 59);

            var subscribedChannels = await _channelsService.GetSubscribedChannelIdsAsync(deviceId);
            var channels = await _channelsService.GetChannelsAsync();
            var channelName = channels.Where(x => x.Id == channelId).Select(x => x.Name).FirstOrDefault();

            bool isSubscribed = subscribedChannels.Any(x => x == channelId);

            var programs = await _programsService.GetAllProgramsAsync();

            var events = programs.Where(x => x.StartTime >= start && x.EndTime <= end).Select(x => CreateEvent(x, isSubscribed, channelName));


            if (!events.Any())
            {
                op.ErrorMessages.Add("No events found");
                return op;
            }

            op.Payload = events.ToList();

            return op;
        }

        public async Task<Operation<List<Event>>> GetPlayingEventsAsync(string deviceId, CancellationToken ct = default(CancellationToken))
        {
            Operation<List<Event>> op = new Operation<List<Event>>()
            {
                IsValid = true,
                ErrorMessages = new List<string>()
            };

            var subscribedChannels = await _channelsService.GetSubscribedChannelIdsAsync(deviceId);

            HashSet<string> subscribedChannelIds = new HashSet<string>(subscribedChannels);

            var channels = await _channelsService.GetChannelsAsync();

            var channelsD = channels.ToDictionary(c => c.Id, c => c.Name);

            var programs = await _programsService.GetAllProgramsAsync();

            List<Event> events = new List<Event>();

            Program prev = null;

            bool isNextToAdd = false;

            DateTime now = DateTime.Now;

            foreach(var program in programs)
            {
                if (isNextToAdd && prev.ChannelId == program.ChannelId)
                {
                    events.Add(CreateEvent(program, subscribedChannelIds, channelsD));
                    isNextToAdd = false;
                    continue;
                }

                if(now >= program.StartTime && now <= program.EndTime)
                {
                    if(prev != null && prev.ChannelId == program.ChannelId)
                        events.Add(CreateEvent(prev, subscribedChannelIds, channelsD));

                    prev = program;

                    events.Add(CreateEvent(program, subscribedChannelIds, channelsD));

                    isNextToAdd = true;

                    continue;
                }

                prev = program;
                isNextToAdd = false;
            }

            if (!events.Any())
            {
                op.ErrorMessages.Add("No events found");
                return op;
            }

            op.Payload = events;
            return op;
        }

        private Event CreateEvent(Program program, HashSet<string> subscribedChannelIds, Dictionary<string,string> channelsD)
        {
            return new Event
            {
               ChannelName = channelsD[program.ChannelId],
               IsSubscribed = subscribedChannelIds.Contains(program.ChannelId),
               ProgramDescription = program.Description,
               ProgramEndTime = program.EndTime,
               ProgramStartTime = program.StartTime,
               ProgramTitle = program.Title
            };
        }

        private Event CreateEvent(Program program, bool isSubscribed, string channelName)
        {
            return new Event
            {
                ChannelName = channelName,
                IsSubscribed = isSubscribed,
                ProgramDescription = program.Description,
                ProgramEndTime = program.EndTime,
                ProgramStartTime = program.StartTime,
                ProgramTitle = program.Title
            };
        }
    }
}
