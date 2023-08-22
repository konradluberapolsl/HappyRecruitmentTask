using TeslaRent.Application;
using TeslaRent.Infrastructure;
using TeslaRent.Infrastructure.DAL;

const string defaultCorsPolicy = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var origins = builder.Configuration["origins"].Split(";");
builder.Services.AddCors(options =>
{
    options.AddPolicy(defaultCorsPolicy,
        corsPolicyBuilder =>
        {
            corsPolicyBuilder
                .WithOrigins(origins)
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error-development");
}
else
{
    app.UseExceptionHandler("/error");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction() == false)
{
    app.UseSwagger();
    app.UseSwaggerUI();

    //app.UseDeveloperExceptionPage();

    using (var scope = app.Services.CreateScope())
    {
        var initializer = scope.ServiceProvider.GetRequiredService<TeslaRentDbContextInitializer>();
        
        await initializer.InitDatabase();
        await initializer.SeedDatabase();
    }
}

app.UseCors(defaultCorsPolicy);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();