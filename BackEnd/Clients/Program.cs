using Clientes.Models;
using Clientes.Repositories.Implementations;
using Clientes.Repositories.Interfaces;
using Clientes.Services.Implementations;
using Clientes.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    IServiceCollection services = builder.Services;
    IConfiguration configuration = builder.Configuration;

    builder.Services.AddScoped<IClientesService, ClientesService>();
    builder.Services.AddScoped<IClientesRepository, ClientesRepository>();


    var connectionString = builder.Configuration.GetConnectionString("ConnectionString");

    builder.Services.AddDbContext<MyDbContext>(options =>
        options.UseSqlServer(connectionString));



    //Cors
    services.AddCors(c => { c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin()); });
    services.AddControllers();

    //Documentacion Swagger
    services.AddSwaggerGen(options =>
    {
        options.CustomSchemaIds(type => type.ToString());
        options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "TalycapGlobal Clientes v1",
            Version = "v1",
            Description = "TalycapGlobal Clientes API"
        });
    });

    var app = builder.Build();



    app.UseCors(x => x
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .SetIsOriginAllowed(origin => true)
                 .AllowCredentials());

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TalycapGlobal Clientes v1");
        c.DefaultModelsExpandDepth(-1);
    });

    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Error al iniciar la aplicación");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}

