using Background.Services;

namespace Application.Services.Background
{
    public interface IStarter
    {
        public void Start();
    }
    public class Starter : IStarter
    {
        private readonly SpeedCameraGenerator _speedCameraGenerator;
        private readonly EjectAllCarsinHighwayService _ejectAllCarsinHighwayService;

        public Starter(SpeedCameraGenerator speedCameraGenerator,
        EjectAllCarsinHighwayService ejectAllCarsinHighwayService)
        {
            _speedCameraGenerator = speedCameraGenerator;
            _ejectAllCarsinHighwayService = ejectAllCarsinHighwayService;
        }

        public void Start()
        {
            _speedCameraGenerator.AutoGenerates();
            _ejectAllCarsinHighwayService.Eject();
        }

    }
}
