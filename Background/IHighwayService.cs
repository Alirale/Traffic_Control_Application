using System.Threading;
using System.Threading.Tasks;

namespace Background
{
    public interface IHighwayService
    {
        Task DoWork(CancellationToken stoppingToken);
    }
}
