using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAPbobsCOM;

namespace Soindus.Interfaces.APIRestSBO.SBO
{
    public class IntegracionSBO
    {
        public IntegracionSBO()
        {
            if (SBO.ConexionDIAPI.oCompany == null || !SBO.ConexionDIAPI.oCompany.Connected)
            {
                new SBO.ConexionDIAPI();
            }
        }

        public Clases.Documento ObtenerEntrega(string DocEntry)
        {
            Clases.Documento documento = new Clases.Documento();
            try
            {
                //SBO.ConexionDIAPI.oCompany.XmlExportType = BoXmlExportTypes.xet_ExportImportMode;
                var oDoc = (SAPbobsCOM.Documents)SBO.ConexionDIAPI.oCompany.GetBusinessObject(BoObjectTypes.oDeliveryNotes);
                //SBO.ConexionDIAPI.oCompany.XMLAsString = true;
                string key = @"<DocumentParams><DocEntry>" + DocEntry + @"</DocEntry></DocumentParams>";
                oDoc.Browser.GetByKeys(key);
                //var lol = oDoc.GetAsXML();
                documento.DocEntry = oDoc.DocEntry;
                documento.DocNum = oDoc.DocNum;
                documento.DocDate = oDoc.DocDate;
                documento.DocDueDate = oDoc.DocDueDate;
                documento.TaxDate = oDoc.TaxDate;
                documento.CardCode = oDoc.CardCode;
                documento.Comments = oDoc.Comments;
                documento.Indicator = oDoc.Indicator;
                documento.U_SDG_DTE_TT = oDoc.UserFields.Fields.Item("U_SDG_DTE_TT").Value.ToString();
                documento.U_SDG_DTE_CHOFER = oDoc.UserFields.Fields.Item("U_SDG_DTE_CHOFER").Value.ToString();
                documento.U_SDG_DTE_PATENTE = oDoc.UserFields.Fields.Item("U_SDG_DTE_PATENTE").Value.ToString();
                documento.U_SDG_DTE_RUTCHOFER = oDoc.UserFields.Fields.Item("U_SDG_DTE_RUTCHOFER").Value.ToString();
                //documento.U_SDG_DTE_FACRESERVA = oDoc.UserFields.Fields.Item("U_SDG_DTE_FACRESERVA").Value.ToString();
                documento.U_SDG_DTE_AUTO_PRN = oDoc.UserFields.Fields.Item("U_SDG_DTE_AUTO_PRN").Value.ToString();
                documento.U_SDG_DTE_PRINTER = oDoc.UserFields.Fields.Item("U_SDG_DTE_PRINTER").Value.ToString();
                documento.U_SDG_DTE_MAIL_PDF = oDoc.UserFields.Fields.Item("U_SDG_DTE_MAIL_PDF").Value.ToString();
                documento.U_Email_Chofer = oDoc.UserFields.Fields.Item("U_Email_Chofer").Value.ToString();
                Clases.Detalle[] det = new Clases.Detalle[oDoc.Lines.Count];
                for (int i = 0; i < oDoc.Lines.Count; i++)
                {
                    oDoc.Lines.SetCurrentLine(i);
                    Clases.Detalle detalle = new Clases.Detalle();
                    detalle.DocEntry = oDoc.Lines.DocEntry;
                    detalle.LineNum = oDoc.Lines.LineNum;
                    detalle.BaseType = oDoc.Lines.BaseType;
                    detalle.BaseEntry = oDoc.Lines.BaseEntry;
                    detalle.BaseLine = oDoc.Lines.BaseLine;
                    detalle.ItemCode = oDoc.Lines.ItemCode;
                    detalle.Quantity = oDoc.Lines.Quantity;
                    detalle.WhsCode = oDoc.Lines.WarehouseCode;
                    det[i] = detalle;
                }
                documento.Detalle = det;
            }
            catch (Exception)
            {
                documento = new Clases.Documento();
            }
            return documento;
        }

