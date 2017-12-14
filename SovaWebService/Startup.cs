using AutoMapper;
using DataAccessLayer.dbContext;
using DataAccessLayer.dbDTO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SovaWebService.Models;

namespace SovaWebService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddAutoMapper();
            services.AddSingleton<IDataService, DataService>();
            services.AddSingleton<IMapper>(CreateMapper());
            services.AddRouting();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
                
            }

            app.UseFileServer();
            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "custom",
                    template: "{controller}/{action}");
                    

                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}");
            });
        }

        public IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<marks, MarksModel>()
                    .ReverseMap();
            });

            

            return config.CreateMapper();
        }
    }
}
