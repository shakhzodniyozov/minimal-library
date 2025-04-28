using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MinimalApi.Library.Responses;
using MinimalApi.Library.Validator;

namespace MinimalApi.Library.Endpoints;

public class ValidationFilter<T> : IEndpointFilter where T : class
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var argToValidate = context.Arguments.FirstOrDefault(x => x?.GetType() == typeof(T)) as T;

        var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();

        if (validator is not null)
        {
            ValidationResult validationResult;

            if (validator is AbstractValidatorDynamicRule<T> validatorDynamicRule)
            {
                await validatorDynamicRule.InitDynamicRuleAsync();
                validationResult = await GetValidateResult(validatorDynamicRule, argToValidate!);
            }
            else
            {
                validationResult = await GetValidateResult(validator, argToValidate!);
            }

            if (!validationResult.IsValid)
            {
                return Results.Ok(new ErrorResponse(ResponseErrorCode.BadRequest, null,
                    validationResult.ToDictionary()));
            }
        }

        return await next.Invoke(context);
    }

    private async Task<ValidationResult> GetValidateResult(IValidator<T> validator, T argToValidate)
    {
        return await validator.ValidateAsync(argToValidate);
    }
}