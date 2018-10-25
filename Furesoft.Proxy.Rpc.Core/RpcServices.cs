using Furesoft.Proxy.Rpc.Core.Messages;
using System.IO;
using System.Xaml;

namespace Furesoft.Proxy.Rpc.Core
{
    public static class RpcServices
    {
        public static byte[] Serialize(RpcMethod m)
        {
            var ms = new MemoryStream();
            XamlServices.Save(ms, m);

            return ms.ToArray();
        }
        public static RpcMethod Deserialize(byte[] src)
        {
            return (RpcMethod)XamlServices.Load(new MemoryStream(src));
        }
    }
}