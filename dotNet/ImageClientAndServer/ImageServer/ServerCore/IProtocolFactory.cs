using System.Net.Sockets;

namespace ServerCore
{
    public interface IProtocolFactory
    {
        IProtocol CreateProtocol(Socket a_ClientSocket, ILogger a_Logger, IDataSource a_DataSource);
    }
}
