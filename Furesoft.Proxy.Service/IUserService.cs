using Furesoft.Proxy.Core;
using Kälberstall.Core;
using Kälberstall_Milchhof_Gut_Parchim.Models;

namespace Furesoft.Proxy.Service
{
    public interface IUserService : IService
    {
        bool Add(User user);
        void Update(User user);
        void Delete(User user);

        User GetUserByUsername(string username);
    }
}