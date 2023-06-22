using Opc.UaFx.Server;

namespace Server {
  public class OpcUaServer : IDisposable {
    private readonly OpcServer _server;
    
    public OpcUaServer(string address, OpcUaNodeManager nodeManager) {
      _server = new OpcServer(address, nodeManager);
    }
    
    public void Start() {
      _server.Start();
    }
    
    #region IDisposable Members
    private volatile bool _disposed;

    ~OpcUaServer() {
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