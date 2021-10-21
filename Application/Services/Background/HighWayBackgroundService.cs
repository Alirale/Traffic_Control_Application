using Application.Services.Background;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Background
{
    public interface IHighwayService
    {
        Task DoWork(CancellationToken stoppingToken);
    }

    public class HighWayBackgroundService : BackgroundService, IHighwayService
    {
        private readonly RoadFacade _roadFacade;
        private readonly Starter _starter;

        public HighWayBackgroundService(RoadFacade roadFacade, Starter starter)
        {
            _starter = starter;
            _roadFacade = roadFacade;
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            await _starter.Start();
            await _roadFacade.SimulateRoad(stoppingToken);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await DoWork(stoppingToken);
        }
    }
}