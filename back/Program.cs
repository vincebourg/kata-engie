using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    Helpers.AddNetTopologySuiteConverters(options.SerializerSettings.Converters);
    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
    {
        NamingStrategy = new Newtonsoft.Json.Serialization.DefaultNamingStrategy()
    };
}); ;
builder.Services.AddCors(options => options.AddDefaultPolicy(corsBuilder => corsBuilder.AllowAnyOrigin()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();
app.MapControllers();
app.Run();
