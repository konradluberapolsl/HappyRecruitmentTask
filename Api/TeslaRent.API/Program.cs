using TeslaRent.Infrastructure;

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

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction() == false)
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseDeveloperExceptionPage();
}

app.UseCors(defaultCorsPolicy);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();