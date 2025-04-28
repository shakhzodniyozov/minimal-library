namespace MinimalApi.Library.Responses;

public enum ResponseErrorCode
{
    UnknownError = 1,
    ServiceUnavailable = 2,
    ApiDeprecated = 3,
    ResourceNotFound = 4,
    ErrorAuth = 5,
    Forbidden = 6,
    TokenExpired = 7,
    TooManyRequests = 8,
    NeedToUpgrade = 9,
    Timeout = 10,
    BadRequest = 12,
    DuplicateRequest = 13,
    LimitExceeded = 14,
    UserTemporarilyBlocked = 15,
    TokenRefreshExpired = 16,
    UnhandledException = 17,
    AdminUserNotFound = 18
}