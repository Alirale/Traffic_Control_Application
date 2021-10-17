using BackgroundTask.Service;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundTask
{

    public class HighWayBackgroundService : BackgroundService, IHighwayService
    {

        private readonly HighwayEngineService _highwayEngineService;
        private readonly EnterCarInHighwayService _enterCarInHighwayService;
        private readonly CameraSpeedCheckService _cameraSpeedCheckService;
        private readonly DriverService _driverService;
        private readonly SpeedCameraGenerator _speedCameraGenerator;
        private readonly EjectAllCarsinHighwayService _ejectAllCarsinHighwayService;
        public HighWayBackgroundService(
            EnterCarInHighwayService enterCarInHighwayService,
            HighwayEngineService highwayEngineService,
            CameraSpeedCheckService cameraSpeedCheckService,
            DriverService driverService,
            SpeedCameraGenerator speedCameraGenerator,
            EjectAllCarsinHighwayService ejectAllCarsinHighwayService
            )
        {
            _enterCarInHighwayService = enterCarInHighwayService;
            _highwayEngineService = highwayEngineService;
            _cameraSpeedCheckService = cameraSpeedCheckService;
            _driverService = driverService;
            _speedCameraGenerator = speedCameraGenerator;
            _ejectAllCarsinHighwayService = ejectAllCarsinHighwayService;
        }



        public async Task DoWork(CancellationToken stoppingToken)
        {
            _speedCameraGenerator.AutoGenerates(10, 999);
            _ejectAllCarsinHighwayService.Eject();

            while (!stoppingToken.IsCancellationRequested)
            {
                _enterCarInHighwayService.RunCarEntrance();
                _driverService.DriverEngine();
                _highwayEngineService.RunEngine();
                _cameraSpeedCheckService.RunSpeedCheck();

                await Task.Delay(1000, stoppingToken);
            }

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await DoWork(stoppingToken);
        }
    }
}