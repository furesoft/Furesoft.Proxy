using Furesoft.Signals;

namespace ApiTestConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var channel = Signal.CreateSenderChannel("FuresoftProxy");
            var result = Signal.CallMethod<string>(channel, 0x6A3B, "{ hello }");
        }
    }
}