using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Xml;

namespace NetworkCore
{
    public class Protocol
    {
	        const string _StudentName = "Michael Hargiss";

	        public enum ClientMessage
	        {
	                ReqSession = 1,
	                Complete = 2,
	                CategoryListRequest = 3,
	                ImageListRequest = 4,
	                GetImage = 5,
	                Cancel = 6
	        }

	        public enum ServerMessage
	        {
	                Session = 1,
	                Reset = 2,		// Clear client display and start over
	                CategoryList = 3,
	                ImageList = 4,
	                ImageBegin = 5,
	                Error = 6,		// Server application error while processing request ---> receive error, display error report
	                Fail = 7,		// Application protocol error in client's messages
	        }

	        public static string GetClientMessageXmlStr(ClientMessage a_ProtocolMessage, int a_Index = 0)
	        {
	                StringBuilder msgBuilder = new StringBuilder();

	                switch (a_ProtocolMessage)
	                {
		                    case ClientMessage.ReqSession:
	                                msgBuilder.Append(String.Format("<{0}>{1}</{0}>", Convert.ToString(ClientMessage.ReqSession), _StudentName));
		                            break;
		                    case ClientMessage.Complete:
		                            msgBuilder.Append(String.Format("<{0}/>", Convert.ToString(ClientMessage.Complete)));
		                            break;
		                    case ClientMessage.CategoryListRequest:
                                    msgBuilder.Append(String.Format("<{0}/>", Convert.ToString(ClientMessage.CategoryListRequest)));
		                            break;
		                    case ClientMessage.ImageListRequest:
                                    msgBuilder.Append(String.Format("<{0} category=\"{1}\"/>", Convert.ToString(ClientMessage.ImageListRequest), a_Index));
		                            break;
                            case ClientMessage.GetImage:
                                    msgBuilder.Append(String.Format("<{0} index=\"{1}\"/>", Convert.ToString(ClientMessage.GetImage), a_Index));
		                            break;
                            case ClientMessage.Cancel:
                                    msgBuilder.Append(String.Format("<{0}/>", Convert.ToString(ClientMessage.Cancel)));
		                            break;
	                }

	                return msgBuilder.ToString();
	        }

            public static ClientMessage GetClientMessageType(XmlDocument a_XmlDoc)
            {
                    ClientMessage clientMessageType = ClientMessage.Cancel;

                    const string ReqSession = "ReqSession";
                    const string Complete = "Complete";
                    const string CategoryListRequest = "CategoryListRequest";
                    const string ImageListRequest = "ImageListRequest";
                    const string GetImage = "GetImage";

                    XmlElement rootNode = a_XmlDoc.DocumentElement;
                    if (rootNode != null)
                    {
                        switch (rootNode.Name)
                        {
                            case ReqSession:
                                clientMessageType = ClientMessage.ReqSession;
                                break;
                            case Complete:
                                clientMessageType = ClientMessage.Complete;
                                break;
                            case CategoryListRequest:
                                clientMessageType = ClientMessage.CategoryListRequest;
                                break;
                            case ImageListRequest:
                                clientMessageType = ClientMessage.ImageListRequest;
                                break;
                            case GetImage:
                                clientMessageType = ClientMessage.GetImage;
                                break;
                        }
                    }

                    return clientMessageType;
            }

	        public static ServerMessage GetServerMessageType(XmlDocument a_XmlDoc)
	        {
	                ServerMessage serverMessageType = ServerMessage.Fail;

	                const string Session = "Session";
	                const string Reset = "Reset";
	                const string CategoryList = "CategoryList";
	                const string ImageList = "ImageList";
	                const string ImageBegin = "ImageBegin";
	                const string Error = "Error";
	                const string Fail = "Fail";

                    XmlElement rootNode = a_XmlDoc.DocumentElement;
	                if (rootNode != null)
	                    switch (rootNode.Name)
	                    {
	                        case Session:
	                            serverMessageType = ServerMessage.Session;
	                            break;
	                        case Reset:
	                            serverMessageType = ServerMessage.Reset;
	                            break;
	                        case CategoryList:
	                            serverMessageType = ServerMessage.CategoryList;
	                            break;
	                        case ImageList:
	                            serverMessageType = ServerMessage.ImageList;
	                            break;
	                        case ImageBegin:
	                            serverMessageType = ServerMessage.ImageBegin;
	                            break;
	                        case Error:
	                            serverMessageType = ServerMessage.Error;
	                            break;
	                        case Fail:
	                            serverMessageType = ServerMessage.Fail;
	                            break;
	                    }

	                return serverMessageType;
	        }