        public Clases.Respuesta IntegrarEntrega(Clases.Documento documento)
        {
            Clases.Respuesta resp = new Clases.Respuesta();
            try
            {
                var oDoc = (SAPbobsCOM.Documents)SBO.ConexionDIAPI.oCompany.GetBusinessObject(BoObjectTypes.oDeliveryNotes);
                oDoc.DocDate = documento.DocDate != null ? documento.DocDate : DateTime.Now;
                oDoc.DocDueDate = documento.DocDueDate != null ? documento.DocDueDate : DateTime.Now;
                oDoc.TaxDate = documento.TaxDate != null ? documento.TaxDate : DateTime.Now;
                oDoc.DocType = BoDocumentTypes.dDocument_Items;
                oDoc.CardCode = documento.CardCode;
                oDoc.Comments = documento.Comments;
                oDoc.Indicator = documento.Indicator;
                //oDoc.UserFields.Fields.Item("U_SDG_DTE_TT").Value = documento.U_SDG_DTE_TT;
                oDoc.UserFields.Fields.Item("U_SDG_DTE_CHOFER").Value = documento.U_SDG_DTE_CHOFER;
                oDoc.UserFields.Fields.Item("U_SDG_DTE_PATENTE").Value = documento.U_SDG_DTE_PATENTE;
                oDoc.UserFields.Fields.Item("U_SDG_DTE_RUTCHOFER").Value = documento.U_SDG_DTE_RUTCHOFER;
                //oDoc.UserFields.Fields.Item("U_SDG_DTE_FACRESERVA").Value = documento.U_SDG_DTE_FACRESERVA;
                oDoc.UserFields.Fields.Item("U_SDG_DTE_AUTO_PRN").Value = documento.U_SDG_DTE_AUTO_PRN;
                oDoc.UserFields.Fields.Item("U_SDG_DTE_PRINTER").Value = documento.U_SDG_DTE_PRINTER;
                oDoc.UserFields.Fields.Item("U_SDG_DTE_MAIL_PDF").Value = documento.U_SDG_DTE_MAIL_PDF;
                oDoc.UserFields.Fields.Item("U_Email_Chofer").Value = documento.U_Email_Chofer;
                int linea = -1;
                foreach (var item in documento.Detalle)
                {
                    linea = linea + 1;
                    if (linea > 0)
                    {
                        oDoc.Lines.Add();
                    }
                    oDoc.Lines.SetCurrentLine(linea);
                    oDoc.Lines.ItemCode = item.ItemCode;
                    oDoc.Lines.Quantity = item.Quantity;
                    oDoc.Lines.WarehouseCode = item.WhsCode;
                }
                int errCode = 0;
                string errMsg = string.Empty;
                int retDoc = oDoc.Add();
                if (retDoc != 0)
                {
                    SBO.ConexionDIAPI.oCompany.GetLastError(out errCode, out errMsg);
                    resp.Estado = "ERR";
                    resp.Interno = null;
                    resp.Documento = null;
                    resp.ErrCode = errCode;
                    resp.ErrMsg = errMsg;
                }
                else
                {
                    var nuevo_doc = SBO.ConexionDIAPI.oCompany.GetNewObjectKey();
                    oDoc = null;
                    oDoc = (SAPbobsCOM.Documents)SBO.ConexionDIAPI.oCompany.GetBusinessObject(BoObjectTypes.oDeliveryNotes);
                    string key = @"<DocumentParams><DocEntry>" + nuevo_doc + @"</DocEntry></DocumentParams>";
                    oDoc.Browser.GetByKeys(key);
                    resp.Estado = "OK";
                    resp.Interno = oDoc.DocEntry.ToString();
                    resp.Documento = oDoc.DocNum.ToString();
                    resp.ErrCode = null;
                    resp.ErrMsg = null;
                }
                oDoc = null;
            }
            catch (Exception ex)
            {
                resp.Estado = "ERR";
                resp.Interno = null;
                resp.Documento = null;
                resp.ErrCode = -1;
                resp.ErrMsg = ex.Message;
            }
            return resp;
        }
    }
}