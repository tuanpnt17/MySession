﻿using System.Runtime.CompilerServices;

namespace MySession.Session
{
    public static class MySessionExtension
    {
        private const string MySessionIdName = "MY_SESSION_ID";

        public static ISession GetSession(this HttpContext context)
        {
            var scopedContainer =
                context.RequestServices.GetRequiredService<MySessionScopedContainer>();
            if (scopedContainer.Session != null)
            {
                return scopedContainer.Session;
            }

            var sessionId = context.Request.Cookies[MySessionIdName];
            var sessionStorage = context.RequestServices.GetRequiredService<IMySessionStorage>();
            var session = IsSessionIdValidFormat(sessionId)
                ? sessionStorage.Get(sessionId!)
                : sessionStorage.Create();
            context.Response.Cookies.Append(
                MySessionIdName,
                session.Id,
                new CookieOptions() { HttpOnly = true }
            );

            scopedContainer.Session = session;
            return session;
        }

        private static bool IsSessionIdValidFormat(string? sessionId)
        {
            // check the sessionId not null and sessionId is a guid
            return !string.IsNullOrEmpty(sessionId) && Guid.TryParse(sessionId, out var _);
        }
    }
}
