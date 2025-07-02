using Application.DTO;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Http;

namespace Application.Validators.CRUD
{

    public class UploadFileValidator : AbstractValidator<IFormFile>
    {
        public UploadFileValidator()
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x.Length).NotEqual(0);
        }


    }
}
