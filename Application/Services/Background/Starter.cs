using Background.Services;
using System.Threading.Tasks;

namespace Application.Services.Background
{
    public class Starter
    {
        private readonly SpeedCameraGenerator _speedCameraGenerator;
        private readonly EjectAllCarsinHighwayService _ejectAllCarsinHighwayService;

        public Starter(SpeedCameraGenerator speedCameraGenerator,
        EjectAllCarsinHighwayService ejectAllCarsinHighwayService)
        {
            _speedCameraGenerator = speedCameraGenerator;
            _ejectAllCarsinHighwayService = ejectAllCarsinHighwayService;
        }

        public async Task Start()
        {
            await _speedCameraGenerator.AutoGenerates(10, 999);
            await _ejectAllCarsinHighwayService.Eject();
        }

    }
}
