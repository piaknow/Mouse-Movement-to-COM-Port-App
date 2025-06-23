using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialCOM
{
    public class MySerialCOM
    {
        public byte[] readBuffer;
        public const int rxBufSizeMax = 1000;

        public string[] portName;
        public BaudRateItem[] baudRateItems;

        public struct BaudRateItem
        {
            public string rateName;
            public int rateValue;

            public BaudRateItem(string _baudRateName, int _baudRateValue)
            {
                rateName = _baudRateName;
                rateValue = _baudRateValue;
            }
        }

        public MySerialCOM()
        {
            readBuffer = new byte[rxBufSizeMax];
            string[] baudRateNames = { "4800bps", "9600bps", "19200bps", "38400bps", "57600bps", "115200bps", "500000bps" };
            int[] baudRateValues = { 4800, 9600, 19200, 38400, 57600, 115200, 500000 };
            baudRateItems = new BaudRateItem[baudRateNames.Length];
            for (int i = 0; i < baudRateNames.Length; i++)
            {
                baudRateItems[i] = new BaudRateItem(baudRateNames[i], baudRateValues[i]);
            }
        }
    }
}
