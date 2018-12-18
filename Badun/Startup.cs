using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using static Badun.Models.InitModels;

namespace Badun
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
			services.AddDbContext<MyDbContext>(options => options.UseMySQL(Configuration.GetConnectionString("MySql")));
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "Contacts API", Version = "v1" });
				c.AddSecurityDefinition("Bear", new ApiKeyScheme { In = "header", Description = "Please enter JWT with Bearer into field", Name = "Authorization", Type = "apiKey" });
				c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
				{ "Bear", Enumerable.Empty<string>() },
				});
			});
			services.AddSession();
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			services.AddCors(options =>
			{
				options.AddPolicy("AllowAllOrigin",
								builder => builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
			});
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}
			app.UseSwagger();
			app.UseSession();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contacts API V1");
				//c.RoutePrefix = "hendao-swagger-clothes-api";
			});
			app.UseCors("AllowAllOrigin");
			app.UseHttpsRedirection();
			app.UseMvc();
			DefaultFilesOptions options = new DefaultFilesOptions();
			options.DefaultFileNames.Clear();
			options.DefaultFileNames.Add("index.html");
			app.UseDefaultFiles(options);
			//app.UseStaticFiles();

			var contentTypeProvider = new FileExtensionContentTypeProvider();
			contentTypeProvider.Mappings[".txt"] = "text/*";

			app.UseStaticFiles(new StaticFileOptions()
			{
				ContentTypeProvider = contentTypeProvider
			});

			//// Set up custom content types -associating file extension to MIME type
			//var provider = new FileExtensionContentTypeProvider();
			//// Add new mappings
			//provider.Mappings[".txt"] = "text/*";
			//app.UseStaticFiles(new StaticFileOptions()
			//{
			//	FileProvider = new PhysicalFileProvider(
			//	Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", "")),
			//	RequestPath = new PathString("/"),
			//	ContentTypeProvider = provider
			//});
		}
    }
}
