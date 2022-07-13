
namespace TraegerExample.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var serverAddress = "opc.tcp://localhost:4840/SampleServer";
            
            var nodeManager = new TraegerExampleOpcUaNodeManager(serverAddress);
            var server = new TraegerExampleOpcUaServer(serverAddress, nodeManager);
            server.Start();
            
            System.Console.WriteLine("Started.");
            System.Console.ReadKey(true);
        }
    }
}