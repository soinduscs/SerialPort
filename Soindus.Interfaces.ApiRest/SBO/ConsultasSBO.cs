using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Soindus.Interfaces.ApiRest.SBO
{
    public class ConsultasSBO
    {
        public static int ObtenerIDConnectedUser(string UserCode)
        {
            var Id = 1;
            SAPbobsCOM.Recordset oRecord = (SAPbobsCOM.Recordset)SBO.ConexionDIAPI.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            string _query = String.Empty;

            _query = @"SELECT ""USERID"" FROM ""OUSR"" WHERE ""USER_CODE"" = '" + UserCode + @"'";
            oRecord.DoQuery(_query);

            // Si hay datos
            if (!oRecord.EoF)
            {
                Id = int.Parse(oRecord.Fields.Item(0).Value.ToString());
            }
            Local.FuncionesComunes.LiberarObjetoGenerico(oRecord);
            return Id;
        }

        public static int ObtenerNumeroInternoSBO(int? BaseType, int? BaseDocNum, string CardCode)
        {
            var DocEntry = 0;
            SAPbobsCOM.Recordset oRecord = (SAPbobsCOM.Recordset)SBO.ConexionDIAPI.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            string _query = String.Empty;

            if (BaseType.Equals(17))
            {
                // Orden de Venta - ORDR
                _query = @"SELECT ""DocEntry"" FROM ""ORDR"" WHERE ""DocNum"" = " + BaseDocNum + @" AND ""CardCode"" = '" + CardCode + @"'";
                oRecord.DoQuery(_query);
            }
            if (BaseType.Equals(13))
            {
                // Factura - OINV
                _query = @"SELECT ""DocEntry"" FROM ""OINV"" WHERE ""DocNum"" = " + BaseDocNum + @" AND ""CardCode"" = '" + CardCode + @"'";
                oRecord.DoQuery(_query);
            }
            // Si hay datos
            if (!oRecord.EoF)
            {
                DocEntry = int.Parse(oRecord.Fields.Item(0).Value.ToString());
            }
            Local.FuncionesComunes.LiberarObjetoGenerico(oRecord);
            return DocEntry;
        }

        public static Clases.Maestro ObtenerItem(string Codigo)
        {
            Clases.Maestro maestro = new Clases.Maestro();
            SAPbobsCOM.Recordset oRecord = (SAPbobsCOM.Recordset)SBO.ConexionDIAPI.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            string _query = String.Empty;
            _query = @"SELECT
                ""ItemCode"",
                ""ItemName"",
                ""ManBtchNum"",
                ""InvntryUom""
                FROM ""OITM"" WHERE ""ItemCode"" = '" + Codigo + @"'
                ORDER BY 1";
            oRecord.DoQuery(_query);
            if (!oRecord.EoF)
            {
                maestro.Codigo = oRecord.Fields.Item("ItemCode").Value.ToString();
                maestro.Descripcion = oRecord.Fields.Item("ItemName").Value.ToString();
                maestro.GestionLote = oRecord.Fields.Item("ManBtchNum").Value.ToString();
                maestro.UnidadMedidaInventario = oRecord.Fields.Item("InvntryUom").Value.ToString();
            }
            Local.FuncionesComunes.LiberarObjetoGenerico(oRecord);
            return maestro;
        }

        public static List<Clases.Maestro> ObtenerItems()
        {
            List<Clases.Maestro> maestros = new List<Clases.Maestro>();
            Clases.Maestro maestro = null;
            SAPbobsCOM.Recordset oRecord = (SAPbobsCOM.Recordset)SBO.ConexionDIAPI.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            string _query = String.Empty;
            _query = @"SELECT
                ""ItemCode"",
                ""ItemName"",
                ""ManBtchNum"",
                ""InvntryUom""
                FROM ""OITM"" ORDER BY 1";
            oRecord.DoQuery(_query);
            while (!oRecord.EoF)
            {
                maestro = new Clases.Maestro();
                maestro.Codigo = oRecord.Fields.Item("ItemCode").Value.ToString();
                maestro.Descripcion = oRecord.Fields.Item("ItemName").Value.ToString();
                maestro.GestionLote = oRecord.Fields.Item("ManBtchNum").Value.ToString();
                maestro.UnidadMedidaInventario = oRecord.Fields.Item("InvntryUom").Value.ToString();
                maestros.Add(maestro);
                oRecord.MoveNext();
            }
            Local.FuncionesComunes.LiberarObjetoGenerico(oRecord);
            return maestros;
        }

        public static Clases.Maestro ObtenerAlmacen(string Codigo)
        {
            Clases.Maestro maestro = new Clases.Maestro();
            SAPbobsCOM.Recordset oRecord = (SAPbobsCOM.Recordset)SBO.ConexionDIAPI.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            string _query = String.Empty;
            _query = @"SELECT
                ""WhsCode"",
                ""WhsName""
                FROM ""OWHS"" WHERE ""WhsCode"" = '" + Codigo + @"'
                ORDER BY 1";
            oRecord.DoQuery(_query);
            if (!oRecord.EoF)
            {
                maestro.Codigo = oRecord.Fields.Item("WhsCode").Value.ToString();
                maestro.Descripcion = oRecord.Fields.Item("WhsName").Value.ToString();
            }
            Local.FuncionesComunes.LiberarObjetoGenerico(oRecord);
            return maestro;
        }

        public static List<Clases.Maestro> ObtenerAlmacenes()
        {
            List<Clases.Maestro> maestros = new List<Clases.Maestro>();
            Clases.Maestro maestro = null;
            SAPbobsCOM.Recordset oRecord = (SAPbobsCOM.Recordset)SBO.ConexionDIAPI.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            string _query = String.Empty;
            _query = @"SELECT
                ""WhsCode"",
                ""WhsName""
                FROM ""OWHS"" ORDER BY 1";
            oRecord.DoQuery(_query);
            while (!oRecord.EoF)
            {
                maestro = new Clases.Maestro();
                maestro.Codigo = oRecord.Fields.Item("WhsCode").Value.ToString();
                maestro.Descripcion = oRecord.Fields.Item("WhsName").Value.ToString();
                maestros.Add(maestro);
                oRecord.MoveNext();
            }
            Local.FuncionesComunes.LiberarObjetoGenerico(oRecord);
            return maestros;
        }

        public static List<Clases.Consulta> ObtenerListaOrdenesVentaSN(string CardCode)
        {
            List<Clases.Consulta> listconsulta = new List<Clases.Consulta>();
            Clases.Consulta consulta = null;
            SAPbobsCOM.Recordset oRecord = (SAPbobsCOM.Recordset)SBO.ConexionDIAPI.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            string _query = String.Empty;
            _query = @"SELECT
                'OV' AS ""TipoDoc"",
                ""T0"".""DocEntry"",
                ""T0"".""DocNum"",
                ""T1"".""LineNum"",
                ""T1"".""ItemCode"",
                ""T2"".""ItemName"",
                ""T1"".""OpenQty"",
                ""T1"".""Quantity"",
                ""T1"".""unitMsr"" AS ""Unidad"",
                ""T1"".""WhsCode"",
                ""T3"".""Currency"",
                ""T1"".""Rate"",
                ""T1"".""VatPrcnt"",
                ""T1"".""Price"",
                /*CASE WHEN ""T3"".""Currency"" = 'US$'*/
                CASE WHEN ""T3"".""Currency"" = 'US$' OR ""T3"".""Currency"" = '##'
                THEN
                CASE WHEN SUBSTRING(""T1"".""Currency"", 1, 2) = 'US'
                THEN (""T1"".""OpenQty"" * (""T1"".""Price""))
                /*ELSE (""T1"".""OpenQty"" * (""T1"".""Price"")) / ""T4"".""Rate""*/
                ELSE (""T1"".""OpenQty"" * (""T1"".""Price"")) / ""T1"".""Rate""
                END
                ELSE
                CASE WHEN SUBSTRING(""T1"".""Currency"", 1, 2) = 'US'
                /*THEN (""T1"".""OpenQty"" * (""T1"".""Price"")) * ""T4"".""Rate""*/
                THEN (""T1"".""OpenQty"" * (""T1"".""Price"")) * ""T1"".""Rate""
                ELSE (""T1"".""OpenQty"" * (""T1"".""Price"")) 
                END
                END AS ""TotalLinea"",
                ""T0"".""U_SDG_DTE_TT"",
                ""T0"".""U_SDG_DTE_CHOFER"",
                ""T0"".""U_SDG_DTE_PATENTE"",
                ""T0"".""U_SDG_DTE_RUT_CHOFER"",
                ""T0"".""U_SDG_DTE_AUTO_PRN"",
                ""T0"".""U_SDG_DTE_PRINTER"",
                ""T0"".""U_SDG_DTE_MAIL_PDF"",
                ""T0"".""U_Email_Chofer""
                FROM ""ORDR"" ""T0""
                JOIN ""RDR1"" ""T1"" ON ""T0"".""DocEntry"" = ""T1"".""DocEntry""
                LEFT JOIN ""OITM"" ""T2"" ON ""T1"".""ItemCode"" = ""T2"".""ItemCode""
                JOIN ""OCRD"" ""T3"" ON ""T0"".""CardCode"" = ""T3"".""CardCode""
                /*JOIN ""ORTT"" T4 ON ""T4"".""RateDate"" = FORMAT(GETDATE(), 'yyyyMMdd') AND ""T4"".""Currency"" = CASE WHEN ""T3"".""Currency"" = 'US$' THEN 'USD' ELSE ""T3"".""Currency"" END*/
                WHERE ""T1"".""LineStatus"" = 'O'
                AND ""T0"".""CardCode"" = '" + CardCode + @"'
                AND YEAR(""T0"".""DocDate"") >= 2022
                ORDER BY 2, 3";
            oRecord.DoQuery(_query);
            while (!oRecord.EoF)
            {
                consulta = new Clases.Consulta();
                consulta.TipoDoc = oRecord.Fields.Item("TipoDoc").Value.ToString();
                consulta.DocEntry = oRecord.Fields.Item("DocEntry").Value.ToString();
                consulta.DocNum = oRecord.Fields.Item("DocNum").Value.ToString();
                consulta.LineNum = oRecord.Fields.Item("LineNum").Value.ToString();
                consulta.ItemCode = oRecord.Fields.Item("ItemCode").Value.ToString();
                consulta.ItemName = oRecord.Fields.Item("ItemName").Value.ToString();
                consulta.OpenQty = oRecord.Fields.Item("OpenQty").Value.ToString();
                consulta.Quantity = oRecord.Fields.Item("Quantity").Value.ToString();
                consulta.Unidad = oRecord.Fields.Item("Unidad").Value.ToString();
                consulta.WhsCode = oRecord.Fields.Item("WhsCode").Value.ToString();
                consulta.Currency = oRecord.Fields.Item("Currency").Value.ToString();
                consulta.Rate = oRecord.Fields.Item("Rate").Value.ToString();
                consulta.VatPrcnt = oRecord.Fields.Item("VatPrcnt").Value.ToString();
                consulta.Price = oRecord.Fields.Item("Price").Value.ToString();
                consulta.TotalLinea = oRecord.Fields.Item("TotalLinea").Value.ToString();
                consulta.U_SDG_DTE_TT = oRecord.Fields.Item("U_SDG_DTE_TT").Value.ToString();
                consulta.U_SDG_DTE_CHOFER = oRecord.Fields.Item("U_SDG_DTE_CHOFER").Value.ToString();
                consulta.U_SDG_DTE_PATENTE = oRecord.Fields.Item("U_SDG_DTE_PATENTE").Value.ToString();
                consulta.U_SDG_DTE_RUT_CHOFER = oRecord.Fields.Item("U_SDG_DTE_RUT_CHOFER").Value.ToString();
                consulta.U_SDG_DTE_FACRESERVA = "";
                consulta.U_SDG_DTE_AUTO_PRN = oRecord.Fields.Item("U_SDG_DTE_AUTO_PRN").Value.ToString();
                consulta.U_SDG_DTE_PRINTER = oRecord.Fields.Item("U_SDG_DTE_PRINTER").Value.ToString();
                consulta.U_SDG_DTE_MAIL_PDF = oRecord.Fields.Item("U_SDG_DTE_MAIL_PDF").Value.ToString();
                consulta.U_Email_Chofer = oRecord.Fields.Item("U_Email_Chofer").Value.ToString();
                listconsulta.Add(consulta);
                oRecord.MoveNext();
            }
            Local.FuncionesComunes.LiberarObjetoGenerico(oRecord);
            return listconsulta;
        }

        public static List<Clases.Consulta> ObtenerListaFacturasSN(string CardCode)
        {
            List<Clases.Consulta> listconsulta = new List<Clases.Consulta>();
            Clases.Consulta consulta = null;
            SAPbobsCOM.Recordset oRecord = (SAPbobsCOM.Recordset)SBO.ConexionDIAPI.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            string _query = String.Empty;
            _query = @"SELECT
                'FC' AS ""TipoDoc"",
                ""T0"".""DocEntry"",
                ""T0"".""DocNum"",
                ""T1"".""LineNum"",
                ""T1"".""ItemCode"",
                ""T2"".""ItemName"",
                ""T1"".""OpenQty"",
                ""T1"".""Quantity"",
                ""T1"".""unitMsr"" AS ""Unidad"",
                ""T1"".""WhsCode"",
                ""T3"".""Currency"",
                ""T1"".""Rate"",
                ""T1"".""VatPrcnt"",
                ""T1"".""Price"",
                CASE WHEN ""T3"".""Currency"" = 'US$'
                THEN ""T1"".""LineTotal"" + ""T1"".""VatSum""
                ELSE ""T1"".""TotalFrgn"" + ""T1"".""VatSumFrgn""
                END AS ""TotalLinea"",
                ""T0"".""U_SDG_DTE_TT"",
                ""T0"".""U_SDG_DTE_CHOFER"",
                ""T0"".""U_SDG_DTE_PATENTE"",
                ""T0"".""U_SDG_DTE_RUT_CHOFER"",
                ""T0"".""U_SDG_DTE_AUTO_PRN"",
                ""T0"".""U_SDG_DTE_PRINTER"",
                ""T0"".""U_SDG_DTE_MAIL_PDF"",
                ""T0"".""U_Email_Chofer""
                FROM ""OINV"" ""T0""
                JOIN ""INV1"" ""T1"" ON ""T0"".""DocEntry"" = ""T1"".""DocEntry""
                LEFT JOIN ""OITM"" ""T2"" ON ""T1"".""ItemCode"" = ""T2"".""ItemCode""
                JOIN ""OCRD"" ""T3"" ON ""T0"".""CardCode"" = ""T3"".""CardCode""
                WHERE ""T0"".""DocSubType"" = '--'
                AND ""T1"".""LineStatus"" = 'O'
                AND ""T0"".""CardCode"" = '" + CardCode + @"'
                AND ""T0"".""isIns"" = 'Y'
                ORDER BY 2, 3";
            oRecord.DoQuery(_query);
            while (!oRecord.EoF)
            {
                consulta = new Clases.Consulta();
                consulta.TipoDoc = oRecord.Fields.Item("TipoDoc").Value.ToString();
                consulta.DocEntry = oRecord.Fields.Item("DocEntry").Value.ToString();
                consulta.DocNum = oRecord.Fields.Item("DocNum").Value.ToString();
                consulta.LineNum = oRecord.Fields.Item("LineNum").Value.ToString();
                consulta.ItemCode = oRecord.Fields.Item("ItemCode").Value.ToString();
                consulta.ItemName = oRecord.Fields.Item("ItemName").Value.ToString();
                consulta.OpenQty = oRecord.Fields.Item("OpenQty").Value.ToString();
                consulta.Quantity = oRecord.Fields.Item("Quantity").Value.ToString();
                consulta.Unidad = oRecord.Fields.Item("Unidad").Value.ToString();
                consulta.WhsCode = oRecord.Fields.Item("WhsCode").Value.ToString();
                consulta.Currency = oRecord.Fields.Item("Currency").Value.ToString();
                consulta.Rate = oRecord.Fields.Item("Rate").Value.ToString();
                consulta.VatPrcnt = oRecord.Fields.Item("VatPrcnt").Value.ToString();
                consulta.Price = oRecord.Fields.Item("Price").Value.ToString();
                consulta.TotalLinea = oRecord.Fields.Item("TotalLinea").Value.ToString();
                consulta.U_SDG_DTE_TT = oRecord.Fields.Item("U_SDG_DTE_TT").Value.ToString();
                consulta.U_SDG_DTE_CHOFER = oRecord.Fields.Item("U_SDG_DTE_CHOFER").Value.ToString();
                consulta.U_SDG_DTE_PATENTE = oRecord.Fields.Item("U_SDG_DTE_PATENTE").Value.ToString();
                consulta.U_SDG_DTE_RUT_CHOFER = oRecord.Fields.Item("U_SDG_DTE_RUT_CHOFER").Value.ToString();
                consulta.U_SDG_DTE_FACRESERVA = "";
                consulta.U_SDG_DTE_AUTO_PRN = oRecord.Fields.Item("U_SDG_DTE_AUTO_PRN").Value.ToString();
                consulta.U_SDG_DTE_PRINTER = oRecord.Fields.Item("U_SDG_DTE_PRINTER").Value.ToString();
                consulta.U_SDG_DTE_MAIL_PDF = oRecord.Fields.Item("U_SDG_DTE_MAIL_PDF").Value.ToString();
                consulta.U_Email_Chofer = oRecord.Fields.Item("U_Email_Chofer").Value.ToString();
                listconsulta.Add(consulta);
                oRecord.MoveNext();
            }
            Local.FuncionesComunes.LiberarObjetoGenerico(oRecord);
            return listconsulta;
        }

        public static List<Clases.SocioNegocio> ObtenerListaSociosNegocio(string ClienteAduana)
        {
            List<Clases.SocioNegocio> listSN = new List<Clases.SocioNegocio>();
            Clases.SocioNegocio SN = null;
            SAPbobsCOM.Recordset oRecord = (SAPbobsCOM.Recordset)SBO.ConexionDIAPI.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            string _query = String.Empty;
            _query = @"SELECT
                ""T0"".""CardCode"",
                ""T0"".""CardName"",
                ""T0"".""validFor"",
                ""T0"".""frozenFor"",
                ""T0"".""U_ADUANA""
                FROM ""OCRD"" ""T0""
                WHERE ""T0"".""CardType"" = 'C'";
            if (!string.IsNullOrEmpty(ClienteAduana))
            {
                _query += @" AND ""T0"".""U_ADUANA"" = '" + ClienteAduana + "'";
            }
            oRecord.DoQuery(_query);
            while (!oRecord.EoF)
            {
                SN = new Clases.SocioNegocio();
                SN.CardCode = oRecord.Fields.Item("CardCode").Value.ToString();
                SN.CardName = oRecord.Fields.Item("CardName").Value.ToString();
                SN.validFor = oRecord.Fields.Item("validFor").Value.ToString();
                SN.frozenFor = oRecord.Fields.Item("frozenFor").Value.ToString();
                SN.ClienteAduana = oRecord.Fields.Item("U_ADUANA").Value.ToString();
                listSN.Add(SN);
                oRecord.MoveNext();
            }
            Local.FuncionesComunes.LiberarObjetoGenerico(oRecord);
            return listSN;
        }

        public static Clases.Message ObtenerLCSocioNegocio(string CardCode, double MontoTrx)
        {
            Clases.Message message = new Clases.Message();
            SAPbobsCOM.Recordset oRecord = (SAPbobsCOM.Recordset)SBO.ConexionDIAPI.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            string _query = String.Empty;
            _query = @"
                SELECT
                CASE WHEN (""Moneda"" = '##' OR (""Credito"" - ""Deuda"") < 0) THEN 'TRUE'
                ELSE 'FALSE'
                END AS ""Resp""
                FROM
                (
                SELECT
                ""Currency"" AS ""Moneda"",
                CASE WHEN ""Currency"" = 'US$'
                THEN ISNULL(""Balance"", 0) + ISNULL(""DNotesBal"", 0) - ISNULL(""ChecksBal"", 0) + " + MontoTrx + @"
                ELSE ISNULL(""BalanceFC"", 0) + ISNULL(""DNoteBalFC"", 0) - ISNULL(""ChecksBal"", 0) + " + MontoTrx + @"
                END AS ""Deuda"",
                ""CreditLine"" AS ""Credito""
                FROM ""OCRD"" WHERE ""CardCode"" = '" + CardCode + @"'
                ) TOCRD";
            oRecord.DoQuery(_query);
            if (!oRecord.EoF)
            {
                message.Mensaje = oRecord.Fields.Item("Resp").Value.ToString();
            }
            Local.FuncionesComunes.LiberarObjetoGenerico(oRecord);
            return message;
        }

        public static Clases.Message ObtenerEntradaDraftStatus(string DocEntry)
        {
            Clases.Message message = new Clases.Message();
            SAPbobsCOM.Recordset oRecord = (SAPbobsCOM.Recordset)SBO.ConexionDIAPI.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            string _query = String.Empty;
            _query = @"
                SELECT
                CASE ""WddStatus""
                WHEN '-' THEN 'Without'
                WHEN 'A' THEN 'Generated by Authorizer'
                WHEN 'C' THEN 'Canceled'
                WHEN 'N' THEN 'Rejected'
                WHEN 'P' THEN 'Generated'
                WHEN 'W' THEN 'Pending'
                WHEN 'Y' THEN 'Approved'
                ELSE '' END AS ""Resp""
                FROM ""ODRF"" WHERE ""ObjType"" = 15 AND ""DocEntry"" = " + DocEntry + "";
            oRecord.DoQuery(_query);
            if (!oRecord.EoF)
            {
                message.Mensaje = oRecord.Fields.Item("Resp").Value.ToString();
            }
            Local.FuncionesComunes.LiberarObjetoGenerico(oRecord);
            return message;
        }
    }
}