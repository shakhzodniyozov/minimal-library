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
}

public abstract class EndpointHandler<TResponse> : EndpointHandlerBase<TResponse>, IEndpointHandler
{
    public RequestParameters RequestParameters { get; set; } = new();
    public abstract Task<IResult> HandleAsync(CancellationToken cancellationToken = default);

    protected T FromRoute<T>(string paramname)
    {
        if (RequestParameters.RouteParameters!.TryGetValue(paramname.ToLower(), out var value))
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));

            if (converter.IsValid(value!))
            {
                return (T?)converter.ConvertFrom(value!);
            }
        }

        return default;
    }
    
    protected T FromQuery<T>(string paramname)
    {
        if (RequestParameters.QueryParameters!.TryGetValue(paramname, out var value))
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));

            if (converter.IsValid(value!))
            {
                return (T?)converter.ConvertFrom(value!);
            }
        }

        return default;
    }
}

public abstract class EndpointHandler<TRequest, TResponse> : EndpointHandlerBase<TResponse>, IEndpointHandler<TRequest>
{
    public RequestParameters<TRequest> RequestParameters { get; set; } = new();
    public abstract Task<IResult> HandleAsync(CancellationToken cancellationToken = default);
}