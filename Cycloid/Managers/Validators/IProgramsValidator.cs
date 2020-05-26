using Cycloid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cycloid.Managers.Validators
{
    public interface IProgramsValidator
    {
        void ValidateGetById(Operation<Program> operation, string id);
    }
}
