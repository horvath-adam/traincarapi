using Microsoft.EntityFrameworkCore;
using TrainCarAPI.Context;
using TrainCarAPI.Services;
using TrainCarAPI.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IRollingStockService, RollingStockService>();
builder.Services.AddScoped<ISiteService, SiteService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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


app.MapRazorPages();
/// <summary>
/// UseEndpoints adds endpoint execution to the middleware pipeline. It runs the delegate associated with the selected endpoint.
/// </summary>
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
