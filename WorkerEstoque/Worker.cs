using Core.Services;

namespace WorkerEstoque
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        public IServiceProvider services { get; }
        public IConfiguration configuration { get; set; }

        public Worker(ILogger<Worker> logger, IServiceProvider services, IConfiguration configuration)
        {
            _logger = logger;
            this.services = services;
            this.configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = services.CreateScope())
                    {
                        var estoqueService = scope.ServiceProvider.GetRequiredService<EstoqueService>();

                        estoqueService.ProcessaPedido();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}