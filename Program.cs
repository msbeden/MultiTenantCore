using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Multiple.Configurations;
using Multiple.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<TenantSettings>(builder.Configuration.GetSection("TenantSettings"));
builder.Services.AddInfrastructureService();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("UserControllers", new OpenApiInfo { Title = "MultiTenantWebApi - User Tables", Version = "v1" });
    c.SwaggerDoc("ProductControllers", new OpenApiInfo { Title = "MultiTenantWebApi - Product Tables", Version = "v1" });

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "Jwt",
        Name = "Jwt Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Enter your **_ONLY_** Jwt Bearer token in the text box below!",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement { { jwtSecurityScheme, Array.Empty<string>() } });
});
await builder.Services.AddPersistenceService();

builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/UserControllers/swagger.json", "MultiTenantWebApi - User Tables");
        c.SwaggerEndpoint("/swagger/ProductControllers/swagger.json", "MultiTenantWebApi - Product Tables");
    });
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();