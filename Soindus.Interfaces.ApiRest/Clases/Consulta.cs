using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Soindus.Interfaces.ApiRest.Clases
{
    public class Consulta
    {
        public string TipoDoc { get; set; }
        public string DocEntry { get; set; }
        public string DocNum { get; set; }
        public string LineNum { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string OpenQty { get; set; }
        public string Quantity { get; set; }
        public string Unidad { get; set; }
        public string WhsCode { get; set; }
        public string Currency { get; set; }
        public string Rate { get; set; }
        public string VatPrcnt { get; set; }
        public string Price { get; set; }
        public string TotalLinea { get; set; }
        public string U_SDG_DTE_TT { get; set; }
        public string U_SDG_DTE_CHOFER { get; set; }
        public string U_SDG_DTE_PATENTE { get; set; }
        public string U_SDG_DTE_RUT_CHOFER { get; set; }
        public string U_SDG_DTE_FACRESERVA { get; set; }
        public string U_SDG_DTE_AUTO_PRN { get; set; }
        public string U_SDG_DTE_PRINTER { get; set; }
        public string U_SDG_DTE_MAIL_PDF { get; set; }
        public string U_Email_Chofer { get; set; }
    }
}