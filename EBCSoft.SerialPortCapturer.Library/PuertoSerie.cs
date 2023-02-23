using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.IO.Ports;

namespace EBCSoft.SerialPortCapturer.Library
{
    public class PuertoSerie
    {
        public System.IO.Ports.SerialPort gSerialPort;
        public string mensaje = string.Empty;
        public string saltolinea = "N";
        public string respuesta = string.Empty;
        public int msecDelay = 3000;
        public int msecSend = 1000;
        public int msecTimeOut = 40000;
        public int intentos = 0;
        public int maxintentos = 3;
        public int maxintentosglobal = 60;
        public string extraevalores = "N";
        public string parteentera = "0,0";
        public string partedecimal = "0,0";
        public string separadorsalida = ",";

        public PuertoSerie()
        {
            PuertoSerieConfig SPconfig = new PuertoSerieConfig();
            saltolinea = SPconfig.SendCrLf;
            mensaje = SPconfig.SendString;
            respuesta = string.Empty;
            gSerialPort = new System.IO.Ports.SerialPort();
            gSerialPort.PortName = SPconfig.PortName;
            gSerialPort.BaudRate = Convert.ToInt32(SPconfig.BaudRate);
            gSerialPort.Parity = (Parity)Enum.Parse(typeof(Parity), SPconfig.Parity);
            gSerialPort.DataBits = Convert.ToInt32(SPconfig.DataBits);
            gSerialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), SPconfig.StopBits);
            gSerialPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), SPconfig.Handshake);
            msecDelay = Convert.ToInt32(SPconfig.DelayInterval);
            msecSend = Convert.ToInt32(SPconfig.WriteTimeOut);
            gSerialPort.WriteTimeout = msecSend;
            msecTimeOut = Convert.ToInt32(SPconfig.ReadTimeOut);
            gSerialPort.ReadTimeout = msecTimeOut;
            maxintentos = Convert.ToInt32(SPconfig.MaxTrySec);
            maxintentosglobal = Convert.ToInt32(SPconfig.MaxTryTimeOutSec);
            extraevalores = SPconfig.ValueExtract;
            parteentera = SPconfig.IntPart;
            partedecimal = SPconfig.DecPart;
            separadorsalida = SPconfig.DecimalSepExit;
            Capturar();
        }

        public void Capturar()
        {
            //Thread _PuertoSerieThread = new Thread(this.CapturarThread);
            //_PuertoSerieThread.IsBackground = true;
            //_PuertoSerieThread.Priority = ThreadPriority.Highest;
            //_PuertoSerieThread.Start();
            CapturarThread();
        }
        private void CapturarThread()
        {
            respuesta = string.Empty;
            try
            {
                //this.RecvResponse(msecDelay);
                do
                {
                    try
                    {
                        this.RecvResponse(msecDelay);
                    }
                    catch (Exception ex)
                    {
                        if (gSerialPort.IsOpen == true)
                        {
                            gSerialPort.Close();
                        }
                        respuesta = "ERROR" + ex.Message;
                    }
                    if (intentos > maxintentosglobal)
                    {
                        respuesta = string.Format("Se excedió el tiempo máximo de espera: {0} seg.", maxintentosglobal);
                    }
                } while (respuesta == string.Empty);
                if (gSerialPort.IsOpen == true)
                    {
                        gSerialPort.Close();
                    }
                }
            catch (Exception ex)
            {
                if (gSerialPort.IsOpen == true)
                {
                    gSerialPort.Close();
                }
                respuesta = ex.Message;
                //respuesta = "ERROR";
            }
        }
        private void RecvResponse(int _msecDelay)
        {
            if (gSerialPort.IsOpen == false)
            {
                gSerialPort.Open();
            }
            if (gSerialPort.IsOpen == true)
            {
                //if (mensaje != string.Empty)
                //{
                //    if (saltolinea == "Y")
                //    {
                //        gSerialPort.WriteLine(string.Format("{0}\r\n", mensaje));
                //    }
                //    //else
                //    //{
                //    //    gSerialPort.WriteLine(string.Format("{0}", mensaje));
                //    //}
                //}
                //System.Threading.Thread.Sleep(_msecDelay);
                gSerialPort.DiscardNull = true;
                respuesta = gSerialPort.ReadLine();
                respuesta = gSerialPort.ReadLine();
                gSerialPort.DiscardInBuffer();
                gSerialPort.DiscardOutBuffer();
                //gSerialPort.DiscardNull = true;
                //respuesta = gSerialPort.ReadExisting();
                //gSerialPort.DiscardInBuffer();
                //gSerialPort.DiscardOutBuffer();
                if (respuesta != string.Empty)
                {
                    if (extraevalores == "Y")
                    {
                        string nuevovalor = "0";
                        char _sep = ',';
                        try
                        {
                            string[] pEntera = parteentera.Split(_sep);
                            Int32 pos1 = Convert.ToInt32(pEntera[0]);
                            Int32 len1 = Convert.ToInt32(pEntera[1]);
                            if (len1 > 0)
                            {
                                nuevovalor = respuesta.Substring(pos1, len1).ToString().Trim();
                            }
                        }
                        catch (Exception ex)
                        {
                            nuevovalor = "0";
                        }
                        respuesta = nuevovalor;

                        //string nuevovalor = "0" + separadorsalida + "0";
                        //char _sep = ',';
                        //try
                        //{
                        //    string[] pEntera = parteentera.Split(_sep);
                        //    Int32 pos1 = Convert.ToInt32(pEntera[0]);
                        //    Int32 len1 = Convert.ToInt32(pEntera[1]);
                        //    string[] pDecimal = partedecimal.Split(_sep);
                        //    Int32 pos2 = Convert.ToInt32(pDecimal[0]);
                        //    Int32 len2 = Convert.ToInt32(pDecimal[1]);
                        //    if (len1 > 0)
                        //    {
                        //        nuevovalor = respuesta.Substring(pos1, len1).ToString().Trim();
                        //    }
                        //    else
                        //    {
                        //        nuevovalor = "0";
                        //    }
                        //    if (len2 > 0)
                        //    {
                        //        nuevovalor = nuevovalor + separadorsalida + respuesta.Substring(pos2, len2).ToString().Trim();
                        //    }
                        //    else
                        //    {
                        //        nuevovalor = nuevovalor + separadorsalida + "0";
                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    nuevovalor = "0" + separadorsalida + "0";
                        //}
                        //respuesta = nuevovalor;
                    }
                }
                if (string.IsNullOrEmpty(respuesta))
                {
                    respuesta = "0";
                }
                else
                {
                    string pattern = "^([\\s]*)([0-9]{1,6})$";
                    if (!Regex.IsMatch(respuesta, pattern))
                    {
                        respuesta = "0";
                    }
                }
                //if (string.IsNullOrEmpty(respuesta))
                //{
                //    respuesta = "0,0";
                //}
                //else
                //{
                //    string pattern = "^([\\s]*)([0-9]{1,6})([" + separadorsalida + "]{1})([0-9]+)$";
                //    if (!Regex.IsMatch(respuesta, pattern))
                //    {
                //        respuesta = "0,0";
                //    }
                //}
            }
            intentos++;
        }
    }
}
