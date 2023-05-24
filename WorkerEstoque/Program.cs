using Core.Services;
using WorkerEstoque;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddScoped<EstoqueService>();
    })
    .Build();

await host.RunAsync();
