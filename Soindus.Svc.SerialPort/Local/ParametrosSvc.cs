using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Soindus.Svc.SerialPort.Local
{
    public class ParametrosSvc
    {
        public string TOKEN { get; set; }
        public Params Params { get; set; }

        public ParametrosSvc()
        {
            SetParametros();
        }

        private void SetParametros()
        {
            TOKEN = ConfigurationManager.AppSettings["TOKEN"].ToString();
            Params = ObtenerParametrosToken(TOKEN);
        }

        private Params ObtenerParametrosToken(string Token)
        {
            Params result = new Params();
            // Decodificar el string base 64
            string origen = Token.Replace("dG9rZW4gZGUgc2VndXJpZGFk.", "");
            Byte[] datos = Convert.FromBase64String(origen);
            origen = Encoding.UTF8.GetString(datos);
            result = JsonConvert.DeserializeObject<Params>(origen);
            return result;
        }
    }

    public class Params
    {
        public string UrlApiRestSBO { get; set; }
        public string ComID { get; set; }
        public string UserID { get; set; }
        public int TimerMS { get; set; }
    }
}
