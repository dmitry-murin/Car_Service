using Car_Service.BLL.Interfaces;
using Car_Service.BLL.Services;
using Car_Service.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.StaticFiles;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using System;
using System.IO;
using System.Web.Http;

[assembly: OwinStartup(typeof(Car_Service.App_Start.Startup))]

namespace Car_Service.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseNinjectMiddleware(() => NinjectConfig.CreateKernel.Value);
            app.UseOAuthBearerTokens(oAuthCondig);
            app.UseNinjectWebApi(config);
        }

        private OAuthAuthorizationServerOptions oAuthCondig = new OAuthAuthorizationServerOptions
        {
            TokenEndpointPath = new PathString("/api/token"),
            Provider = new OAuthProvider(NinjectConfig.CreateKernel.Value.Get<IUserService>()),
            AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
            AllowInsecureHttp = true
        };
    }
   
}