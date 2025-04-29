using Microsoft.AspNetCore.Http;

namespace MinimalApi.Library.Endpoints;

public interface IEndpointHandler : IEndpointHandlerBase
{
    Task<IResult> HandleAsync(CancellationToken cancellationToken = default);
}

public interface IEndpointHandler<TRequest> : IEndpointHandlerBase
{
    Task<IResult> HandleAsync(CancellationToken cancellationToken = default);
}