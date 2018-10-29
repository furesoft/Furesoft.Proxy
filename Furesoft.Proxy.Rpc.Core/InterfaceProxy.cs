using System;
using System.Dynamic;
using System.Threading.Tasks;

namespace Furesoft.Proxy.Rpc.Core
{
    public class InterfaceProxy<Interface> : DynamicObject
        where Interface : class
    {
        private RpcClient rpcClient;

        public bool IsAsync { get; }

        public InterfaceProxy(RpcClient rpcClient, bool isAsync)
        {
            this.rpcClient = rpcClient;
            IsAsync = isAsync;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = Call(() =>
                rpcClient.CallMethod<Interface>(binder.Name, args)
            );

            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            Run(() =>
                rpcClient.SetProperty<Interface>(binder.Name, value) //.Wait();
            );

            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = Call(() => rpcClient.GetProperty<Interface>(binder.Name));

            return true;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            result = Call(() => rpcClient.GetIndex<Interface>(indexes)).Result;

            return true;
        }

        public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
        {
            Run(() =>
                rpcClient.SetIndex<Interface>(indexes, value)
            );

            return true;
        }

        private Task Run(Action act)
        {
            if (IsAsync)
            {
                return Task.Run(act);
            }
            else
            {
                act();
                return Task.FromResult(0);
            }
        }
        private Task<object> Call(Func<object> act)
        {
            if (IsAsync)
            {
                return Task.Run(act);
            }
            else
            {
                return Task.FromResult(act());
            }
        }
    }
}