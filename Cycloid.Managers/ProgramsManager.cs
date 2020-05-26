using Cycloid.Managers.Validators;
using Cycloid.Models;
using Cycloid.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cycloid.Managers
{
    public class ProgramsManager : IProgramsManager
    {
        private readonly IProgramsService _programsService;
        private readonly IProgramsValidator _programsValidator;
        public ProgramsManager(IProgramsService programsService, IProgramsValidator programsValidator)
        {
            _programsService = programsService;
            _programsValidator = programsValidator;
        }

        public async Task<Operation<List<Program>>> GetByChannel(string channelId, int skip, int take)
        {
            Operation<List<Program>> op = new Operation<List<Program>>
            {
                ErrorMessages = new List<string>(),
                IsValid = true
            };


            var programs = await _programsService.GetAllProgramsAsync();
            var programsRes = programs.Where(p => p.ChannelId == channelId).Skip(skip).Take(take);

            if (programsRes == null || !programsRes.Any())
            {
                op.ErrorMessages.Add("No programs found");
                return op;
            }

            op.Payload = programsRes.ToList();

            return op;
                

        }

        public async Task<Operation<Program>> GetByIdAsync(string id)
        {
            Operation<Program> op = new Operation<Program>();

            _programsValidator.ValidateGetById(op, id);

            if (!op.IsValid)
                return op;

            var programs = await _programsService.GetAllProgramsAsync();
            var program = programs.Where(p => p.Id == id).FirstOrDefault();

            if (program == null)
            {
                op.ErrorMessages.Add("No program found");
                return op;
            }

            op.Payload = program;
            return op;

        }

   

           



    }
}
