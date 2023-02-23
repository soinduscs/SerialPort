using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBCSoftLib = EBCSoft.SerialPortCapturer.Library;

namespace EBCSoft.SerialPortCapturer
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            while (true)
            {
                System.Threading.Thread.Sleep(500);
                EBCSoftLib.PuertoSerie puertoSerie = new EBCSoftLib.PuertoSerie();
                Console.WriteLine(puertoSerie.respuesta);
                ////System.Threading.Thread.Sleep(5000);
                ///
                //System.Threading.Thread.Sleep(150);
                //RegistrarCaptura regCap = new RegistrarCaptura()
                //{
                //    //UrlApi = @"https://localhost:44328/SOcomData/RegCom",
                //    UrlApi = @"http://190.13.134.173/Soindus/SoindusApi/RegCom",
                //    Com_ID = "BALANZA1",
                //    User_ID = "TEST",
                //    StrValue = random.Next(10000, 30000).ToString()
                //};
                //string result = regCap.Registrar();
            }
        }
    }
}
