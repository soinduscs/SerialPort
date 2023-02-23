using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Soindus.Interfaces.ApiRest.Clases
{
    public class Maestro
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string GestionLote { get; set; }
        public string UnidadMedidaInventario { get; set; }
    }
}