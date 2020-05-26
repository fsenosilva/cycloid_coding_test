
using Cycloid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cycloid.Managers.Validators
{
    public interface IDeviceValidator
    {
        void ValidateGetDeviceId(Operation<string> operation, string session);
    }
}
