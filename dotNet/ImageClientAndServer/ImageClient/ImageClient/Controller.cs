using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Xml;
using NetworkCore;

namespace ImageClient
{
    class Controller
    {
	    public static Dictionary<int, string> categoryList;
	    public static Dictionary<int, string> imageList;
	    public static List< KeyValuePair<int, Image> > images = new List< KeyValuePair<int, Image> >();

        public static string GetServerAddresses()
        {
            return ConfigurationManager.AppSettings["ServerAddressesList"];
        }

        public static Dictionary<int, string> GetCategoryList(string a_ServerAddress, int a_ServerPortNum)
        {
		    Socket socket = new TcpClient(a_ServerAddress, a_ServerPortNum).Client;

		    // get sessionId from image server
	        string messageToSend = Protocol.GetClientMessageXmlStr(Protocol.ClientMessage.ReqSession);
            GetServerResponse(socket, messageToSend);

		    // get category list
            messageToSend = Protocol.GetClientMessageXmlStr(Protocol.ClientMessage.CategoryListRequest);
            GetServerResponse(socket, messageToSend);

		    // close connection to server
		    CloseConnectionToServer(socket);

		    return categoryList;
        }

	    public static Dictionary<int, string> GetImageList(string a_ServerAddress, int a_ServerPortNum, int a_CategoryIndex)
        {
		    Socket socket = new TcpClient(a_ServerAddress, a_ServerPortNum).Client;

		    // get sessionId from image server
            string messageToSend = Protocol.GetClientMessageXmlStr(Protocol.ClientMessage.ReqSession);
            GetServerResponse(socket, messageToSend);

		    // get image list
            messageToSend = Protocol.GetClientMessageXmlStr(Protocol.ClientMessage.ImageListRequest, a_CategoryIndex);
            GetServerResponse(socket, messageToSend);

		    // close connection to server
		    CloseConnectionToServer(socket);

		    return imageList;
        }

	    public static Image GetImage(string a_ServerAddress, int a_ServerPortNum, int a_ImageIndex)
	    {
	        Socket socket = new TcpClient(a_ServerAddress, a_ServerPortNum).Client;

	        // get sessionId from image server
            string messageToSend = Protocol.GetClientMessageXmlStr(Protocol.ClientMessage.ReqSession);
            GetServerResponse(socket, messageToSend);

	        // get image
            messageToSend = Protocol.GetClientMessageXmlStr(Protocol.ClientMessage.GetImage, a_ImageIndex);
            GetServerResponse(socket, messageToSend);

	        // close connection to server
	        CloseConnectionToServer(socket);

            // if image has already been retrieved, then return the first copy available in the collection
	        var match = from keyValuePair in images where keyValuePair.Key == a_ImageIndex select keyValuePair.Value;
	        var enumerable = match as Image[] ?? match.ToArray();
            
	        return enumerable.FirstOrDefault();
	    }

	    private static void CloseConnectionToServer(Socket a_Socket)
	    {
		    string messageToSend = Protocol.GetClientMessageXmlStr(Protocol.ClientMessage.Complete);
            IoHelper.SendMessage(a_Socket, messageToSend);
		    a_Socket.Close();
	    }

	    private static void GetServerResponse(Socket a_Socket, string a_MessageToSend)
	    {
            string serverReply = IoHelper.SendMessageAndGetResponse(a_Socket, a_MessageToSend);

	        XmlDocument xmlDocument = new XmlDocument();
	        xmlDocument.LoadXml(serverReply);

	        switch (Protocol.GetServerMessageType(xmlDocument))
	        {
		    case Protocol.ServerMessage.Session:
		        int sessionNumber = Protocol.ParseSessionId(xmlDocument);
		        if (sessionNumber.Equals(0))
			        throw new ApplicationException(String.Format("Unable to establish connection with remote host: {0}", a_Socket.GetHashCode()));
		        break;
		    case Protocol.ServerMessage.Reset:
		        throw new ApplicationException(Convert.ToString(Protocol.ServerMessage.Reset));
		    case Protocol.ServerMessage.CategoryList:
		        categoryList = Protocol.ParseCategoryList(xmlDocument);
		        break;
		    case Protocol.ServerMessage.ImageList:
		        imageList = Protocol.ParseImageList(xmlDocument);
		        break;
		    case Protocol.ServerMessage.ImageBegin:
	            int imageIndex = 0;
                var image = Protocol.ParseImage(a_Socket, xmlDocument, ref imageIndex);
                images.Add(new KeyValuePair<int,Image>(imageIndex, image));
		        break;
		    case Protocol.ServerMessage.Fail:
		        throw new Exception(Protocol.ParseErrorMessage(xmlDocument));
	        }
	    }

    }
}
