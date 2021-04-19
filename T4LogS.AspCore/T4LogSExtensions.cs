using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Internal;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using T4LogS.Core;

namespace T4LogS.AspCore
{
    public class T4LogSExtensions
    {
        public static Task<Core.T4LogSWriteException> Execute(HttpContext httpContext)
        {
            return T4LogSExtensions.Execute(httpContext, "");
        }

        public static Task<Core.T4LogSWriteException> Execute(HttpContext httpContext, string description)
        {
            return Task.Run(() =>
            {
                var exceptionFeature = httpContext.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();
                if (exceptionFeature != null)
                {
                    using (Core.T4LogSWriteException log = new Core.T4LogSWriteException(exceptionFeature.Error, T4LogSType.Error, description))
                    {
                        if (!string.IsNullOrEmpty(exceptionFeature.Path))
                        {
                            log.AppendDetail(new Core.T4LogSErrorDetail()
                            {
                                TargetName = "Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature",
                                Name = "Path",
                                Value = exceptionFeature.Path,
                            });
                        }
                        if (httpContext.Request != null)
                        {
                            foreach (PropertyInfo item in httpContext.Request.GetType().GetProperties())
                            {
                                if (
                                    item.Name != nameof(HttpContext.Request.Form)
                                    && item.Name != nameof(HttpContext.Request.Query)
                                    && item.Name != nameof(HttpContext.Request.QueryString)
                                    && item.Name != nameof(HttpContext)
                                    && item.Name != nameof(HttpContext.Request.Body)
                                    )
                                {
                                    log.AppendDetail(item.ToT4LogObject(httpContext.Request));
                                }
                                else
                                {
                                    if (httpContext.Request.HasFormContentType && httpContext.Request.Form != null)
                                    {
                                        log.AppendDetail(item.ToT4LogObject(httpContext.Request.Form));
                                        log.AppendDetail(item.ToT4LogObject(httpContext.Request.Query));
                                        log.AppendDetail(item.ToT4LogObject(httpContext.Request.QueryString));
                                    }
                                }
                            }
                        }
                        return log;
                    }
                }
                return null;
            });
        }
    }
}
