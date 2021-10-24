using Background.Services;
using BackgroundTask.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.Background
{
    public interface IRoadFacade
    {
        public Task SimulateRoad(CancellationToken stoppingToken);
    }
    public class RoadFacade : IRoadFacade
    {
        private readonly HighwayEngineService _highwayEngineService;
        private readonly EnterCarInHighwayService _enterCarInHighwayService;
        private readonly CameraSpeedCheckService _cameraSpeedCheckService;
        private readonly DriverService _driverService;


        public RoadFacade(
            HighwayEngineService highwayEngineService,
            EnterCarInHighwayService enterCarInHighwayService,
            CameraSpeedCheckService cameraSpeedCheckService,
            DriverService driverService)
        {
            _enterCarInHighwayService = enterCarInHighwayService;
            _highwayEngineService = highwayEngineService;
            _cameraSpeedCheckService = cameraSpeedCheckService;
            _driverService = driverService;
        }


        public async Task SimulateRoad(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                _enterCarInHighwayService.RunCarEntrance();
                _driverService.DriverEngine();
                _highwayEngineService.RunEngine();
                _cameraSpeedCheckService.RunSpeedCheck();
                await Task.Delay(1000, stoppingToken);
            }
        }

    }
}
