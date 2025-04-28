using Microsoft.AspNetCore.Routing;

namespace MinimalApi.Library.Endpoints;

public interface IEndpoint
{
    void AddRoute(IEndpointRouteBuilder app);
}