using Cycloid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cycloid.Managers.Validators
{
    public class ProgramsValidator : IProgramsValidator
    {
        public void ValidateGetById(Operation<Program> operation, string id)
        {
            if (operation.ErrorMessages == null)
                operation.ErrorMessages = new List<string>();

            Guid guid;
            bool guidResTrue = Guid.TryParse(id, out guid);

            if (!guidResTrue)
                operation.ErrorMessages.Add("Invalid program Id");

            operation.IsValid = !operation.ErrorMessages.Any();
        }
    }
}
