using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using TestingSystem.Data;
using TestingSystem.Filters;
using TestingSystem.HttpHandler;
using TestingSystem.Hubs;
using TestingSystem.Repositories;
using TestingSystem.Repositories.Interfaces;
using TestingSystem.Services.AuthService;
using TestingSystem.Services.CourseService;
using TestingSystem.Services.GameService;

var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


// Add services to the container.
builder.Services.AddMvc(options =>
{
    options.Filters.Add<ValidationFilter>();
});
builder.Services.AddControllers();
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization Reader using the Bearer scheme (\"Bearer {token} \")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    //builder.WithOrigins("http://localhost:4200/").AllowAnyMethod().AllowAnyHeader().AllowCredentials();

    builder.WithOrigins("http://localhost:4200/")
          .AllowAnyHeader()
          .AllowAnyMethod()
          .AllowCredentials()
          .SetIsOriginAllowed((host) => true);
}));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddTransient<HttpTrackerHandler>();
builder.Services.AddTransient<ICourseService, CourseService>();    

builder.Services.AddHttpClient("auth")
                .AddHttpMessageHandler<HttpTrackerHandler>();

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddHttpContextAccessor();

builder.Services.AddSignalR(configuration =>
{
    configuration.EnableDetailedErrors = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().SetIsOriginAllowed((host) => true).WithOrigins("*"));




app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.UseCors("corsapp");

// Add this line in the Configure method
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<QuizHub>("/quizhub");
});



app.MapControllers();

app.Run();