	        public static int ParseSessionId(XmlDocument a_XmlDoc)
	        {
	                int sessionId = 0;

	                XmlNode sessionNumberNode = a_XmlDoc.SelectSingleNode("/" + Convert.ToString(ServerMessage.Session) + "/@number");
	                if (sessionNumberNode != null && sessionNumberNode.Value.Length > 0)
		            sessionId = Convert.ToInt32(sessionNumberNode.Value);
	    
	                return sessionId;
	        }

	        public static Dictionary<int, string> ParseCategoryList(XmlDocument a_XmlDoc)
	        {
	                var categoryList = new Dictionary<int, string>();

	                XmlNodeList categoryNodes = a_XmlDoc.SelectNodes("//Category");
	                if (categoryNodes != null)
	                    foreach (XmlNode node in categoryNodes)
	                    {
	                        XmlNode catNameNode = node.SelectSingleNode("@name");
	                        XmlNode catIndexNode = node.SelectSingleNode("@index");
                            if (catNameNode != null && catNameNode.Value.Length > 0 && catIndexNode != null && catIndexNode.Value.Length > 0 && !categoryList.Keys.Contains(Convert.ToInt32(catIndexNode.Value)))
	                            categoryList.Add(Convert.ToInt32(catIndexNode.Value), catNameNode.Value);
	                    }

	                return categoryList;
	        }

	        public static Dictionary<int, string> ParseImageList(XmlDocument a_XmlDoc)
	        {
	            var imageList = new Dictionary<int, string>();

	            XmlNodeList imageNodes = a_XmlDoc.SelectNodes("//Item");
	            if (imageNodes != null)
	                foreach (XmlNode node in imageNodes)
	                {
	                    XmlNode imageNameNode = node.SelectSingleNode("@name");
	                    XmlNode imageIndexNode = node.SelectSingleNode("@index");
	                    if (imageNameNode != null && imageIndexNode != null)
	                    {
	                        if (imageNameNode.Value.Length > 0 && imageIndexNode.Value.Length > 0 && !imageList.Keys.Contains(Convert.ToInt32(imageIndexNode.Value)))
	                            imageList.Add(Convert.ToInt32(imageIndexNode.Value), imageNameNode.Value);
	                    }
	                }

	            return imageList;
	        }

            private static void ParseBeginImage(XmlDocument a_XmlDoc, ref int a_ImageDataLength, ref int a_ImageIndex)
            {
                XmlNode imageDataLengthNode = a_XmlDoc.SelectSingleNode("/" + Convert.ToString(ServerMessage.ImageBegin) + "/@length");
                XmlNode imageIndexNode = a_XmlDoc.SelectSingleNode("/" + Convert.ToString(ServerMessage.ImageBegin) + "/@index");

                if (imageDataLengthNode != null && imageDataLengthNode.Value.Length > 0
                    && imageIndexNode != null && imageIndexNode.Value.Length > 0)
                {
                    a_ImageDataLength = Convert.ToInt32(imageDataLengthNode.Value);
                    a_ImageIndex = Convert.ToInt32(imageIndexNode.Value);
                }
            }

	        public static Image ParseImage(Socket a_Socket, XmlDocument a_XmlDoc, ref int a_ImageIndex)
	        {
                int imageDataLength = 0;
                ParseBeginImage(a_XmlDoc, ref imageDataLength, ref a_ImageIndex);
                return IoHelper.ReceiveImage(a_Socket, imageDataLength);
	        }

            public static int ParseCategoryId(XmlDocument a_XmlDoc)
            {
                int categoryId = 0;
                XmlNode categoryIdNode = a_XmlDoc.SelectSingleNode("/" + Convert.ToString(ClientMessage.ImageListRequest) + "/@category");
                if (categoryIdNode != null && categoryIdNode.Value.Length > 0)
                    categoryId = Convert.ToInt32(categoryIdNode.Value);
                return categoryId;
            }

            public static int ParseImageId(XmlDocument a_XmlDoc)
            {
                int imageId = 0;
                XmlNode imageIdNode = a_XmlDoc.SelectSingleNode("/" + Convert.ToString(ClientMessage.GetImage) + "/@index");
                if (imageIdNode != null && imageIdNode.Value.Length > 0)
                    imageId = Convert.ToInt32(imageIdNode.Value);
                return imageId;
            }

	        public static string ParseErrorMessage(XmlDocument a_XmlDoc)
	        {
	            string errorMsg = String.Empty;

	            XmlNode errorNode = a_XmlDoc.SelectSingleNode("/" + Convert.ToString(ServerMessage.Error));
	            if (errorNode != null && errorNode.InnerText.Length > 0)
		        errorMsg = errorNode.InnerText;

	            return errorMsg;
	        }
    }
}
