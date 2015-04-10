using System;
using System.Net.Sockets;
using System.Text;
using System.Xml;
using NetworkCore;
using ServerCore;

namespace ImageServer
{
    public class ClientDialog : IProtocol
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly int _sessionNumber;
        private readonly ILogger _logger;
        private readonly IDataSource _dataSource;
        private readonly Socket _clientSocket;
        private readonly XmlDocument _xmlDoc;
        public string clientName;
        private Protocol.ClientMessage _currentClientRequest;
        private bool _clientProtocolIsInError;
        private bool _keepConnectionOpen = true;
        private static int _numSessions;

        public ClientDialog(Socket a_ClientSocket, ILogger a_Logger, IDataSource a_DataSource)
        {
            _clientSocket = a_ClientSocket;
            _logger = a_Logger;
            _dataSource = a_DataSource;
            _xmlDoc = new XmlDocument();
            clientName = "(Unidentified Client)";
            _sessionNumber = ++_numSessions;
        }

        public void HandleClient()
        {
            try
            {
                while (_keepConnectionOpen)
                {
                    string messageFromClient = IoHelper.ReceiveMessage(_clientSocket);
                    _xmlDoc.LoadXml(messageFromClient);

                    Protocol.ClientMessage messageType = Protocol.GetClientMessageType(_xmlDoc);

                    switch (messageType)
                    {
                        case Protocol.ClientMessage.ReqSession:
                            HandleSessionRequest();
                            break;
                        case Protocol.ClientMessage.Complete:
                            HandleComplete();
                            break;
                        case Protocol.ClientMessage.CategoryListRequest:
                            HandleCategoryListRequest();
                            break;
                        case Protocol.ClientMessage.ImageListRequest:
                            HandleImageListRequest();
                            break;
                        case Protocol.ClientMessage.GetImage:
                            HandleGetImage();
                            break;
                        case Protocol.ClientMessage.Cancel:
                            HandleCancel();
                            break;
                        default:
                            HandleClientProtocolError();
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                _log.Error("ClientDialog ERROR", e);
                HandleClientProtocolError();
            }
        }
		
        private void HandleClientProtocolError()
        {
            try
            {
                _clientProtocolIsInError = true;
                _logger.WriteEntry(ToString());

                var response = GetServerResponse(Protocol.ServerMessage.Fail);
                IoHelper.SendMessage(_clientSocket, response);
                CloseConnection();
            }
            catch (Exception e)
            {
                _log.Error(String.Format("ERROR in HandleClientProtocolError(): {0}", e.Message), e);
            }
        }

        private void HandleCancel()
        {
            try
            {
                _currentClientRequest = Protocol.ClientMessage.Cancel;
                _logger.WriteEntry(ToString());
                
                // reset activity to its initial state TODO
            }
            catch (Exception e)
            {
                _log.Error(String.Format("ERROR in HandleCancel(): {0}", e.Message), e);
                throw;
            }
        }

        private void HandleGetImage()
        {
            try
            {
                _currentClientRequest = Protocol.ClientMessage.GetImage;
                _logger.WriteEntry(ToString());

                int imageIndex = Protocol.ParseImageId(_xmlDoc);
                var image = _dataSource.GetImage(imageIndex);

                var response = GetServerResponse(Protocol.ServerMessage.ImageBegin, imageIndex, image.Length);
                IoHelper.SendImage(_clientSocket, response, image);
            }
            catch (Exception e)
            {
                _logger.WriteEntry(String.Format("ERROR in HandleGetImage(): {0}", e.Message));
                _logger.WriteEntry(e.StackTrace);
                throw;
            }
        }

        private void HandleImageListRequest()
        {
            try
            {
                _currentClientRequest = Protocol.ClientMessage.ImageListRequest;
                _logger.WriteEntry(ToString());

                int categoryIndex = Protocol.ParseCategoryId(_xmlDoc);

                var response = GetServerResponse(Protocol.ServerMessage.ImageList, categoryIndex);
                IoHelper.SendMessage(_clientSocket, response);
            }
            catch (Exception e)
            {
                _log.Error(String.Format("ERROR in HandleImageListRequest(): {0}", e.Message), e);
                throw;
            }
        }

        private void HandleCategoryListRequest()
        {
            try
            {
                _currentClientRequest = Protocol.ClientMessage.CategoryListRequest;
                _logger.WriteEntry(ToString());

                var response = GetServerResponse(Protocol.ServerMessage.CategoryList);
                IoHelper.SendMessage(_clientSocket, response);
            }
            catch (Exception e)
            {
                _log.Error(String.Format("ERROR in HandleCategoryListRequest(): {0}", e.Message), e);
                throw;
            }
        }

        private void HandleComplete()
        {
            try
            {
                _currentClientRequest = Protocol.ClientMessage.Complete;
                _logger.WriteEntry(ToString());
                CloseConnection();
            }
            catch (Exception e)
            {
                _log.Error(String.Format("ERROR in HandleComplete(): {0}", e.Message), e);
                throw;
            }
        }

        private void HandleSessionRequest()
        {
            try
            {
                SetClientName();
                _currentClientRequest = Protocol.ClientMessage.ReqSession;
                _logger.WriteEntry(ToString());

                var response = GetServerResponse(Protocol.ServerMessage.Session);
                IoHelper.SendMessage(_clientSocket, response);
            }
            catch (Exception e)
            {
                _log.Error(String.Format("ERROR in HandleSessionRequest(): {0}", e.Message), e);
                throw;
            }
        }

        private void CloseConnection()
        {
            _keepConnectionOpen = false;
            _clientSocket.Close();
        }

        private void SetClientName()
        {
            var selectSingleNode = _xmlDoc.SelectSingleNode("//ReqSession");
            if (selectSingleNode != null)
                clientName = selectSingleNode.InnerText;
        }

        private string GetServerResponse(Protocol.ServerMessage a_ProtocolMessage, int a_ItemIndex = 0, int a_ImageLength = 0)
        {
            StringBuilder response = new StringBuilder();

            switch (a_ProtocolMessage)
            {
                case Protocol.ServerMessage.Session:
                    response.Append(String.Format("<Session host=\"ImageServer - MHargiss\" number=\"{0}\"/>", _sessionNumber));
                    break;
                case Protocol.ServerMessage.CategoryList:
                    var categoryList = _dataSource.GetCategoryList();
                    response.Append(categoryList);
                    break;
                case Protocol.ServerMessage.ImageList:
                    var imageList = _dataSource.GetImageList(a_ItemIndex);
                    response.Append(imageList);
                    break;
                case Protocol.ServerMessage.ImageBegin:
                    response.Append(String.Format("<ImageBegin index=\"{0}\" length=\"{1}\" />", a_ItemIndex, a_ImageLength));
                    break;
            }

            return response.ToString();
        }

        public override string ToString()
        {
            return _clientProtocolIsInError ? String.Format("Client {0} - {1} - {2}", _sessionNumber, clientName, "Client Protocol Error!") : String.Format("Client {0} - {1} - {2}", _sessionNumber, clientName, _currentClientRequest);
        }
    }
}
