namespace MovieStoreWebAPI.Utilities.Middlewares
{
    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UserCustomExceptionMiddlerware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CustomExceptionMiddlerware>();
        }
    }
}
