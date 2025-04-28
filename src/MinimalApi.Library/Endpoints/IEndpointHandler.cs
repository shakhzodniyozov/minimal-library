using Microsoft.AspNetCore.Http;

namespace MinimalApi.Library.Endpoints;

public interface IEndpointHandler : IEndpointHandlerBase
{
    Task<IResult> HandleAsync(RequestParameters? requestParameters, CancellationToken cancellationToken = default);
}

public interface IEndpointHandler<TRequest> : IEndpointHandlerBase
{
    Task<IResult> HandleAsync(RequestParameters<TRequest> requestParameters, CancellationToken cancellationToken = default);
}