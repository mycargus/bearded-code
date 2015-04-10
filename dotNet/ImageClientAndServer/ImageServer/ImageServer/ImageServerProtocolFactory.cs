using System.Net.Sockets;
using ServerCore;

namespace ImageServer
{
    public class ImageServerProtocolFactory : IProtocolFactory
    {
        public IProtocol CreateProtocol(Socket a_ClientSocket, ILogger a_Logger, IDataSource a_DataSource)
        {
            return new ClientDialog(a_ClientSocket, a_Logger, a_DataSource);
        }
    }
}