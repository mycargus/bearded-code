using System.ComponentModel;
using System.Windows.Forms;

namespace ImageClient
{
    partial class View
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
            this.m_ServerAddressDDL = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_GetCategoryListBTN = new System.Windows.Forms.Button();
            this.m_GetImageListBTN = new System.Windows.Forms.Button();
            this.m_StatusTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_CategoryListDDL = new System.Windows.Forms.ComboBox();
            this.m_ImageListLB = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // m_ServerAddressDDL
            // 
            this.m_ServerAddressDDL.Enabled = false;
            this.m_ServerAddressDDL.FormattingEnabled = true;
            this.m_ServerAddressDDL.Location = new System.Drawing.Point(138, 51);
            this.m_ServerAddressDDL.Margin = new System.Windows.Forms.Padding(2);
            this.m_ServerAddressDDL.Name = "m_ServerAddressDDL";
            this.m_ServerAddressDDL.Size = new System.Drawing.Size(227, 21);
            this.m_ServerAddressDDL.TabIndex = 0;
            this.m_ServerAddressDDL.SelectedIndexChanged += new System.EventHandler(this.m_ServerAddressDDL_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server Address:";
            // 
            // m_GetCategoryListBTN
            // 
            this.m_GetCategoryListBTN.Location = new System.Drawing.Point(53, 99);
            this.m_GetCategoryListBTN.Name = "m_GetCategoryListBTN";
            this.m_GetCategoryListBTN.Size = new System.Drawing.Size(155, 23);
            this.m_GetCategoryListBTN.TabIndex = 2;
            this.m_GetCategoryListBTN.Text = "Get Category List";
            this.m_GetCategoryListBTN.UseVisualStyleBackColor = true;
            this.m_GetCategoryListBTN.Click += new System.EventHandler(this.m_GetCategoryListBTN_Click);
            // 
            // m_GetImageListBTN
            // 
            this.m_GetImageListBTN.Location = new System.Drawing.Point(214, 99);
            this.m_GetImageListBTN.Name = "m_GetImageListBTN";
            this.m_GetImageListBTN.Size = new System.Drawing.Size(151, 23);
            this.m_GetImageListBTN.TabIndex = 3;
            this.m_GetImageListBTN.Text = "Get Image List";
            this.m_GetImageListBTN.UseVisualStyleBackColor = true;
            this.m_GetImageListBTN.Click += new System.EventHandler(this.m_GetImageListBTN_Click);
            // 
            // m_StatusTB
            // 
            this.m_StatusTB.AccessibleDescription = "static";
            this.m_StatusTB.Location = new System.Drawing.Point(138, 147);
            this.m_StatusTB.Name = "m_StatusTB";
            this.m_StatusTB.ReadOnly = true;
            this.m_StatusTB.Size = new System.Drawing.Size(227, 20);
            this.m_StatusTB.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(92, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Status:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Category:";
            // 
            // m_CategoryListDDL
            // 
            this.m_CategoryListDDL.Enabled = false;
            this.m_CategoryListDDL.FormattingEnabled = true;
            this.m_CategoryListDDL.Location = new System.Drawing.Point(139, 184);
            this.m_CategoryListDDL.Name = "m_CategoryListDDL";
            this.m_CategoryListDDL.Size = new System.Drawing.Size(226, 21);
            this.m_CategoryListDDL.TabIndex = 7;
            // 
            // m_ImageListLB
            // 
            this.m_ImageListLB.Enabled = false;
            this.m_ImageListLB.FormattingEnabled = true;
            this.m_ImageListLB.HorizontalScrollbar = true;
            this.m_ImageListLB.Location = new System.Drawing.Point(53, 225);
            this.m_ImageListLB.Name = "m_ImageListLB";
            this.m_ImageListLB.ScrollAlwaysVisible = true;
            this.m_ImageListLB.Size = new System.Drawing.Size(312, 225);
            this.m_ImageListLB.TabIndex = 8;
            this.m_ImageListLB.DoubleClick += new System.EventHandler(this.m_ImageListLB_DoubleClick);
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 509);
            this.Controls.Add(this.m_ImageListLB);
            this.Controls.Add(this.m_CategoryListDDL);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_StatusTB);
            this.Controls.Add(this.m_GetImageListBTN);
            this.Controls.Add(this.m_GetCategoryListBTN);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_ServerAddressDDL);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "View";
            this.Text = "Image Client - Michael Hargiss";
            this.ResumeLayout(false);
            this.PerformLayout();

	}

	#endregion

	private ComboBox m_ServerAddressDDL;
    private Label label1;
    private Button m_GetCategoryListBTN;
    private Button m_GetImageListBTN;
    private Label label2;
    private Label label3;
    private ComboBox m_CategoryListDDL;
    protected TextBox m_StatusTB;
    protected ListBox m_ImageListLB;
    }
}

