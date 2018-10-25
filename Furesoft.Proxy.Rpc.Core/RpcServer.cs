using Furesoft.Proxy.Rpc.Core.Communicator;
using Furesoft.Proxy.Rpc.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Furesoft.Proxy.Rpc.Core
{
    public class RpcServer : IDisposable
    {
        private MemoryMappedFileCommunicator listener;
        private Dictionary<string, object> _binds = new Dictionary<string, object>();

        public RpcServer(string name)
        {
            listener = new MemoryMappedFileCommunicator(name, 50000)
            {
                WritePosition = 0,
                ReadPosition = 2500
            };

            listener.DataReceived += Listener_DataReceived;
        }

        public void Bind<Interface>(Interface obj)
            where Interface : class
        {
            if (!_binds.ContainsKey(typeof(Interface).Name))
            {
                _binds.Add(typeof(Interface).Name, obj);
            }
        }

        public void Dispose()
        {
            listener.Dispose();
        }

        public void Start()
        {
            listener.StartReader();
        }

        public IList<MethodInfo> GetIndexProperties(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            var type = obj.GetType();
            IList<MethodInfo> results = new List<MethodInfo>();

            try
            {
                var props = type.GetProperties(System.Reflection.BindingFlags.Default |
                    System.Reflection.BindingFlags.Public |
                    System.Reflection.BindingFlags.Instance);

                if (props != null)
                {
                    foreach (var prop in props)
                    {
                        var indexParameters = prop.GetIndexParameters();
                        if (indexParameters == null || indexParameters.Length == 0)
                        {
                            continue;
                        }
                        var getMethod = prop.GetGetMethod();
                        if (getMethod == null)
                        {
                            continue;
                        }
                        results.Add(getMethod);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return results;
        }
        public IList<MethodInfo> SetIndexProperties(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            var type = obj.GetType();
            IList<MethodInfo> results = new List<MethodInfo>();

            try
            {
                var props = type.GetProperties(System.Reflection.BindingFlags.Default |
                    System.Reflection.BindingFlags.Public |
                    System.Reflection.BindingFlags.Instance);

                if (props != null)
                {
                    foreach (var prop in props)
                    {
                        var indexParameters = prop.GetIndexParameters();
                        if (indexParameters == null || indexParameters.Length == 0)
                        {
                            continue;
                        }
                        var getMethod = prop.GetSetMethod();
                        if (getMethod == null)
                        {
                            continue;
                        }
                        results.Add(getMethod);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return results;
        }

        private void Listener_DataReceived(object sender, MemoryMappedDataReceivedEventArgs e)
        {
            var method = RpcServices.Deserialize(e.Data);

            object r = null;

            if (_binds.ContainsKey(method.Interface))
            {
                var type = _binds[method.Interface].GetType();

                if (method is RpcIndexMethod ri)
                {
                    if (ri.Name == "get_Index")
                    {
                        var p = GetIndexProperties(_binds[method.Interface]).First();
                        r = p.Invoke(_binds[method.Interface], ri.Indizes);
                    }
                    else
                    {
                        var p = SetIndexProperties(_binds[method.Interface]).First();
                        var args = new List<object>();
                        args.AddRange(ri.Indizes);
                        args.Add(ri.Value);

                        p.Invoke(_binds[method.Interface], args.ToArray());
                    }
                }
                else
                {
                    var m = type.GetMethod(method.Name);

                    if (m?.ReturnType == typeof(void))
                    {
                        r = null;

                        m.Invoke(_binds[method.Interface], method.Args.ToArray());
                    }
                    else
                    {
                        r = m.Invoke(_binds[method.Interface], method.Args.ToArray());
                    }
                }

                var returner = new RpcMethodAwnser()
                {
                    Interface = method.Interface,
                    Name = method.Name,
                    ReturnValue = r
                };

                listener.Write(RpcServices.Serialize(returner));
            }
            else
            {
                throw new Exception($"Interface '{method.Interface}' is not bound!");
            }
        }
    }
}