using System.ComponentModel;

namespace ImageServer
{
    partial class ServerUi
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_StopServerBTN = new System.Windows.Forms.Button();
            this.m_ClearListBTN = new System.Windows.Forms.Button();
            this.m_StaticLBL1 = new System.Windows.Forms.Label();
            this.m_StaticLBL2 = new System.Windows.Forms.Label();
            this.m_UserLV = new System.Windows.Forms.ListView();
            this.m_ServerLogLV = new System.Windows.Forms.ListView();
            this.m_StatusLBL = new System.Windows.Forms.Label();
            this.m_AcceptCountLBL = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_StopServerBTN
            // 
            this.m_StopServerBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_StopServerBTN.Location = new System.Drawing.Point(265, 22);
            this.m_StopServerBTN.Name = "m_StopServerBTN";
            this.m_StopServerBTN.Size = new System.Drawing.Size(137, 36);
            this.m_StopServerBTN.TabIndex = 0;
            this.m_StopServerBTN.Text = "Stop Server";
            this.m_StopServerBTN.UseVisualStyleBackColor = true;
            this.m_StopServerBTN.Click += new System.EventHandler(this.m_StopServerBTN_Click);
            // 
            // m_ClearListBTN
            // 
            this.m_ClearListBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_ClearListBTN.Location = new System.Drawing.Point(265, 71);
            this.m_ClearListBTN.Name = "m_ClearListBTN";
            this.m_ClearListBTN.Size = new System.Drawing.Size(137, 36);
            this.m_ClearListBTN.TabIndex = 1;
            this.m_ClearListBTN.Text = "Clear List";
            this.m_ClearListBTN.UseVisualStyleBackColor = true;
            this.m_ClearListBTN.Click += new System.EventHandler(this.m_ClearListBTN_Click);
            // 
            // m_StaticLBL1
            // 
            this.m_StaticLBL1.AutoSize = true;
            this.m_StaticLBL1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_StaticLBL1.Location = new System.Drawing.Point(30, 42);
            this.m_StaticLBL1.Name = "m_StaticLBL1";
            this.m_StaticLBL1.Size = new System.Drawing.Size(55, 16);
            this.m_StaticLBL1.TabIndex = 3;
            this.m_StaticLBL1.Text = "Status:";
            // 
            // m_StaticLBL2
            // 
            this.m_StaticLBL2.AutoSize = true;
            this.m_StaticLBL2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_StaticLBL2.Location = new System.Drawing.Point(30, 80);
            this.m_StaticLBL2.Name = "m_StaticLBL2";
            this.m_StaticLBL2.Size = new System.Drawing.Size(103, 16);
            this.m_StaticLBL2.TabIndex = 5;
            this.m_StaticLBL2.Text = "Accept Count:";
            // 
            // m_UserLV
            // 
            this.m_UserLV.Location = new System.Drawing.Point(33, 130);
            this.m_UserLV.Name = "m_UserLV";
            this.m_UserLV.Size = new System.Drawing.Size(369, 136);
            this.m_UserLV.TabIndex = 6;
            this.m_UserLV.UseCompatibleStateImageBehavior = false;
            // 
            // m_ServerLogLV
            // 
            this.m_ServerLogLV.Location = new System.Drawing.Point(33, 289);
            this.m_ServerLogLV.Name = "m_ServerLogLV";
            this.m_ServerLogLV.Size = new System.Drawing.Size(369, 289);
            this.m_ServerLogLV.TabIndex = 7;
            this.m_ServerLogLV.UseCompatibleStateImageBehavior = false;
            // 
            // m_StatusLBL
            // 
            this.m_StatusLBL.AutoSize = true;
            this.m_StatusLBL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_StatusLBL.Location = new System.Drawing.Point(161, 42);
            this.m_StatusLBL.Name = "m_StatusLBL";
            this.m_StatusLBL.Size = new System.Drawing.Size(0, 16);
            this.m_StatusLBL.TabIndex = 8;
            // 
            // m_AcceptCountLBL
            // 
            this.m_AcceptCountLBL.AutoSize = true;
            this.m_AcceptCountLBL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_AcceptCountLBL.Location = new System.Drawing.Point(161, 80);
            this.m_AcceptCountLBL.Name = "m_AcceptCountLBL";
            this.m_AcceptCountLBL.Size = new System.Drawing.Size(0, 16);
            this.m_AcceptCountLBL.TabIndex = 9;
            // 
            // ServerUi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 598);
            this.Controls.Add(this.m_AcceptCountLBL);
            this.Controls.Add(this.m_StatusLBL);
            this.Controls.Add(this.m_ServerLogLV);
            this.Controls.Add(this.m_UserLV);
            this.Controls.Add(this.m_StaticLBL2);
            this.Controls.Add(this.m_StaticLBL1);
            this.Controls.Add(this.m_ClearListBTN);
            this.Controls.Add(this.m_StopServerBTN);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ServerUi";
            this.Text = "ImageServer - Michael Hargiss";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_StopServerBTN;
        private System.Windows.Forms.Button m_ClearListBTN;
        private System.Windows.Forms.Label m_StaticLBL1;
        private System.Windows.Forms.Label m_StaticLBL2;
        private System.Windows.Forms.ListView m_UserLV;
        private System.Windows.Forms.ListView m_ServerLogLV;
        private System.Windows.Forms.Label m_StatusLBL;
        private System.Windows.Forms.Label m_AcceptCountLBL;
    }
}

