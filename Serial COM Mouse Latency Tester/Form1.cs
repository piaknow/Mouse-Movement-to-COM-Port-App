using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SerialCOM
{
    public partial class Form1 : Form
    {
        private SerialPort serialPort1;
        private MySerialCOM mySerialCOM;
        byte[] byteArray;
        string rx_str;
        private ASCIIEncoding rx_ascii;
        delegate void dlg_WriteRxText(string _str);
        dlg_WriteRxText Dlg_WriteRxText;

        //mouse input
        const int WM_INPUT = 0x00FF;
        const int RIM_TYPEMOUSE = 0;
        const int RID_INPUT = 0x10000003;

        [StructLayout(LayoutKind.Sequential)]
        public struct RAWINPUTDEVICE
        {
            public ushort usUsagePage;
            public ushort usUsage;
            public uint dwFlags;
            public IntPtr hwndTarget;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RAWINPUTHEADER
        {
            public uint dwType;
            public uint dwSize;
            public IntPtr hDevice;
            public IntPtr wParam;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct RAWMOUSE
        {
            [MarshalAs(UnmanagedType.U2)]
            [FieldOffset(0)]
            public ushort usFlags;
            [MarshalAs(UnmanagedType.U4)]
            [FieldOffset(4)]
            public uint ulButtons;
            [FieldOffset(4)]
            public ushort usButtonFlags;
            [FieldOffset(4)]
            public ushort usButtonData;
            [MarshalAs(UnmanagedType.U4)]
            [FieldOffset(8)]
            public uint ulRawButtons;
            [FieldOffset(12)]
            public int lLastX;
            [FieldOffset(16)]
            public int lLastY;
            [MarshalAs(UnmanagedType.U4)]
            [FieldOffset(20)]
            public uint ulExtraInformation;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RAWINPUT
        {
            public RAWINPUTHEADER header;
            public RAWMOUSE mouse;
        }

        [DllImport("user32.dll")]
        extern static bool RegisterRawInputDevices(RAWINPUTDEVICE[] pRawInputDevices, uint uiNumDevices, uint cbSize);

        [DllImport("user32.dll")]
        extern static uint GetRawInputData(IntPtr hRawInput, uint uiCommand, IntPtr pData, ref uint pcbSize, uint cbSizeHeader);


        public Form1()
        {
            InitializeComponent();
            InitializeRawInput();
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
        }

        private void InitializeRawInput()
        {
            RAWINPUTDEVICE[] Rid = new RAWINPUTDEVICE[1];

            Rid[0].usUsagePage = 0x01;
            Rid[0].usUsage = 0x02; // Mouse
            Rid[0].dwFlags = 0;
            Rid[0].hwndTarget = this.Handle;

            if (!RegisterRawInputDevices(Rid, (uint)Rid.Length, (uint)Marshal.SizeOf(Rid[0])))
            {
                MessageBox.Show("Failed to register raw input devices.");
            }
        }

        byte[] data = System.Text.Encoding.ASCII.GetBytes("a");//アスキーに変換
        bool movingleft = false;
        protected override void WndProc(ref Message message)
        {
            if (message.Msg == WM_INPUT)
            {
                uint dwSize = 0;
                GetRawInputData(message.LParam, RID_INPUT, IntPtr.Zero, ref dwSize, (uint)Marshal.SizeOf(typeof(RAWINPUTHEADER)));
                IntPtr buffer = Marshal.AllocHGlobal((int)dwSize);
                if (GetRawInputData(message.LParam, RID_INPUT, buffer, ref dwSize, (uint)Marshal.SizeOf(typeof(RAWINPUTHEADER))) == dwSize)
                {
                    RAWINPUT rawInput = Marshal.PtrToStructure<RAWINPUT>(buffer);
                    if (rawInput.header.dwType == RIM_TYPEMOUSE)
                    { 
                        if (serialopen == true)
                        {
                            if (rawInput.mouse.lLastX < 0 && movingleft == false)
                            {
                                serialPort1.Write(data, 0, data.Length);
                                movingleft = true;
                            }
                            else if(rawInput.mouse.lLastX > 0 && movingleft == true)
                            {
                                movingleft = false;
                            }
                        //labelmouse.Text = $"dX: {rawInput.mouse.lLastX}, dY: {-rawInput.mouse.lLastY}";
                        //xSum += rawInput.mouse.lLastX;
                        //labelmouse.Text = $"xSum: {xSum}";
                        }
                    }
                }
                Marshal.FreeHGlobal(buffer);
            }

            base.WndProc(ref message);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            serialPort1 = new SerialPort();
            mySerialCOM = new MySerialCOM();

            serialPort1.DataReceived += new SerialDataReceivedEventHandler(myDataReceivedHandler);
            rx_ascii = new ASCIIEncoding();

            mySerialCOM.portName = SerialPort.GetPortNames();
            foreach (string pname in mySerialCOM.portName)
            {
                cBx_PortName.Items.Add(pname);
            }
            if (cBx_PortName.Items.Count > 0)
            {
                cBx_PortName.SelectedIndex = 0;
            }

            foreach (MySerialCOM.BaudRateItem item in mySerialCOM.baudRateItems)
            {
                cBx_BaudRateValue.Items.Add(item.rateName);
            }
            if (cBx_BaudRateValue.Items.Count > 0)
            {
                cBx_BaudRateValue.SelectedIndex = 0;
            }


            richTextBox1.Clear();

            Dlg_WriteRxText = AppendText;
        }


        bool serialopen = false;
        private void bt_Connect_Click(object sender, EventArgs e)
        {
            int baudrate_select_num;

            if (serialPort1.IsOpen == true)
            {
                serialPort1.DiscardInBuffer();
                serialPort1.DiscardOutBuffer();
                serialPort1.Close();
                bt_Connect.Text = "Connect";
                serialopen = false;
            }
            else
            {
                if (cBx_PortName.Items == null || string.IsNullOrEmpty(cBx_PortName.Text))
                {
                    MessageBox.Show("No device is connected");
                }
                else
                {
                    richTextBox1.Focus();
                    bt_Connect.Text = "Disconnect";

                    serialPort1.PortName = cBx_PortName.SelectedItem.ToString();
                    baudrate_select_num = cBx_BaudRateValue.SelectedIndex;
                    serialPort1.BaudRate = mySerialCOM.baudRateItems[baudrate_select_num].rateValue;
                    serialPort1.DataBits = 8;
                    serialPort1.Parity = Parity.None;
                    serialPort1.StopBits = StopBits.One;
                    serialPort1.Handshake = Handshake.None;
                    serialPort1.Encoding = Encoding.ASCII;
                    serialPort1.ReceivedBytesThreshold = 1;
                    serialPort1.Open();
                    serialPort1.DiscardInBuffer();
                    serialPort1.DiscardOutBuffer();
                    serialopen = true;
                }
            }
        }


        //serial port receive handler
        int collecttimes = 0;
        private void myDataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort SP = (SerialPort)sender;
            if (SP.BytesToRead > 0)
            {
                byteArray = new byte[SP.BytesToRead];
                SP.Read(byteArray, 0, byteArray.Length);
                rx_str = rx_ascii.GetString(byteArray);
                AppendText(rx_str);
                collecttimes++;
                if(collecttimes > 9)
                {
                    GC.Collect();
                    collecttimes = 0;
                }
            }
        }

        //rich text box
        private void AppendText(string _str)
        {
            if (richTextBox1.InvokeRequired)
            {
                BeginInvoke(Dlg_WriteRxText, _str);
            }
            else
            {
                richTextBox1.HideSelection = false;
                richTextBox1.AppendText(_str);
            }
        }

        private void bt_sendData_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
            {
                return;
            }

            string str = txb_sendData.Text;
            str += "\n";
            if (string.IsNullOrEmpty(str) == true)
            {
                return;
            }
            byte[] data = System.Text.Encoding.ASCII.GetBytes(str);//アスキーに変換
            serialPort1.Write(data, 0, data.Length);
        }


        //make the window fullscreen
        bool cursorstate = true;
        private void FullScreen_Click(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            //TopMost = true;
            this.Enabled = false;
            if (cursorstate == true)
            { 
                Cursor.Hide();
                cursorstate = false;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = FormWindowState.Normal;
                //TopMost = false;
                this.Enabled = true;
                if (cursorstate == false)
                {
                    Cursor.Show();
                    cursorstate = true;
                }
            }
            else if (e.KeyCode == Keys.F11)
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
                //TopMost = true;
                this.Enabled = false;
                if (cursorstate == true)
                {
                    Cursor.Hide();
                    cursorstate = false;
                }
            }
        }

        private void Windowed_Click(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.Sizable;
            WindowState = FormWindowState.Normal;
            //TopMost = false;
            this.Enabled = true;
            if (cursorstate == false)
            {
                Cursor.Show();
                cursorstate = true;
            }
        }
    }
}
