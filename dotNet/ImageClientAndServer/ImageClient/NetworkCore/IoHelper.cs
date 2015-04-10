using System;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace NetworkCore
{
    public class IoHelper
    {
        private const string _MessageTerminator = "\0";

        public static string SendMessageAndGetResponse(Socket a_Socket, string a_MessageToSend)
        {
            SendMessage(a_Socket, a_MessageToSend);
            return ReceiveMessage(a_Socket);
        }

	    public static void SendMessage(Socket a_Socket, string a_MessageToSend)
	    {
            AppendMessageTerminator(ref a_MessageToSend);
		    byte[] messageBytes = Encoding.ASCII.GetBytes(a_MessageToSend);
		    a_Socket.Send(messageBytes, messageBytes.Length, SocketFlags.None);
	    }

        private static void AppendMessageTerminator(ref string a_Message)
        {
            a_Message += _MessageTerminator;
        }

	    public static string ReceiveMessage(Socket a_Socket)
        {
		    const int Size = 254;
	        string message = String.Empty;

            while (true)
            {
                var messageBuffer = new byte[Size];
                int numBytesReceived = a_Socket.Receive(messageBuffer);

                if (numBytesReceived > 0)
                    message += Encoding.ASCII.GetString(messageBuffer, 0, numBytesReceived);

                if (message.Contains(_MessageTerminator))
                    break;
            }

		    return message;
	    }

        public static Image ReceiveImage(Socket a_Socket, int a_ImageDataLength)
        {
            var stream = new MemoryStream(ReceiveImageData(a_Socket, a_ImageDataLength));
            var image = Image.FromStream(stream);
            stream.Dispose();
            stream.Close();
            return image;
        }

        private static byte[] ReceiveImageData(Socket a_Socket, int a_ImageDataLength)
        {
            byte[] imageBytes = new byte[a_ImageDataLength];
            while (true)
            {
                int numBytesReceived = a_Socket.Receive(imageBytes);

                if (numBytesReceived == a_ImageDataLength)
                    break;

            }

            return imageBytes;
        }

        public static void SendImage(Socket a_Socket, string a_HeaderMessage, byte[] a_ImageData)
        {
            SendMessage(a_Socket, a_HeaderMessage);
            SendImageData(a_Socket, a_ImageData);
        }

        private static void SendImageData(Socket a_Socket, byte[] a_ImageData)
        {
            a_Socket.Send(a_ImageData, a_ImageData.Length, SocketFlags.None);
        }

    }
}
