using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using ServerCore;

namespace ImageServer
{
    public partial class ServerUi : Form
    {
        public ServerUi()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception e)
            {
                string msg = e.Message;
                string trace = e.StackTrace;
                throw;
            }
        }

        private void m_StopServerBTN_Click(object a_Sender, System.EventArgs a_E)
        {
            // stop server, keep window open
            


        }

        private void m_ClearListBTN_Click(object a_Sender, System.EventArgs a_E)
        {
            m_UserLV.Clear();
            m_ServerLogLV.Clear();
        }

        private void ResetForm()
        {
            m_StatusLBL.ResetText();
            m_AcceptCountLBL.ResetText();
            m_UserLV.Clear();
            m_ServerLogLV.Clear();
        }
    }
}
