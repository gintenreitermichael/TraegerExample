
using Server;
using TraegerExample.Console;


            
using var nodeManager = new OpcUaNodeManager(Default.ServerAddress);
using var server = new OpcUaServer(Default.ServerAddress, nodeManager);
server.Start();

System.Console.WriteLine("Started.");
System.Console.ReadKey(true);