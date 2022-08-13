using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TrainCarAPI.Context;
using TrainCarAPI.Middleware;
using TrainCarAPI.Model.Entity;
using TrainCarAPI.Policy;
using TrainCarAPI.Services;
using TrainCarAPI.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IRollingStockService, RollingStockService>();
builder.Services.AddScoped<ISiteService, SiteService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRollingStockUnitOfWork, RollingStockUnitOfWork>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>(options =>
{
    options.User.RequireUniqueEmail = true;
})
                .AddEntityFrameworkStores<TrainCarAPIDbContext>()
                .AddDefaultTokenProviders();

builder.Services.AddSingleton<IAuthorizationHandler, RailwayWorkerAuthorizationHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RailwayWorkerUser", policy =>
    policy.Requirements.Add(new RailwayWorkerPolicy()));
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
}); ;

#region Db

//Add custom TrainCarAPIDbContext "service" to the container.
builder.Services.AddDbContext<TrainCarAPIDbContext>(options =>
{
    var dbBuilder = options.UseSqlServer(@"Server=(local);Database=TrainCarDb;Trusted_Connection=True;MultipleActiveResultSets=true;");
    if (builder.Environment.IsDevelopment())
    {
        dbBuilder.EnableSensitiveDataLogging();
    }
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

/// <summary>
/// UseRouting adds route matching to the middleware pipeline. This middleware looks at the set of endpoints defined in the app, and selects the best match based on the request.
/// </summary>
app.UseRouting();

app.UseAuthorization();
app.UseMiddleware<RequestResponseMiddleware>();

app.MapRazorPages();
/// <summary>
/// UseEndpoints adds endpoint execution to the middleware pipeline. It runs the delegate associated with the selected endpoint.
/// </summary>
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
