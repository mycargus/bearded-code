using System;
using System.Net.Sockets;
using System.Threading;

namespace ServerCore
{
    public class ThreadPerDispatcher : IDispatcher
    {
        private TcpListener _tcpListener;
        private ILogger _logger;
        private IDataSource _dataSource;
        private IProtocolFactory _protocolFactory;
        private int _continueRunning = 1;

        public void StartDispatching(TcpListener a_TcpListener, ILogger a_Logger, IDataSource a_DataSource, IProtocolFactory a_ProtocolFactory)
        {
            _tcpListener = a_TcpListener;
            _logger = a_Logger;
            _dataSource = a_DataSource;
            _protocolFactory = a_ProtocolFactory;

            while (ContinueRunning())
            {
                try
                {
                    StartDispatcher();
                }
                catch (Exception e)
                {
                    _logger.WriteEntry("Exception: " + e.Message);
                }
            }

        }

        private void StartDispatcher()
        {
            _tcpListener.Start();

            Socket clientSocket = _tcpListener.AcceptSocket();
            IProtocol protocol = _protocolFactory.CreateProtocol(clientSocket, _logger, _dataSource);

            Thread thread = new Thread(new ThreadStart(protocol.HandleClient));
            thread.Start();

            _logger.WriteEntry("Created and started Thread: " + thread.GetHashCode());
            
        }

        public bool ContinueRunning()
        {
            return _continueRunning == 1;
        }

        public void Stop()
        {
            _continueRunning = 0;
        }

        public void Restart()
        {
            _continueRunning = 1;

            try
            {
                StartDispatcher();
            }
            catch (System.IO.IOException e)
            {
                _logger.WriteEntry("Exception: " + e.Message);
            }
        }
    }
}