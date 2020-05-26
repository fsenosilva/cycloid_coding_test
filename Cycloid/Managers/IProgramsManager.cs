using Cycloid.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cycloid.Managers
{
    public interface IProgramsManager
    {
        Task<Operation<Program>> GetByIdAsync(string id);
        Task<Operation<List<Program>>> GetByChannel(string channelId, int skip, int take);
    }
}
