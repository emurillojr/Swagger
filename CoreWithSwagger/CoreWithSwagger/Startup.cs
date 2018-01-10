using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger; //
using System;
using System.IO;

namespace CoreWithSwagger
{
    public class Startup
    {
        // added function
        private string GetXmlCommentsPath()
        {
            return Path.Combine(AppContext.BaseDirectory, "CoreWithSwagger.xml");
        }




        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // added Swagger generater 
            services.AddSwaggerGen(c =>
      {
          c.SwaggerDoc("v1", new Info { Title = "Core API", Description = "Swagger Core API" });
          //xml file getting generated with comments
          //var xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + @"CoreWithSwagger.xml";
          c.IncludeXmlComments(GetXmlCommentsPath());
      });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            //enabled the middleware for servicing the generated JSON document and the Swagger UI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
         {
             // provide endpoint
             c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core API");
         }
                );
        }
    }
}
