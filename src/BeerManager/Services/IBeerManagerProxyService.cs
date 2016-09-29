using System.Threading.Tasks;

namespace BeerManager.Services
{
    public interface IBeerManagerProxyService
    {
        Task<string> GetBeers(string order, bool reverse, string name, int page);
    }
}