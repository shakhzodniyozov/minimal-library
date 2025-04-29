namespace MinimalApi.Library.Endpoints;

public class RequestParameters<TRequest>
{
    public RequestParameters(TRequest request)
    {
        Request = request;
    }
    
    public Dictionary<string, string>? RouteParameters { get; set; }
    public TRequest? Request { get; set; }
}

public class RequestParameters
{
    public Dictionary<string, string>? RouteParameters { get; set; }
}