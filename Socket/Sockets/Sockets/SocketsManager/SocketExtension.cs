using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Sockets.SocketsManager
{

    public static class SocketExtension
    {
        public static IServiceCollection AddWebSocketManager(this IServiceCollection service)
        {
            service.AddTransient<ConnectionManager>();
            foreach (var type in Assembly.GetEntryAssembly().ExportedTypes)
            {
                if (type.GetTypeInfo().BaseType == typeof(SocketHandler))
                    service.AddSingleton(type);
            }
            return service;
        }

        public static IApplicationBuilder MapSockets(this IApplicationBuilder app, PathString path,
            SocketHandler socket)
        {
            return app.Map(path, (x) => x.UseMiddleware<SocketMiddelware>(socket));
        }
    }
}
