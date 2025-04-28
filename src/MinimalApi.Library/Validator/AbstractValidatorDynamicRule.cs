using FluentValidation;

namespace MinimalApi.Library.Validator;

public abstract class AbstractValidatorDynamicRule<T> : AbstractValidator<T>
{
    public abstract Task InitDynamicRuleAsync();
}