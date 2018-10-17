using Furesoft.Proxy.Core;
using LiteDB;

namespace Furesoft.Proxy.Services.Interfaces
{
    public interface IDatabaseService : IService
    {
        LiteCollection<T> GetCollection<T>(string name);

        LiteDatabase GetDatabase();
    }
}