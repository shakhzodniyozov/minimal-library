namespace MinimalApi.Library.Responses;

public abstract class ResponseBase<TData>
{
    protected ResponseBase(bool status, ResponseErrorCode? errorCode = null, TData? data = default,
        Meta? meta = default, string? message = null, IDictionary<string, string[]>? errors = null)
    {
        Status = status;
        Data = data;
        Errors = errors;
        Message = message;
        ErrorCode = errorCode;
        Meta = meta;
    }

    public bool Status { get; }
    public ResponseErrorCode? ErrorCode { get; }
    public string? Message { get; }
    public IDictionary<string, string[]>? Errors { get; }
    public TData? Data { get; }
    public Meta? Meta { get; }
}