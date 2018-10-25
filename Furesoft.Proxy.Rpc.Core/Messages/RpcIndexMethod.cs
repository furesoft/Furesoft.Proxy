namespace Furesoft.Proxy.Rpc.Core.Messages
{
    public class RpcIndexMethod : RpcMethod
    {
        public object[] Indizes { get; set; }
        public object Value { get; set; }
    }
}