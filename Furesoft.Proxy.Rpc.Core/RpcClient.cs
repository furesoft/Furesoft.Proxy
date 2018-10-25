using Furesoft.Proxy.Rpc.Core.Communicator;
using Furesoft.Proxy.Rpc.Core.Messages;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Furesoft.Proxy.Rpc.Core
{
    public class RpcClient : IDisposable
    {
        private MemoryMappedFileCommunicator sender;

        public RpcClient(string name)
        {
            sender = new MemoryMappedFileCommunicator(name, 50000); ;

            sender.ReadPosition = 0;
            sender.WritePosition = 2500;
            sender.DataReceived += Sender_DataReceived;
        }

        public void Start()
        {
            sender.StartReader();
        }

        private void Sender_DataReceived(object sender, MemoryMappedDataReceivedEventArgs e)
        {
            var response = (RpcMethodAwnser)RpcServices.Deserialize(e.Data);

            ReturnValue = response.ReturnValue;

            mre.Set();
        }

        object ReturnValue;
        ManualResetEvent mre = new ManualResetEvent(false);

        public object CallMethod<Interface>(string methodname, params object[] args)
            where Interface : class
        {
            mre.Reset();

            var m = new RpcMethod
            {
                Interface = typeof(Interface).Name,
                Name = methodname,
                Args = args.ToList()
            };

            sender.Write(RpcServices.Serialize(m));

            mre.WaitOne();

            return ReturnValue;
        }

        public void SetProperty<Interface>(string propname, object value)
            where Interface : class
        {
            CallMethod<Interface>($"set_{propname}", value);
        }

        public object GetProperty<Interface>(string propertyname)
            where Interface : class
        {
            return CallMethod<Interface>($"get_{propertyname}");
        }

        public void SetIndex<Interface>(object[] indizes, object value)
        {
            mre.Reset();

            var m = new RpcIndexMethod
            {
                Name = "set_Index",
                Interface = typeof(Interface).Name,
                Indizes = indizes,
                Value = value
            };

            sender.Write(RpcServices.Serialize(m));

            mre.WaitOne();
        }
        public object GetIndex<Interface>(object[] indizes)
        {
            mre.Reset();

            var m = new RpcIndexMethod
            {
                Interface = typeof(Interface).Name,
                Name = "get_Index",
                Indizes = indizes
            };

            sender.Write(RpcServices.Serialize(m));

            mre.WaitOne();

            return ReturnValue;
        }

        public dynamic Bind<Interface>()
            where Interface : class
        {
            return new InterfaceProxy<Interface>(this);
        }

        public void Dispose()
        {
            sender.Dispose();
        }
    }
}