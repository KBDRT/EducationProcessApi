using FluentValidation;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1.X509;

namespace Application.CQRS.Analysis.Commands.CreateDocument
{ 
    public class CreateDocumentValidator : AbstractValidator<CreateDocumentCommand>
    {

        public CreateDocumentValidator()
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x.LessonId).NotEqual(Guid.Empty);
            RuleFor(x => x.AuditorName).NotNull();
            RuleFor(x => x.OptionsId).NotNull();
            RuleFor(x => x.ChildrenCount).GreaterThan(0);
        }

    }
}
