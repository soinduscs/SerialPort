using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Modelo = Soindus.Interfaces.Modelos.SOcomData;
using Newtonsoft.Json;
using System.IO;

namespace Soindus.Svc.SerialPort.ApiRestSBO
{
    public class RegistrarCaptura
    {
        public string UrlApi { get; set; }
        public string Com_ID { get; set; }
        public string User_ID { get; set; }
        public string StrValue { get; set; }
        private Modelo.RegCom regCom = null;

        public RegistrarCaptura()
        {
            regCom = new Modelo.RegCom();
        }

        public string Registrar()
        {
            regCom.Com_ID = Com_ID;
            regCom.User_ID = User_ID;
            regCom.LastUpdate = DateTime.Now;
            regCom.StrValue = StrValue;

            string respuesta = "";
            DateTime _hoy = DateTime.Now;
            DateTime _fecha = new DateTime(_hoy.Year, _hoy.Month, _hoy.Day);
            string _url = UrlApi;
            HttpWebRequest _request = (HttpWebRequest)WebRequest.Create(_url);
            //SSL, TLS certificate
            System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(ValidarCertificado);
            //_request.ServerCertificateValidationCallback = TrueValidator;
            _request.Timeout = 110000;
            _request.Method = WebRequestMethods.Http.Post;
            _request.ContentType = "application/json; odata.metadata=none";
            //_request.Headers.Add("Token", strToken);

            string postData = Newtonsoft.Json.JsonConvert.SerializeObject(regCom);
            //ASCIIEncoding encoding = new ASCIIEncoding();
            //byte[] byte1 = encoding.GetBytes(postData);
            //_request.ContentLength = byte1.Length;
            //Stream newStream = _request.GetRequestStream();
            //newStream.Write(byte1, 0, byte1.Length);
            _request.ContentLength = postData.Length;
            using (StreamWriter writer = new StreamWriter(_request.GetRequestStream()))
            {
                writer.Write(postData);
            }
            using (HttpWebResponse _response = (HttpWebResponse)_request.GetResponse())
            {
                if (_response.StatusCode.ToString() == "OK")
                {
                    using (var reader = new System.IO.StreamReader(_response.GetResponseStream()))
                    {
                        var objText = reader.ReadToEnd();
                        respuesta = objText.ToString();
                    }
                }
            }
            _request.KeepAlive = true;
            return respuesta;
        }

        private static bool TrueValidator(object sender, X509Certificate certificate, X509Chain chain,
         SslPolicyErrors sslpolicyerrors)
        {
            return sslpolicyerrors == SslPolicyErrors.None;
        }

        private Boolean ValidarCertificado(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        private static bool RemoteSSLTLSCertificateValidate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate cert, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors ssl)
        {
            //accept
            return true;
        }
    }
}
