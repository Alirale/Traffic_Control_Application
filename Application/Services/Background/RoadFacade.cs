using Background.Services;
using BackgroundTask.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.Background
{
    public class RoadFacade
    {
        private readonly HighwayEngineService _highwayEngineService;
        private readonly EnterCarInHighwayService _enterCarInHighwayService;
        private readonly CameraSpeedCheckService _cameraSpeedCheckService;
        private readonly DriverService _driverService;


        public RoadFacade(EnterCarInHighwayService enterCarInHighwayService,
            HighwayEngineService highwayEngineService,
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
                await _enterCarInHighwayService.RunCarEntrance();
                await _driverService.DriverEngine();
                await _highwayEngineService.RunEngine();
                await _cameraSpeedCheckService.RunSpeedCheck();
                await Task.Delay(1000, stoppingToken);
            }
        }

    }
}
