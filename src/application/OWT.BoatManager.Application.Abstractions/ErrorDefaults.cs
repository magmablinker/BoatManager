namespace OWT.BoatManager.Application.Abstractions;
public static class ErrorDefaults
{
    public static class Generic
    {
        public static Error NotFound() => new($"{nameof(Generic)}.{nameof(NotFound)}");
        public static Error InternalServerError() => new($"{nameof(Generic)}.{nameof(InternalServerError)}");
        public static Error Forbidden() => new($"{nameof(Generic)}.{nameof(Forbidden)}");
        public static Error Unauthorized() => new($"{nameof(Generic)}.{nameof(Unauthorized)}");
        public static Error Conflict() => new($"{nameof(Generic)}.{nameof(Conflict)}");
        public static Error UnprocessableEntity() => new($"{nameof(Generic)}.{nameof(UnprocessableEntity)}");
        public static Error BadRequest() => new($"{nameof(Generic)}.{nameof(BadRequest)}");
    }
}