using EducationProcessAPI.Application.Services.Helpers.Definition;
using EducationProcessAPI.Application.ServiceUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationProcessAPI.Application.Services.Helpers.Implementation
{
    public class OperationResultService : IOperationResultService
    {
        public AppOperationStatus GetStatus(Guid id)
        {
            return id == Guid.Empty ? AppOperationStatus.Error : AppOperationStatus.Success;
        }
    }
}
