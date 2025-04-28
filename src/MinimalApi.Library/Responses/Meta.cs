namespace MinimalApi.Library.Responses;

public class Meta
{
    public object? Pagination { get; set; }
    public MetaFile? File { get; set; }
}

public class MetaFile
{
    public string? Url { get; set; }
    public string? Token { get; set; }
}