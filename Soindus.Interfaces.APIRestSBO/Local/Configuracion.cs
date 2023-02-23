using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace Soindus.Interfaces.APIRestSBO.Local
{
    public class Configuracion
    {
        public string TOKEN { get; set; }
        public Params Params { get; set; }

        public Configuracion()
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
        public string DbServerType { get; set; }
        public string Server { get; set; }
        public string LicenseServer { get; set; }
        public string CompanyDB { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DbUserName { get; set; }
        public string DbPassword { get; set; }
    }
}