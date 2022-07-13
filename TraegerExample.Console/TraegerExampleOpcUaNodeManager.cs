using System;
using System.Collections.Generic;
using System.Threading;
using Opc.UaFx;
using Opc.UaFx.Server;
using TraegerExample.Console.Dtos;

namespace TraegerExample.Console {
  public class TraegerExampleOpcUaNodeManager: OpcNodeManager {
    private readonly CancellationTokenSource _cts;

    protected override bool UseAsyncMethodCalls => true;

    public TraegerExampleOpcUaNodeManager(string address) : base(address) {
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

      var methodsNamespace = DefaultNamespace.GetName("Methods");
      var methods = new OpcFolderNode(methodsNamespace);
      
      references.Add(methods, OpcObjectTypes.ObjectsFolder);

      // ReSharper disable ObjectCreationAsStatement
      new OpcMethodNode(methods, nameof(LoadExample), new Func<Guid, string, ResultCode>(LoadExample));
      // ReSharper restore ObjectCreationAsStatement

      yield return methods;
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