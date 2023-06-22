using Dtos;
using Opc.UaFx;
using Opc.UaFx.Server;

namespace Server {
  public class OpcUaNodeManager: OpcNodeManager {
    private readonly CancellationTokenSource _cts;

    protected override bool UseAsyncMethodCalls => true;

    public OpcUaNodeManager(string address) : base(address) {
      _cts = new CancellationTokenSource();
    }

    protected override void Dispose(bool disposing) {
      if (disposing) {
        _cts.Cancel();
        _cts.Dispose();
      }
      
      base.Dispose(disposing);
    }

    protected override IEnumerable<IOpcNode> CreateNodes(OpcNodeReferenceCollection references) {
      yield return new OpcDataTypeNode<ResultCode>();
      yield return new OpcDataTypeNode<CustomDataType>();

      var methodsNamespace = DefaultNamespace.GetName("Methods");
      var methods = new OpcFolderNode(methodsNamespace);
      
      var dataNamespace = DefaultNamespace.GetName("Data");
      var data = new OpcFolderNode(dataNamespace);
      
      references.Add(methods, OpcObjectTypes.ObjectsFolder);
      references.Add(data, OpcObjectTypes.ObjectsFolder);

      // ReSharper disable ObjectCreationAsStatement
      new OpcMethodNode(methods, nameof(LoadExample), new Func<Guid, string, ResultCode>(LoadExample));
      new OpcDataVariableNode<CustomDataType>(data, data.Namespace.GetName("LicenseOption"), new CustomDataType(Guid.NewGuid().ToString()));
      // ReSharper restore ObjectCreationAsStatement
      
      yield return methods;
      yield return data;
    }

    
    [return: OpcArgument("Result")]
    private ResultCode LoadExample([OpcArgument("ExampleId")] Guid exampleId, [OpcArgument("ExampleName")] string exampleName) {
      if (exampleId == Guid.Empty)
      {
        return ResultCode.ErrorNoExampleId;
      }

      if (string.IsNullOrWhiteSpace(exampleName))
      {
        return ResultCode.ErrorNoExampleName;
      }
      
      return ResultCode.Success;
    }
  }
}