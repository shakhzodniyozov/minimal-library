namespace MinimalApi.Library.Endpoints;

public class RequestParameters<TRequest>
{
    public Dictionary<string, string>? RouteParameters { get; set; }
    public TRequest? Request { get; set; }
}

public class RequestParameters
{
    public Dictionary<string, string> RouteParameters { get; set; } = new();
    public Dictionary<string, string> QueryParameters { get; set; } = new();
}