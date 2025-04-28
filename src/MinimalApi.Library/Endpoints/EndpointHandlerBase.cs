using System.ComponentModel;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using MinimalApi.Library.Responses;

namespace MinimalApi.Library.Endpoints;

public abstract class EndpointHandlerBase<TResponse> : IEndpointHandlerBase
{
    protected readonly HttpContext? HttpContext;

    protected EndpointHandlerBase(IHttpContextAccessor httpContextAccessor)
    {
        HttpContext = httpContextAccessor.HttpContext;
    }

    protected static Ok<SuccessResponse<TResponse>> SuccessResponse(TResponse? data, Meta? meta = null,
        string? message = null)
    {
        return TypedResults.Ok(new SuccessResponse<TResponse>(data, meta, message));
    }

    protected static Ok<ErrorResponse> ErrorResponse(ResponseErrorCode responseErrorCode, string? message = null,
        IDictionary<string, string[]>? errors = null)
    {
        return TypedResults.Ok(new ErrorResponse(responseErrorCode, message, errors));
    }

    protected T? FromRoute<T>(string paramName)
    {
        if (HttpContext!.Request.RouteValues.TryGetValue(paramName, out var val))
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));

            if (converter.IsValid(val!))
            {
                return (T?)converter.ConvertFrom(val!);
            }
        }

        return default;
    }
}

public abstract class EndpointHandler<TResponse> : EndpointHandlerBase<TResponse>, IEndpointHandler
{
    public EndpointHandler(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
    }

    public abstract Task<IResult> HandleAsync(RequestParameters? requestParameters,
        CancellationToken cancellationToken = default);
}

public abstract class EndpointHandler<TRequest, TResponse> : EndpointHandlerBase<TResponse>, IEndpointHandler<TRequest>
{
    public EndpointHandler(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
    }

    public abstract Task<IResult> HandleAsync(RequestParameters<TRequest> requestParameters,
        CancellationToken cancellationToken);
}