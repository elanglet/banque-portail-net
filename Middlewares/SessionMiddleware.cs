namespace BanqueNET.Middlewares
{
    public class SessionMiddleware
    {
        private RequestDelegate _next;

        public SessionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {

            var leclient = httpContext.Session.Get("leclient");
            // En ASP .NET Core - Insuffisant... Mais pas de gestion manuelle possible !
            httpContext.Session.Clear();
            // En ASP .NET 
            // httpContext.Session.Abandon();

            if(leclient != null)
            {
                httpContext.Session.Set("leclient", leclient);
            }

            return _next(httpContext);
        }
    }

    public static class SessionMiddlewareExtensions
    {
        public static IApplicationBuilder UseSessionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SessionMiddleware>();
        }
    }
}