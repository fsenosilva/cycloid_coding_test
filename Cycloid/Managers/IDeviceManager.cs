using Cycloid.Models;

namespace Cycloid.Managers
{
    public interface IDeviceManager
    {
        Operation<string> GetDeviceId(string sessionId);
    }
}
