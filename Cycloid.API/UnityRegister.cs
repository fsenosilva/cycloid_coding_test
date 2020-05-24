using Cycloid.Managers;
using Cycloid.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace Cycloid.API
{
    public static class UnityRegister
    {
        public static void RegisterAll(IUnityContainer container)
        {
            #region Services
            container.RegisterType<IChannelsService, ChannelsWcfService>();
            container.RegisterType<IProgramsService, ProgramsRestService>();
            #endregion

            #region Managers
            container.RegisterType<IChannelsManager, ChannelsManager>();
            container.RegisterType<IDeviceManager, DeviceManager>();
            container.RegisterType<IEventsManager, EventsManager>();
            container.RegisterType<IProgramsManager, ProgramsManager>();
            #endregion

            #region Repositories
            container.RegisterType<IDeviceManager, DeviceManager>();
            #endregion
        }
    }
}