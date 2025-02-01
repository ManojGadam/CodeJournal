using PersonalWebsite.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using PersonalWebsite.Repository;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IProblemRepository, ProblemRepository>();
builder.Services.AddDbContext<ProblemContext>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("Deployment");
         //if (!builder.Environment.IsDevelopment()) test
         //   {
         //       var password = Environment.GetEnvironmentVariable("MSSQL_SA_PASSWORD");
         //       Console.WriteLine($"Password from env: {connectionString} , {password}");
         //       connectionString = string.Format(connectionString, password);
         //   } 
         //   Console.WriteLine($"Total Password: {connectionString}");
        options.UseSqlServer(connectionString);
    });

// builder.Services.AddHttpsRedirection(options =>
// {
//     options.HttpsPort = 5001; // Specify the HTTPS port
// });

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("cache:6379,abortConnect=false,connectTimeout=10000"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin() // The origin of your frontend
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");

    // Consider using HSTS in production
    //app.UseHsts();

    // Enable HTTPS redirection unless running behind a proxy that handles HTTPS
    //app.UseHttpsRedirection();
}


app.UseCors("AllowAllOrigins");
app.UseRouting();


app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProblemContext>();
    // Check if there are any pending migrations
    var pendingMigrations = db.Database.GetPendingMigrations();

    // Check if the database contains any tables
    var existingTables = db.Database.GetAppliedMigrations();
    if (existingTables.Any() || pendingMigrations.Any())
    {
        db.Database.Migrate();
    }
}

app.Run();
