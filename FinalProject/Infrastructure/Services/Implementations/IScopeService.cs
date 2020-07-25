using System.Threading;
using System.Threading.Tasks;

namespace FinalProject.Infrastructure.Services.Implementations
{
    interface IScopeService<T>
        where T : IMyScope
    {
        Task DoWorkAsync(CancellationToken stoppingToken);
    }

    public class DealerLoad : IMyScope { }
    public interface IMyScope { }
}
