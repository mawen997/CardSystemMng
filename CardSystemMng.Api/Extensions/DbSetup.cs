﻿using CardSystemMng.Model.SeedDt;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardSystemMng.Api.Extensions
{
    /// <summary>
    /// Db 启动服务
    /// </summary>
    public static class DbSetup
    {
        public static void AddDbSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddScoped<MyContext>();
        }
    }
}
