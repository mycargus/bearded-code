using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using NetworkCore;

namespace ImageClient
{
    public partial class View : Form
    {

	        enum StatusMessage
	        {
	                Waiting = 1,
	                SendRequest = 2,
	                ReceiveResponse = 3,
	                ProcessData = 4,
	                Error = 5
	        }

	        public View()
	        {
	                try
	                {
		                    InitializeComponent();
		                    InitializeForm();
	                }
	                catch(Exception ex)
	                {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine(ex.StackTrace);
                            Console.ReadLine();
	                }
	        }

	        private void InitializeForm()
	        {
                    BindServerAddresses();
	                BindEventHandlers();
                    ChangeStatusMessage(StatusMessage.Waiting);
	        }

            private void BindServerAddresses()
            {
                var serverAddresses = new Dictionary<string, string>();
                foreach (string serverAddress in Controller.GetServerAddresses().Split(','))
                {
                    string[] addressPair = serverAddress.Split('=');
                    serverAddresses.Add(addressPair[0], addressPair[1]);
                }
                m_ServerAddressDDL.DropDownStyle = ComboBoxStyle.DropDownList;
                m_ServerAddressDDL.DataSource = new BindingSource(serverAddresses, null);
                m_ServerAddressDDL.DisplayMember = "Key";
                m_ServerAddressDDL.ValueMember = "Value";
                m_ServerAddressDDL.Enabled = true;
            }

            private void BindEventHandlers()
            {
                m_ImageListLB.Click += m_ImageListLB_DoubleClick;
                m_ImageListLB.MouseDoubleClick += m_ImageListLB_DoubleClick;
                m_CategoryListDDL.SelectedIndexChanged += m_CategoryListDDL_SelectedIndexChanged;
            }

	        private void m_ServerAddressDDL_SelectedIndexChanged(object a_Sender, EventArgs a_E)
	        {
	                ResetForm();
	        }

            private string GetServerAddress()
            {
                return m_ServerAddressDDL.GetItemText(m_ServerAddressDDL.SelectedItem);
            }

            private int GetServerPortNumber()
            {
                return Convert.ToInt32(m_ServerAddressDDL.SelectedValue);
            }

            private void m_GetCategoryListBTN_Click(object a_Sender, EventArgs a_E)
	        {
	                try
	                {
		                    ChangeStatusMessage(StatusMessage.SendRequest);
	                        BindCategoryList();
		                    ChangeStatusMessage(StatusMessage.Waiting);
	                }
	                catch (Exception ex)
	                {
		                    DisplayErrorMessage(ex.Message);
		                    Console.WriteLine(ex.StackTrace);
	                }
            }

            private void BindCategoryList()
            {
                ClearImageList();
                ClearCategoryList();

                var serverAddress = GetServerAddress();
                var serverPortNum = GetServerPortNumber();

                var categoryList = Controller.GetCategoryList(serverAddress, serverPortNum);
                ChangeStatusMessage(StatusMessage.ReceiveResponse);
                Thread.Sleep(500);
                ChangeStatusMessage(StatusMessage.ProcessData);
                Thread.Sleep(500);

                m_CategoryListDDL.Items.Add("(select one)");
                foreach (var categoryItem in categoryList)
                {
                    m_CategoryListDDL.Items.Add(new KeyValuePair<int, string>(categoryItem.Key, categoryItem.Value));
                }

                m_CategoryListDDL.SelectedIndex = 0;
                m_CategoryListDDL.DropDownStyle = ComboBoxStyle.DropDownList;
                m_CategoryListDDL.Enabled = true;
            }

            private void m_CategoryListDDL_SelectedIndexChanged(object a_Sender, EventArgs a_E)
            {
                ClearImageList();
            }

            private void m_GetImageListBTN_Click(object a_Sender, EventArgs a_E)
	        {
	                try
	                {
		                    ChangeStatusMessage(StatusMessage.SendRequest);
	                        BindImageList();
                            ChangeStatusMessage(StatusMessage.Waiting);
	                }
	                catch (Exception ex)
	                {
		                    DisplayErrorMessage(ex.Message);
		                    Console.WriteLine(ex.StackTrace);
	                }
            }

