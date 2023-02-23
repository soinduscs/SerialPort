using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Soindus.Interfaces.ApiRest.Clases
{
    public class SocioNegocio
    {
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string validFor { get; set; }
        public string frozenFor { get; set; }
        public string ClienteAduana { get; set; }
    }
}