using System.Security.AccessControl;

namespace MinimalApi.Library.Endpoints;

public class RequestParameters<TRequest>
{
    public RequestParameters(TRequest request)
    {
        Request = request;
    }
    
    public List<string>? RouteParameters { get; set; }
    public TRequest? Request { get; set; }
}

public class RequestParameters
{
    public List<string>? RouteParameters { get; set; }
}