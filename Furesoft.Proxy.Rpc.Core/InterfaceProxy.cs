using System;
using System.Dynamic;
using System.Threading.Tasks;

namespace Furesoft.Proxy.Rpc.Core
{
    public class InterfaceProxy<Interface> : DynamicObject
        where Interface : class
    {
        private RpcClient rpcClient;

        public InterfaceProxy(RpcClient rpcClient, bool v)
        {
            this.rpcClient = rpcClient;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var t = rpcClient.CallMethod<Interface>(binder.Name, args);

            result = t;

            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            rpcClient.SetProperty<Interface>(binder.Name, value); //.Wait();

            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var t = rpcClient.GetProperty<Interface>(binder.Name);

            result = t;

            return true;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            result = rpcClient.GetIndex<Interface>(indexes); ;

            return true;
        }

        public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
        {
            rpcClient.SetIndex<Interface>(indexes, value);

            return true;
        }
    }

}