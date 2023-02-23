using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Soindus.Interfaces.ApiRest.Clases
{
    public class Traslado
    {
        public int? DocEntry { get; set; }
        public int? DocNum { get; set; }
        public string CardCode { get; set; }
        public DateTime DocDate { get; set; }
        public DateTime TaxDate { get; set; }
        public string Filler { get; set; }
        public string ToWhsCode { get; set; }
        public string Comments { get; set; }
        public string U_SDG_DTE_TT { get; set; }
        public string U_SDG_DTE_CHOFER { get; set; }
        public string U_SDG_DTE_PATENTE { get; set; }
        public string U_SDG_DTE_RUT_CHOFER { get; set; }
        public string U_SDG_DTE_FACRESERVA { get; set; }
        public string U_SDG_DTE_AUTO_PRN { get; set; }
        public string U_SDG_DTE_PRINTER { get; set; }
        public string U_SDG_DTE_MAIL_PDF { get; set; }
        public string U_Email_Chofer { get; set; }
        public string U_CLIFINAL { get; set; }
        public DetalleTraslado[] Detalle { get; set; }
    }

    public class DetalleTraslado
    {
        public int? BaseDocNum { get; set; }
        public int? DocEntry { get; set; }
        public int? LineNum { get; set; }
        public int? BaseType { get; set; }
        public int? BaseEntry { get; set; }
        public int? BaseLine { get; set; }
        public string ItemCode { get; set; }
        public double Quantity { get; set; }
        public string FromWhsCod { get; set; }
        public string WhsCode { get; set; }
        public string LineStatus { get; set; }
        public Lote[] Lotes { get; set; }
    }
}