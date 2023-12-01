using JobOpeningsAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<JobContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("JobOpeningsContext")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
 {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "JobOpeningsAPI", Version = "v1" });

     var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
     var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
     c.IncludeXmlComments(xmlPath);

     c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
     {
         Name = "Authorization",
         Type = SecuritySchemeType.Http,
         Scheme = "Bearer",
         BearerFormat = "Jwt",
         In = ParameterLocation.Header,
         Description = "Please Insert Token"

     });
     c.AddSecurityRequirement(new OpenApiSecurityRequirement
     {
         {
               new OpenApiSecurityScheme
               {
                   Reference = new OpenApiReference
                   {
                       Type = ReferenceType.SecurityScheme,
                       Id = "Bearer"
                   }
               },
              new string[] {}
         }
     });
 });



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JobOpeningsAPI v1"));
}
//app.UseAuthentication();
//app.UseAuthorization();


app.MapControllers();

app.Run();
