using FluentValidation;
using OnlineExamSystem.Common.Contracts.Services;
using OnlineExamSystem.Common.Dtos;
using OnlineExamSystem.Common.Validators;
using Exceptions = OnlineExamSystem.Common.Exceptions;


namespace OnlineExamSystem.Services.Services;
public class ValidationService : IValidationService
{
    private readonly IDictionary<Type, Type> _validators;
    private readonly IServiceProvider _serviceProvider;

    public ValidationService(IServiceProvider serviceProvider)
    {
        this._serviceProvider = serviceProvider;
        this._validators = new Dictionary<Type, Type>
            {
                { typeof(AddRoleRequest), typeof(AddRoleValidator) },
                { typeof(AddUserToRoleDto), typeof(AddUserToRoleValidator) },
                { typeof(CreateUserDto), typeof(CreateUserValidator) },
                { typeof(string), typeof(DeleteRoleValidator) },
                { typeof(GetUserRolesDto), typeof(GetUserRolesValidator) },
                { typeof(DeleteUserfromRoleDto), typeof(DeleteUserFromRoleValidator) },
                { typeof(DeleteUserDto), typeof(DeleteUserValidator) },
                { typeof(LoginDto), typeof(LoginDtoValidator) },
            };
    }

    private IValidator<T> GetValidator<T>()
    {
        var modelType = typeof(T);
        var hasValidator = this._validators.ContainsKey(modelType);
        if (hasValidator == false)
        {
            throw new Exception("Missing validator");
        }

        var validatorType = this._validators[modelType];
        var validator = _serviceProvider.GetService(validatorType) as IValidator<T>;
        return validator;
    }

    public async Task EnsureValid<T>(T model)
    {
        var validator = this.GetValidator<T>();
        var result = await validator.ValidateAsync(model);
        if (!result.IsValid)
            throw new Exceptions.ValidationException(result);
    }
}