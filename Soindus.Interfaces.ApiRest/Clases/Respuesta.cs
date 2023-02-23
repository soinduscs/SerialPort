using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Soindus.Interfaces.ApiRest.Clases
{
    public class Respuesta
    {
        public string Estado { get; set; }
        public string Interno { get; set; }
        public string Documento { get; set; }
        public int? ErrCode { get; set; }
        public string ErrMsg { get; set; }
        public Documento DocumentoSBO { get; set; }
        public Traslado TrasladoSBO { get; set; }
    }
}