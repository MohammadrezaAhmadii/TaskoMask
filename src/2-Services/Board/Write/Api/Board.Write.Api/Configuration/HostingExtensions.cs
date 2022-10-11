using Serilog;
using TaskoMask.BuildingBlocks.Web.MVC.Configuration.Serilog;
using TaskoMask.BuildingBlocks.Web.MVC.Configuration.Startup;
using TaskoMask.Services.Board.Infrastructure.CrossCutting.DI;

namespace TaskoMask.Services.Board.Write.Api.Configuration
{
    internal static class HostingExtensions
    {


        /// <summary>
        /// 
        /// </summary>
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.AddCustomSerilog();

            builder.Services.AddModules(builder.Configuration);

            builder.Services.AddWebApiPreConfigured(builder.Configuration);

            return builder.Build();
        }



        /// <summary>
        /// 
        /// </summary>
        public static WebApplication ConfigurePipeline(this WebApplication app)
        {

            app.UseSerilogRequestLogging();

            app.UseWebApiPreConfigured(app.Services, app.Environment);

            app.Services.InitialDatabasesAndSeedEssentialData();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app;
        }
    }
}