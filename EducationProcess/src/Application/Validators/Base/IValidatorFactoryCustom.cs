using FluentValidation;

namespace Application.Validators.Base
{
    public interface IValidatorFactoryCustom
    {
        IValidator<T> GetValidator<T>() where T : class;
    }
}
