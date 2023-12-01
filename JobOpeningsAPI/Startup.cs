//using JobOpeningsAPI.Data;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
//using System.Reflection;
//using System.Text;
//using System.Web.Mvc;

//namespace JobOpeningsAPI
//{
//    public class Startup
//    {
//        public Startup(IConfiguration configuration)
//        {
//            Configuration = configuration;
//        }

//        public IConfiguration Configuration { get; }

//        // This method gets called by the runtime. Use this method to add services to the conta=iner.
//        public void ConfigureServices(IServiceCollection services)
//        {

//            services.AddControllers();
//            services.AddDbContext<JobContext>(options =>
//                options.UseSqlServer(Configuration.GetConnectionString("JobOpeningsContext")));
//            services.AddSwaggerGen(c =>
//            {
//                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JobOpeningsAPI", Version = "v1" });

//                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
//                c.IncludeXmlComments(xmlPath);

//                //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
//                //{
//                //    Name = "Authorization",
//                //    Type = SecuritySchemeType.ApiKey,
//                //    Scheme = "Bearer",
//                //    BearerFormat = "Jwt",
//                //    In = ParameterLocation.Header,
//                //    Description = "JWT Authorization header using the Bearer scheme."

//                //});
//                //c.AddSecurityRequirement(new OpenApiSecurityRequirement
//                //{
//                //    {
//                //          new OpenApiSecurityScheme
//                //          {
//                //              Reference = new OpenApiReference
//                //              {
//                //                  Type = ReferenceType.SecurityScheme,
//                //                  Id = "Bearer"
//                //              }
//                //          },
//                //         new string[] {}
//                //    }
//                //});
//            });

//            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(Options =>
//            {
//                Options.TokenValidationParameters = new TokenValidationParameters
//                {

//                    ValidateIssuer = true,
//                    ValidateAudience = true,
//                    ValidateLifetime = true,
//                    ValidIssuer = Configuration["Jwt:Issuer"],
//                    ValidAudience = Configuration["Jwt:Audience"],
//                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
//                };
//            });
//        }

//        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//        {
//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//                app.UseSwagger();
//                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JobOpeningsAPI v1"));
//            }

//            app.UseRouting();

//            app.UseAuthorization();
//            app.UseAuthentication();

//            app.UseEndpoints(endpoints =>
//            {
//                endpoints.MapControllers();
//            });
//        }
//    }
//}