            private void BindImageList()
            {
                ClearImageList();

                if (m_CategoryListDDL.SelectedIndex > 0 && m_CategoryListDDL.Items[m_CategoryListDDL.SelectedIndex].ToString().Length > 0)
                {
                    var serverAddress = GetServerAddress();
                    var serverPortNum = GetServerPortNumber();

                    var itemKeyValuePair = (KeyValuePair<int, string>) m_CategoryListDDL.SelectedItem;
                    int categoryIndex = itemKeyValuePair.Key;

                    var imageList = Controller.GetImageList(serverAddress, serverPortNum, categoryIndex);
                    ChangeStatusMessage(StatusMessage.ReceiveResponse);
                    Thread.Sleep(500);
                    ChangeStatusMessage(StatusMessage.ProcessData);
                    Thread.Sleep(500);

                    foreach (var imageItem in imageList)
                        m_ImageListLB.Items.Add(new KeyValuePair<int, string>(imageItem.Key, imageItem.Value));

                    m_ImageListLB.ForeColor = Color.Black;
                    m_ImageListLB.Enabled = true;
                    m_ImageListLB.ClearSelected();
                }
                else
                {
                    ChangeStatusMessage(StatusMessage.Error);
                    DisplayErrorMessage("Please select a Category first.");
                }
            }

            private void m_ImageListLB_DoubleClick(object a_Sender, EventArgs a_E)
	        {
	                try
	                {
	                        m_ImageListLB.Enabled = false;
		                    ChangeStatusMessage(StatusMessage.SendRequest);
                            BindImage();
                            m_ImageListLB.Enabled = true;
                            ChangeStatusMessage(StatusMessage.Waiting);
	                }
	                catch (Exception ex)
	                {
		                    DisplayErrorMessage(ex.Message);
		                    Console.WriteLine(ex.StackTrace);
	                }
	        }

            private void BindImage()
            {
                if (m_ImageListLB.SelectedIndex != -1 &&
                    m_ImageListLB.Items[m_ImageListLB.SelectedIndex].ToString().Length > 0)
                {
                    var serverAddress = GetServerAddress();
                    var serverPortNum = GetServerPortNumber();

                    var itemKeyValuePair = (KeyValuePair<int, string>) m_ImageListLB.SelectedItem;
                    int imageIndex = itemKeyValuePair.Key;

                    var image = Controller.GetImage(serverAddress, serverPortNum, imageIndex);
                    ChangeStatusMessage(StatusMessage.ReceiveResponse);
                    Thread.Sleep(500);
                    ChangeStatusMessage(StatusMessage.ProcessData);
                    Thread.Sleep(500);
                    ShowImage(image);
                }
                else
                {
                    ChangeStatusMessage(StatusMessage.Error);
                    DisplayErrorMessage("Unable to obtain image.");
                }
            }

            private void ShowImage(Image a_Image)
            {
                PictureBox pictureBox = new PictureBox
                {
                    Dock = DockStyle.Fill,
                    Image = a_Image,
                    SizeMode = PictureBoxSizeMode.StretchImage
                };

                Form picWindow = new Form();
                picWindow.Controls.Add(pictureBox);
                picWindow.Show();
            }

            public void DisplayErrorMessage(string a_Message)
            {
                    ClearImageList();
                    MessageBox.Show(a_Message);
	                ChangeStatusMessage(StatusMessage.Error);

                    if (!a_Message.Contains(Convert.ToString(Protocol.ServerMessage.Reset))) 
                        return;

                    const string Message = "RESET request received from server. Resetting form...";
                    MessageBox.Show(Message);
                    Application.DoEvents();
                    Thread.Sleep(2000);
                    ResetForm();
            }

	        private void ChangeStatusMessage(StatusMessage a_StatusMessage)
	        {
	                string messageStr = "";
	                switch (a_StatusMessage)
	                {
		                case StatusMessage.Waiting:
		                    messageStr = "Waiting for next command";
		                    break;
		                case StatusMessage.SendRequest:
		                    messageStr = "Sending request to server...";
		                    break;
		                case StatusMessage.ReceiveResponse:
		                    messageStr = "Receiving message from server...";
		                    break;
		                case StatusMessage.ProcessData:
		                    messageStr = "Processing data...";
		                    break;
		                case StatusMessage.Error:
		                    messageStr = "ERROR. See message.";
		                    break;
	                }

	                m_StatusTB.Text = messageStr;
	                Application.DoEvents();
	        }

            private void ClearImageList()
            {
                if (m_ImageListLB.Items.Count > 0)
                    m_ImageListLB.Items.Clear();
            }

            private void ClearCategoryList()
            {
                if (m_CategoryListDDL.Items.Count > 0)
                    m_CategoryListDDL.Items.Clear();
            }

	        private void ResetForm()
	        {
	                ClearImageList();
	                ClearCategoryList();
	                ChangeStatusMessage(StatusMessage.Waiting);
	        }

    }
}
