using Cycloid.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cycloid.Services
{
    public interface IProgramsService
    {
        Task<List<Program>> GetAllProgramsAsync();
    }
}
