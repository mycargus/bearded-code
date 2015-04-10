using System.Net.Sockets;

namespace ServerCore
{
    public interface IDispatcher
    {
        void StartDispatching(TcpListener a_TcpListener, ILogger a_Logger, IDataSource a_DataSource, IProtocolFactory a_ProtocolFactory);
    }
}