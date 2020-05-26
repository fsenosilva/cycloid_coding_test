using Cycloid.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cycloid.Managers.Validators
{
    public class DeviceValidator : IDeviceValidator
    {
        private readonly string[] allowedSessions =  new string[] { "session-001", "session-002", "session-003" };
        public void ValidateGetDeviceId(Operation<string> operation, string session)
        {
            if (operation.ErrorMessages == null)
                operation.ErrorMessages = new List<string>();

            if (!allowedSessions.Where(a => a == session).Any())
                operation.ErrorMessages.Add("Invalid Session");

            operation.IsValid = !operation.ErrorMessages.Any();


        }
    }
}
