using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using ServerCore;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace ImageServer
{
    static class Program
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
	    {
            try
            {
                StartServer();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ServerUi());
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
            }
        }

	    private static void StartServer()
	    {
		    var serverThread = new Thread(StartServerThread);
		    serverThread.Start();
	    }

		private static void StartServerThread()
		{
		    int serverPort = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            string ipAddress = ConfigurationManager.AppSettings["serverIpAddress"];

		    TcpListener listener = new TcpListener(IPAddress.Parse(ipAddress), serverPort);

            //IDataSource dataSource = new ImageDb(ConfigurationManager.AppSettings["DatabaseConnectionString"]);
            IDataSource dataSource = new ImageDbStringLiteral();

            //ILogger logger = new FormLogger(null);
            ILogger logger = new FileLogger("./server_log.txt");

		    IProtocolFactory protocolFactory = new ImageServerProtocolFactory();

		    IDispatcher dispatcher = new ThreadPerDispatcher();
		    dispatcher.StartDispatching(listener, logger, dataSource, protocolFactory);
		}
    }
}
