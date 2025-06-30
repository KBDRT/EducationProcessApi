using Application.Exceptions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Validators.Base
{
    public class ValidatorFactory : IValidatorFactoryCustom
    {

        private readonly IServiceProvider _serviceProvider;

        public ValidatorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IValidator<T> GetValidator<T>() where T : class
        {
            try
            {
                return _serviceProvider.GetRequiredService<IValidator<T>>();
            }
            catch
            {
                throw new ValidatorFactoryException("Validator problem", typeof(T));
            }
        }
    }
}
