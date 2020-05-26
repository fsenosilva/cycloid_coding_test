using Cycloid.Managers.Validators;
using Cycloid.Models;
using Cycloid.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cycloid.Managers
{
    public class DeviceManager : IDeviceManager
    {
        private readonly IDevicesRepository _devicesRepository;
        private readonly IDeviceValidator _deviceValidator;
        public DeviceManager(IDevicesRepository devicesRepository, IDeviceValidator deviceValidator)
        {
            _devicesRepository = devicesRepository;
            _deviceValidator = deviceValidator;
        }

        public Operation<string> GetDeviceId(string sessionId)
        {
            Operation<string> op = new Operation<string>
            {
                IsValid = true
            };

            _deviceValidator.ValidateGetDeviceId(op, sessionId);

            if (!op.IsValid)
                return op;

            op.Payload = _devicesRepository.GetDevice(sessionId).Id;

            return op;
        }
    }
}
