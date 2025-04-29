using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Library.Responses;

namespace MinimalApi.Library.Endpoints;

public abstract class EndpointBase<TRequest, TResponse> : EndpointBase<TResponse>
    where TResponse : class where TRequest : class
{
    protected RouteHandlerBuilder Post(IEndpointRouteBuilder app, [StringSyntax("Route")] string pattern,
        Delegate handler)
    {
        return base.Post(app, pattern, handler).AddEndpointFilter<ValidationFilter<TRequest>>();
    }

    protected RouteHandlerBuilder Get(IEndpointRouteBuilder app, [StringSyntax("Route")] string pattern,
        Delegate handler)
    {
        return base.Get(app, pattern, handler).AddEndpointFilter<ValidationFilter<TRequest>>();
    }
}

public abstract class EndpointBase<TResponse> : IEndpoint where TResponse : class
{
    public abstract void AddRoute(IEndpointRouteBuilder app);

    protected RouteHandlerBuilder Post(IEndpointRouteBuilder app, [StringSyntax("Route")] string pattern,
        Delegate handler)
    {
        return app.MapPost(pattern, handler)
            .Produces<SuccessResponse<TResponse>>()
            .Produces<ErrorResponse>();
    }

    protected RouteHandlerBuilder Get(IEndpointRouteBuilder app, [StringSyntax("Route")] string pattern,
        Delegate handler)
    {
        return app.MapGet(pattern, handler)
            .Produces<SuccessResponse<TResponse>>()
            .Produces<ErrorResponse>();
    }
}