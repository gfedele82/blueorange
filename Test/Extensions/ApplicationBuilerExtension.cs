using Common.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Test.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ApplicationBuilerExtension
    {
        public static void InitializeSwagger(this IApplicationBuilder app, string domian)
        {
            var swaggerversion = SystemParameters.SwaggerVersion;
            app.UseSwagger(
                options =>
                {
                    options.PreSerializeFilters.Add((swagger, httpReq) =>
                    {
                        swagger.Servers = new List<OpenApiServer>()
                        {
                            new OpenApiServer() { Url = $"https://{httpReq.Host}" },
                            new OpenApiServer() { Url = $"http://{httpReq.Host}" },
                        };
                    });
                });

            app.UseSwaggerUI(c => c.SwaggerEndpoint($"/swagger/{swaggerversion}/swagger.json", $"{domian} {swaggerversion}"));       
        }
    }
}
