using Ganss.XSS;
using System.Text;

namespace BanqueNET.Middlewares
{
    public class AntiXSSMiddleware
    {
        private RequestDelegate _next;

        public AntiXSSMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext) {
            
            var sanitizer = new HtmlSanitizer();

            var url = httpContext.Request.Path.Value;
            if(!string.IsNullOrEmpty(url))
            {
                var urlNettoyee = sanitizer.Sanitize(url);

                if(url != urlNettoyee)
                {
                    throw new BadHttpRequestException("XSS injection detected in URL.");
                }
            }

            var query = httpContext.Request.QueryString.Value;
            if(!string.IsNullOrEmpty(query))
            {
                var queryNettoyee = sanitizer.Sanitize(query);

                if(query != queryNettoyee)
                {
                    throw new BadHttpRequestException("XSS injection detected in Query String.");
                }
            }

            var buffer = new MemoryStream();
            await httpContext.Request.Body.CopyToAsync(buffer);
            httpContext.Request.Body = buffer;
            buffer.Position = 0;

            var encoding = Encoding.UTF8;

            var requestContent = await new StreamReader(buffer, encoding).ReadToEndAsync();
            httpContext.Request.Body.Position = 0;

            if (IsDangerousString(requestContent, out _)) 
            {
                throw new BadHttpRequestException("XSS injection detected in Query String.");
            }
            await _next.Invoke(httpContext);
        }

        private static bool IsDangerousString(string s, out int matchIndex)
        {
            char[] StartingChars = { '<', '&' };

            //bool inComment = false;
            matchIndex = 0;

            for (var i = 0; ;)
            {

                // Look for the start of one of our patterns 
                var n = s.IndexOfAny(StartingChars, i);

                // If not found, the string is safe
                if (n < 0) return false;

                // If it's the last char, it's safe 
                if (n == s.Length - 1) return false;

                matchIndex = n;

                switch (s[n])
                {
                    case '<':
                        // If the < is followed by a letter or '!', it's unsafe (looks like a tag or HTML comment)
                        if (IsAtoZ(s[n + 1]) || s[n + 1] == '!' || s[n + 1] == '/' || s[n + 1] == '?') return true;
                        break;
                    case '&':
                        // If the & is followed by a #, it's unsafe (e.g. S) 
                        if (s[n + 1] == '#') return true;
                        break;

                }

                // Continue searching
                i = n + 1;
            }
        }

        private static bool IsAtoZ(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }
    }

    public static class AntiXSSMiddlewareExtensions
    {
        public static IApplicationBuilder UseAntiXSSMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AntiXSSMiddleware>();
        }
    }
}