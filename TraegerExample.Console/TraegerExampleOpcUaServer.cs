using System;
using Opc.UaFx.Server;

namespace TraegerExample.Console {
  public class TraegerExampleOpcUaServer {
    private readonly OpcServer _server;
    
    public TraegerExampleOpcUaServer(string address, TraegerExampleOpcUaNodeManager nodeManager) {
      _server = new OpcServer(address, nodeManager);
    }
    
    public void Start() {
      _server.Start();
    }
    
    #region IDisposable Members
    private volatile bool _disposed;

    ~TraegerExampleOpcUaServer() {
      Dispose(false);
    }

    public void Dispose() {
      Dispose(true);

      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) {
      if (!_disposed) {
        if (disposing) {
          // release managed resources
          _server.Dispose();
        }

        // release unmanaged resources
      }

      _disposed = true;
    }

    protected void ThrowIfDisposed() {
      if (_disposed) throw new ObjectDisposedException(GetType().Name);
    }
    #endregion
  }
}