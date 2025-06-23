namespace SerialCOM
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.bt_Connect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cBx_PortName = new System.Windows.Forms.ComboBox();
            this.cBx_BaudRateValue = new System.Windows.Forms.ComboBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.txb_sendData = new System.Windows.Forms.TextBox();
            this.bt_sendData = new System.Windows.Forms.Button();
            this.labelmouse = new System.Windows.Forms.Label();
            this.FullScreen = new System.Windows.Forms.Button();
            this.Windowed = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bt_Connect
            // 
            this.bt_Connect.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_Connect.Location = new System.Drawing.Point(12, 12);
            this.bt_Connect.Name = "bt_Connect";
            this.bt_Connect.Size = new System.Drawing.Size(130, 52);
            this.bt_Connect.TabIndex = 0;
            this.bt_Connect.Text = "Connect";
            this.bt_Connect.UseVisualStyleBackColor = true;
            this.bt_Connect.Click += new System.EventHandler(this.bt_Connect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(148, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "COM Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(150, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "baud rate";
            // 
            // cBx_PortName
            // 
            this.cBx_PortName.FormattingEnabled = true;
            this.cBx_PortName.Location = new System.Drawing.Point(209, 14);
            this.cBx_PortName.Name = "cBx_PortName";
            this.cBx_PortName.Size = new System.Drawing.Size(109, 20);
            this.cBx_PortName.TabIndex = 3;
            // 
            // cBx_BaudRateValue
            // 
            this.cBx_BaudRateValue.FormattingEnabled = true;
            this.cBx_BaudRateValue.Location = new System.Drawing.Point(209, 43);
            this.cBx_BaudRateValue.Name = "cBx_BaudRateValue";
            this.cBx_BaudRateValue.Size = new System.Drawing.Size(109, 20);
            this.cBx_BaudRateValue.TabIndex = 4;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 133);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(460, 316);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // txb_sendData
            // 
            this.txb_sendData.Location = new System.Drawing.Point(12, 106);
            this.txb_sendData.Name = "txb_sendData";
            this.txb_sendData.Size = new System.Drawing.Size(379, 19);
            this.txb_sendData.TabIndex = 6;
            // 
            // bt_sendData
            // 
            this.bt_sendData.Location = new System.Drawing.Point(397, 104);
            this.bt_sendData.Name = "bt_sendData";
            this.bt_sendData.Size = new System.Drawing.Size(75, 23);
            this.bt_sendData.TabIndex = 7;
            this.bt_sendData.Text = "Send";
            this.bt_sendData.UseVisualStyleBackColor = true;
            this.bt_sendData.Click += new System.EventHandler(this.bt_sendData_Click);
            // 
            // labelmouse
            // 
            this.labelmouse.AutoSize = true;
            this.labelmouse.Location = new System.Drawing.Point(10, 79);
            this.labelmouse.Name = "labelmouse";
            this.labelmouse.Size = new System.Drawing.Size(62, 12);
            this.labelmouse.TabIndex = 8;
            this.labelmouse.Text = "labelmouse";
            // 
            // FullScreen
            // 
            this.FullScreen.Location = new System.Drawing.Point(324, 12);
            this.FullScreen.Name = "FullScreen";
            this.FullScreen.Size = new System.Drawing.Size(148, 23);
            this.FullScreen.TabIndex = 11;
            this.FullScreen.Text = "FullScreen (F11)";
            this.FullScreen.UseVisualStyleBackColor = true;
            this.FullScreen.Click += new System.EventHandler(this.FullScreen_Click);
            // 
            // Windowed
            // 
            this.Windowed.Location = new System.Drawing.Point(324, 41);
            this.Windowed.Name = "Windowed";
            this.Windowed.Size = new System.Drawing.Size(148, 23);
            this.Windowed.TabIndex = 12;
            this.Windowed.Text = "Windowed (Esc)";
            this.Windowed.UseVisualStyleBackColor = true;
            this.Windowed.Click += new System.EventHandler(this.Windowed_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.Windowed);
            this.Controls.Add(this.FullScreen);
            this.Controls.Add(this.labelmouse);
            this.Controls.Add(this.bt_sendData);
            this.Controls.Add(this.txb_sendData);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.cBx_BaudRateValue);
            this.Controls.Add(this.cBx_PortName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bt_Connect);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Serial COM Mouse Latency Tester";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_Connect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cBx_PortName;
        private System.Windows.Forms.ComboBox cBx_BaudRateValue;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox txb_sendData;
        private System.Windows.Forms.Button bt_sendData;
        private System.Windows.Forms.Label labelmouse;
        private System.Windows.Forms.Button FullScreen;
        private System.Windows.Forms.Button Windowed;
    }
}

