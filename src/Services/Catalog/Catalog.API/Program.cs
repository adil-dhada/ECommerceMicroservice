var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddCarter(new CarterDepedencyContextAssemblyCatalog());
    builder.Services.AddMediatR(config =>
    {
        config.RegisterServicesFromAssembly(typeof(IAssemblyMarker).Assembly);
    });
    builder.Services.AddMarten(opts =>
    {
        opts.Connection(builder.Configuration.GetConnectionString("DbConnection")!);
    }).UseLightweightSessions();
}


var app = builder.Build();
{
    app.MapCarter();

    app.Run();
}
