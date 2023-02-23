using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Reflection;

namespace EBCSoft.SerialPortCapturer.Library
{
    public class PuertoSerieConfig
    {
        private string _portname = "COM1"; //COM1
        private string _baudrate = "9600"; //9600
        private string _parity = "0"; //Parity.None
        private string _databits = "8"; //8
        private string _stopbits = "1"; //StopBits.One
        private string _handshake = "0"; // Handshake.None
        private string _delayinterval = "100"; //1000
        private string _writetimeout = "1000"; //1000
        private string _readtimeout = "40000"; //40000
        private string _maxtrysec = "3"; //3
        private string _maxtrytimeoutsec = "60"; //60
        private string _sendstring = ""; //P
        private string _sendcrlf = "Y"; //Si
        private string _decimalsep = ","; //,
        private string _decimalsepexit = ","; //,
        private string _valueextract = "Y"; //Si
        private string _intpart = "5,4"; //5,4
        private string _decpart = "9,1"; //9,1
        public string PortName
        {
            get { return getConfValue("PortName", _portname); }
        }
        public string BaudRate
        {
            get { return getConfValue("BaudRate", _baudrate); }
        }
        public string Parity
        {
            get { return getConfValue("Parity", _parity); }
        }
        public string DataBits
        {
            get { return getConfValue("DataBits", _databits); }
        }
        public string StopBits
        {
            get { return getConfValue("StopBits", _stopbits); }
        }
        public string Handshake
        {
            get { return getConfValue("Handshake", _handshake); }
        }
        public string DelayInterval
        {
            get { return getConfValue("DelayInterval", _delayinterval); }
        }
        public string WriteTimeOut
        {
            get { return getConfValue("WriteTimeOut", _writetimeout); }
        }
        public string ReadTimeOut
        {
            get { return getConfValue("ReadTimeOut", _readtimeout); }
        }
        public string MaxTrySec
        {
            get { return getConfValue("MaxTrySec", _maxtrysec); }
        }
        public string MaxTryTimeOutSec
        {
            get { return getConfValue("MaxTryTimeOutSec", _maxtrytimeoutsec); }
        }
        public string SendString
        {
            get { return getConfValue("SendString", _sendstring); }
        }
        public string SendCrLf
        {
            get { return getConfValue("SendCrLf", _sendcrlf); }
        }
        public string DecimalSep
        {
            get { return getConfValue("DecimalSep", _decimalsep); }
        }
        public string DecimalSepExit
        {
            get { return getConfValue("DecimalSepExit", _decimalsepexit); }
        }
        public string ValueExtract
        {
            get { return getConfValue("ValueExtract", _valueextract); }
        }
        public string IntPart
        {
            get { return getConfValue("IntPart", _intpart); }
        }
        public string DecPart
        {
            get { return getConfValue("DecPart", _decpart); }
        }
        private string getConfValue(string key, string defval)
        {
            string sValue = string.Empty;
            string ConfigFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase), @"PuertoSerieConfig.xml");
            XmlDocument document = new XmlDocument();
            document.Load(ConfigFile);
            XmlNodeList nodes = document.SelectNodes("/Settings/SerialPort");
            foreach (XmlNode node in nodes)
            {
                try
                {
                    sValue = node[key].InnerText;
                }
                catch
                {
                }
            }
            if (sValue == string.Empty)
            {
                sValue = defval;
            }
            return sValue;
        }
    }
}
