using System.Threading;
using System.Threading.Tasks;

namespace BackgroundTask
{
    public interface IHighwayService
    {
        Task DoWork(CancellationToken stoppingToken);
    }
}
