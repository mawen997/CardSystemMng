using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.DynamicProxy;
using CardSystemMng.Api.AOP;
using CardSystemMng.Api.Extensions;
using CardSystemMng.Api.Filter;
using CardSystemMng.Api.Middlewares;
using CardSystemMng.Common.Helper;
using CardSystemMng.Common.Hubs;
using CardSystemMng.Common.LogHelper;
using CardSystemMng.Common.Othe;
using CardSystemMng.Common.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using static CardSystemMng.Api.SwaggerHelper.CustomApiVersion;

namespace CardSystemMng.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // ����code�����������в�һ��,�Դ������˷�װ,����鿴�Ҳ� Extensions �ļ���.
            services.AddSingleton<IRedisCacheManager, RedisCacheManager>();
            services.AddSingleton(new Appsettings(Env.ContentRootPath));
            services.AddSingleton(new LogLock(Env.ContentRootPath));

            services.AddMemoryCacheSetup();
            services.AddSqlsugarSetup();
            services.AddDbSetup();
            services.AddAutoMapperSetup();
            services.AddCorsSetup();
            services.AddMiniProfilerSetup();
            services.AddSwaggerSetup();
            services.AddJobSetup();
            services.AddHttpContextSetup();
            services.AddAuthorizationSetup();

            services.AddSignalR().AddNewtonsoftJsonProtocol();

            services.AddScoped<UseServiceDIAttribute>();

            services.AddControllers(o =>
            {
                // ȫ���쳣����
                o.Filters.Add(typeof(GlobalExceptionsFilter));
                // ȫ��·��Ȩ�޹�Լ
               // o.Conventions.Insert(0, new GlobalRouteAuthorizeConvention());
                // ȫ��·��ǰ׺��ͳһ�޸�·��
                o.Conventions.Insert(0, new GlobalRoutePrefixFilter(new RouteAttribute(RoutePrefix.Name)));
            })
            //ȫ������Json���л�����
            .AddNewtonsoftJson(options =>
            {
                //����ѭ������
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //��ʹ���շ���ʽ��key
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //����ʱ���ʽ
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
            });
          
        }
        // ע����CreateDefaultBuilder�У����Autofac���񹤳�
        public void ConfigureContainer(ContainerBuilder builder)
        {
            var basePath = AppContext.BaseDirectory;
            //builder.RegisterType<AdvertisementServices>().As<IAdvertisementServices>();


            #region ���нӿڲ�ķ���ע��


            var servicesDllFile = Path.Combine(basePath, "CardSystemMng.Services.dll");
            var repositoryDllFile = Path.Combine(basePath, "CardSystemMng.Repository.dll");

            if (!(File.Exists(servicesDllFile) && File.Exists(repositoryDllFile)))
            {
                throw new Exception("Repository.dll��service.dll ��ʧ����Ϊ��Ŀ�����ˣ�������Ҫ��F6���룬��F5���У����� bin �ļ��У���������");
            }



            // AOP ���أ������Ҫ��ָ���Ĺ��ܣ�ֻ��Ҫ�� appsettigns.json ��Ӧ��Ӧ true ���С�
            var cacheType = new List<Type>();
            if (Appsettings.app(new string[] { "AppSettings", "RedisCachingAOP", "Enabled" }).ObjToBool())
            {
                builder.RegisterType<RedisCacheAOP>();
                cacheType.Add(typeof(RedisCacheAOP));
            }
            if (Appsettings.app(new string[] { "AppSettings", "MemoryCachingAOP", "Enabled" }).ObjToBool())
            {
                builder.RegisterType<CacheAOP>();
                cacheType.Add(typeof(CacheAOP));
            }
            if (Appsettings.app(new string[] { "AppSettings", "TranAOP", "Enabled" }).ObjToBool())
            {
                builder.RegisterType<TranAOP>();
                cacheType.Add(typeof(TranAOP));
            }
            if (Appsettings.app(new string[] { "AppSettings", "LogAOP", "Enabled" }).ObjToBool())
            {
                builder.RegisterType<LogAOP>();
                cacheType.Add(typeof(LogAOP));
            }

            // ��ȡ Service.dll ���򼯷��񣬲�ע��
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);
            builder.RegisterAssemblyTypes(assemblysServices)
                      .AsImplementedInterfaces()
                      .InstancePerDependency()
                      .EnableInterfaceInterceptors()//����Autofac.Extras.DynamicProxy;
                      .InterceptedBy(cacheType.ToArray());//����������������б�����ע�ᡣ

            // ��ȡ Repository.dll ���򼯷��񣬲�ע��
            var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
            builder.RegisterAssemblyTypes(assemblysRepository)
                   .AsImplementedInterfaces()
                   .InstancePerDependency();

            #endregion

            #region û�нӿڲ�ķ����ע��

            //��Ϊû�нӿڲ㣬���Բ���ʵ�ֽ��ֻ���� Load ������
            //ע�����ʹ��û�нӿڵķ��񣬲������ʹ�� AOP ���أ��ͱ�������Ϊ�鷽��
            //var assemblysServicesNoInterfaces = Assembly.Load("CardSystemMng.Services");
            //builder.RegisterAssemblyTypes(assemblysServicesNoInterfaces);

            #endregion

            #region û�нӿڵĵ����� class ע��

            //ֻ��ע������е��鷽�����ұ�����public
            //builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(Love)))
            //    .EnableClassInterceptors()
            //    .InterceptedBy(cacheType.ToArray());

            #endregion

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            // ��¼���еķ��ʼ�¼
            loggerFactory.AddLog4Net();
            // ��¼�����뷵������ 
            app.UseReuestResponseLog();
            // signalr 
            app.UseSignalRSendMildd();
            // ��¼ip����
            app.UseIPLogMildd();

            #region Environment
            if (env.IsDevelopment())
            {
                // �ڿ��������У�ʹ���쳣ҳ�棬�������Ա�¶�����ջ��Ϣ�����Բ�Ҫ��������������
                app.UseDeveloperExceptionPage();

                //app.Use(async (context, next) =>
                //{
                //    //������ε��ã��������һ�¾��У���Ҫ��ע��
                //    //var blogs =await _blogArticleServices.GetBlogs();
                //    var processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                //    Console.WriteLine(processName);
                //    await next();
                //});
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // �ڷǿ��������У�ʹ��HTTP�ϸ�ȫ����(or HSTS) ���ڱ���web��ȫ�Ƿǳ���Ҫ�ġ�
                // ǿ��ʵʩ HTTPS �� ASP.NET Core����� app.UseHttpsRedirection
                //app.UseHsts();

            }
            #endregion
            // ʹ�þ�̬�ļ�
            app.UseStaticFiles();
            #region Swagger

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //���ݰ汾���Ƶ��� ����չʾ
                var ApiName = Appsettings.app(new string[] { "Startup", "ApiName" });
                typeof(ApiVersions).GetEnumNames().OrderByDescending(e => e).ToList().ForEach(version =>
                {
                    c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{ApiName} {version}");
                });
                // ��swagger��ҳ�����ó������Զ����ҳ�棬�ǵ�����ַ�����д�������������.index.html���������Ҽ����ԣ�Ƕ�����Դ
               // c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("CardSystemMng.Api.index.html");//���������MiniProfiler�������ܼ�صģ������£���������AOP�Ľӿ����ܷ�����������㲻��Ҫ��������ʱ��ע�͵�����Ӱ���֡�
                c.RoutePrefix = ""; //·�����ã�����Ϊ�գ���ʾֱ���ڸ�������localhost:8001�����ʸ��ļ�,ע��localhost:8001/swagger�Ƿ��ʲ����ģ�ȥlaunchSettings.json��launchUrlȥ����������뻻һ��·����ֱ��д���ּ��ɣ�����ֱ��дc.RoutePrefix = "doc";
            });
            #endregion

            // ������������ ע���±���Щ�м����˳�򣬺���Ҫ ������������

            app.UseCors("LimitRequests");

            // ��תhttps
            //app.UseHttpsRedirection();
           
            // ʹ��cookie
            app.UseCookiePolicy();
            // ���ش�����
            app.UseStatusCodePages();//�Ѵ����뷵��ǰ̨��������404
            // Routing
            app.UseRouting();
            // �����Զ�����Ȩ�м�������Գ��ԣ������Ƽ�
            // app.UseJwtTokenAuth();
            // �ȿ�����֤
            app.UseAuthentication();
            // Ȼ������Ȩ�м��
            app.UseAuthorization();

            // �����쳣�м����Ҫ�ŵ����
            //app.UseExceptionHandlerMidd();

            app.UseMiniProfiler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHub<ChatHub>("/api2/chatHub");
            });
        }
    }
}
