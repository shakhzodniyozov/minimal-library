using Microsoft.AspNetCore.Http;

namespace MinimalApi.Library.Endpoints;

public interface IEndpointHandler<TRequest> : IEndpointHandlerBase
{
    Task<IResult> HandleAsync(TRequest requestParameters, CancellationToken cancellationToken = default);
}