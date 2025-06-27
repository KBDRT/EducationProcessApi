using EducationProcessAPI.Application.ServiceUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationProcessAPI.Application.Services.Helpers.Definition
{
    public interface IOperationResultService
    {
        public AppOperationStatus GetStatus(Guid id);
    }
}
