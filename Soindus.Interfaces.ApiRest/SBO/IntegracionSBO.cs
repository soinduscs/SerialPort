using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAPbobsCOM;

namespace Soindus.Interfaces.ApiRest.SBO
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

        public Clases.Respuesta ObtenerOrden(string DocEntry)
        {
            Clases.Respuesta resp = new Clases.Respuesta();
            Clases.Documento documento = new Clases.Documento();
            try
            {
                //SBO.ConexionDIAPI.oCompany.XmlExportType = BoXmlExportTypes.xet_ExportImportMode;
                var oDoc = (SAPbobsCOM.Documents)SBO.ConexionDIAPI.oCompany.GetBusinessObject(BoObjectTypes.oOrders);
                //SBO.ConexionDIAPI.oCompany.XMLAsString = true;
                string key = @"<DocumentParams><DocEntry>" + DocEntry + @"</DocEntry></DocumentParams>";
                oDoc.Browser.GetByKeys(key);
                //var lol = oDoc.GetAsXML();
                if (oDoc.DocEntry == null || oDoc.DocEntry.Equals(0))
                {
                    throw new Exception("Documento no encontrado");
                }
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
                documento.U_SDG_DTE_RUT_CHOFER = oDoc.UserFields.Fields.Item("U_SDG_DTE_RUT_CHOFER").Value.ToString();
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
                    detalle.BaseDocNum = null;
                    detalle.DocEntry = oDoc.Lines.DocEntry;
                    detalle.LineNum = oDoc.Lines.LineNum;
                    detalle.BaseType = oDoc.Lines.BaseType;
                    detalle.BaseEntry = oDoc.Lines.BaseEntry;
                    detalle.BaseLine = oDoc.Lines.BaseLine;
                    detalle.ItemCode = oDoc.Lines.ItemCode;
                    detalle.Quantity = oDoc.Lines.Quantity;
                    detalle.WhsCode = oDoc.Lines.WarehouseCode;
                    detalle.LineStatus = oDoc.Lines.LineStatus.Equals(BoStatus.bost_Open) ? "Open" : "Closed";
                    if (oDoc.Lines.BatchNumbers.Count > 0)
                    {
                        Clases.Lote[] lotes = new Clases.Lote[oDoc.Lines.BatchNumbers.Count];
                        for (int k = 0; k < oDoc.Lines.BatchNumbers.Count; k++)
                        {
                            oDoc.Lines.BatchNumbers.SetCurrentLine(k);
                            Clases.Lote lote = new Clases.Lote();
                            lote.BatchNumber = oDoc.Lines.BatchNumbers.BatchNumber;
                            lote.Quantity = oDoc.Lines.BatchNumbers.Quantity;
                            lotes[k] = lote;
                        }
                        detalle.Lotes = lotes;
                    }
                    det[i] = detalle;
                }
                documento.Detalle = det;
                resp.Estado = "OK";
                resp.Interno = oDoc.DocEntry.ToString();
                resp.Documento = oDoc.DocNum.ToString();
                resp.ErrCode = null;
                resp.ErrMsg = null;
                resp.DocumentoSBO = documento;
            }
            catch (Exception ex)
            {
                resp.Estado = "ERR";
                resp.Interno = null;
                resp.Documento = null;
                resp.ErrCode = -1;
                resp.ErrMsg = ex.Message;
                resp.DocumentoSBO = null;
            }
            return resp;
        }

        public Clases.Respuesta ObtenerEntrega(string DocEntry)
        {
            Clases.Respuesta resp = new Clases.Respuesta();
            Clases.Documento documento = new Clases.Documento();
            try
            {
                //SBO.ConexionDIAPI.oCompany.XmlExportType = BoXmlExportTypes.xet_ExportImportMode;
                var oDoc = (SAPbobsCOM.Documents)SBO.ConexionDIAPI.oCompany.GetBusinessObject(BoObjectTypes.oDeliveryNotes);
                //SBO.ConexionDIAPI.oCompany.XMLAsString = true;
                string key = @"<DocumentParams><DocEntry>" + DocEntry + @"</DocEntry></DocumentParams>";
                oDoc.Browser.GetByKeys(key);
                //var lol = oDoc.GetAsXML();
                if (oDoc.DocEntry == null || oDoc.DocEntry.Equals(0))
                {
                    throw new Exception("Documento no encontrado");
                }
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
                documento.U_SDG_DTE_RUT_CHOFER = oDoc.UserFields.Fields.Item("U_SDG_DTE_RUT_CHOFER").Value.ToString();
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
                    detalle.BaseDocNum = null;
                    detalle.DocEntry = oDoc.Lines.DocEntry;
                    detalle.LineNum = oDoc.Lines.LineNum;
                    detalle.BaseType = oDoc.Lines.BaseType;
                    detalle.BaseEntry = oDoc.Lines.BaseEntry;
                    detalle.BaseLine = oDoc.Lines.BaseLine;
                    detalle.ItemCode = oDoc.Lines.ItemCode;
                    detalle.Quantity = oDoc.Lines.Quantity;
                    detalle.WhsCode = oDoc.Lines.WarehouseCode;
                    detalle.LineStatus = oDoc.Lines.LineStatus.Equals(BoStatus.bost_Open) ? "Open" : "Closed";
                    if (oDoc.Lines.BatchNumbers.Count > 0)
                    {
                        Clases.Lote[] lotes = new Clases.Lote[oDoc.Lines.BatchNumbers.Count];
                        for (int k = 0; k < oDoc.Lines.BatchNumbers.Count; k++)
                        {
                            oDoc.Lines.BatchNumbers.SetCurrentLine(k);
                            Clases.Lote lote = new Clases.Lote();
                            lote.BatchNumber = oDoc.Lines.BatchNumbers.BatchNumber;
                            lote.Quantity = oDoc.Lines.BatchNumbers.Quantity;
                            lotes[k] = lote;
                        }
                        detalle.Lotes = lotes;
                    }
                    det[i] = detalle;
                }
                documento.Detalle = det;
                resp.Estado = "OK";
                resp.Interno = oDoc.DocEntry.ToString();
                resp.Documento = oDoc.DocNum.ToString();
                resp.ErrCode = null;
                resp.ErrMsg = null;
                resp.DocumentoSBO = documento;
            }
            catch (Exception ex)
            {
                resp.Estado = "ERR";
                resp.Interno = null;
                resp.Documento = null;
                resp.ErrCode = -1;
                resp.ErrMsg = ex.Message;
                resp.DocumentoSBO = null;
            }
            return resp;
        }

        public Clases.Respuesta ObtenerEntregaDraft(string DocEntry)
        {
            Clases.Respuesta resp = new Clases.Respuesta();
            Clases.Documento documento = new Clases.Documento();
            try
            {
                //SBO.ConexionDIAPI.oCompany.XmlExportType = BoXmlExportTypes.xet_ExportImportMode;
                var oDoc = (SAPbobsCOM.Documents)SBO.ConexionDIAPI.oCompany.GetBusinessObject(BoObjectTypes.oDrafts);
                //SBO.ConexionDIAPI.oCompany.XMLAsString = true;
                string key = @"<DocumentParams><DocEntry>" + DocEntry + @"</DocEntry></DocumentParams>";
                oDoc.Browser.GetByKeys(key);
                //var lol = oDoc.GetAsXML();
                if (oDoc.DocEntry == null || oDoc.DocEntry.Equals(0))
                {
                    throw new Exception("Documento no encontrado");
                }
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
                documento.U_SDG_DTE_RUT_CHOFER = oDoc.UserFields.Fields.Item("U_SDG_DTE_RUT_CHOFER").Value.ToString();
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
                    detalle.BaseDocNum = null;
                    detalle.DocEntry = oDoc.Lines.DocEntry;
                    detalle.LineNum = oDoc.Lines.LineNum;
                    detalle.BaseType = oDoc.Lines.BaseType;
                    detalle.BaseEntry = oDoc.Lines.BaseEntry;
                    detalle.BaseLine = oDoc.Lines.BaseLine;
                    detalle.ItemCode = oDoc.Lines.ItemCode;
                    detalle.Quantity = oDoc.Lines.Quantity;
                    detalle.WhsCode = oDoc.Lines.WarehouseCode;
                    detalle.LineStatus = oDoc.Lines.LineStatus.Equals(BoStatus.bost_Open) ? "Open" : "Closed";
                    if (oDoc.Lines.BatchNumbers.Count > 0)
                    {
                        Clases.Lote[] lotes = new Clases.Lote[oDoc.Lines.BatchNumbers.Count];
                        for (int k = 0; k < oDoc.Lines.BatchNumbers.Count; k++)
                        {
                            oDoc.Lines.BatchNumbers.SetCurrentLine(k);
                            Clases.Lote lote = new Clases.Lote();
                            lote.BatchNumber = oDoc.Lines.BatchNumbers.BatchNumber;
                            lote.Quantity = oDoc.Lines.BatchNumbers.Quantity;
                            lotes[k] = lote;
                        }
                        detalle.Lotes = lotes;
                    }
                    det[i] = detalle;
                }
                documento.Detalle = det;
                resp.Estado = "OK";
                resp.Interno = oDoc.DocEntry.ToString();
                resp.Documento = oDoc.DocNum.ToString();
                resp.ErrCode = null;
                resp.ErrMsg = null;
                resp.DocumentoSBO = documento;
            }
            catch (Exception ex)
            {
                resp.Estado = "ERR";
                resp.Interno = null;
                resp.Documento = null;
                resp.ErrCode = -1;
                resp.ErrMsg = ex.Message;
                resp.DocumentoSBO = null;
            }
            return resp;
        }

        public Clases.Respuesta ObtenerFactura(string DocEntry)
        {
            Clases.Respuesta resp = new Clases.Respuesta();
            Clases.Documento documento = new Clases.Documento();
            try
            {
                //SBO.ConexionDIAPI.oCompany.XmlExportType = BoXmlExportTypes.xet_ExportImportMode;
                var oDoc = (SAPbobsCOM.Documents)SBO.ConexionDIAPI.oCompany.GetBusinessObject(BoObjectTypes.oInvoices);
                //SBO.ConexionDIAPI.oCompany.XMLAsString = true;
                string key = @"<DocumentParams><DocEntry>" + DocEntry + @"</DocEntry></DocumentParams>";
                oDoc.Browser.GetByKeys(key);
                //var lol = oDoc.GetAsXML();
                if(oDoc.DocEntry == null || oDoc.DocEntry.Equals(0))
                {
                    throw new Exception("Documento no encontrado");
                }
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
                documento.U_SDG_DTE_RUT_CHOFER = oDoc.UserFields.Fields.Item("U_SDG_DTE_RUT_CHOFER").Value.ToString();
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
                    detalle.BaseDocNum = null;
                    detalle.DocEntry = oDoc.Lines.DocEntry;
                    detalle.LineNum = oDoc.Lines.LineNum;
                    detalle.BaseType = oDoc.Lines.BaseType;
                    detalle.BaseEntry = oDoc.Lines.BaseEntry;
                    detalle.BaseLine = oDoc.Lines.BaseLine;
                    detalle.ItemCode = oDoc.Lines.ItemCode;
                    detalle.Quantity = oDoc.Lines.Quantity;
                    detalle.WhsCode = oDoc.Lines.WarehouseCode;
                    detalle.LineStatus = oDoc.Lines.LineStatus.Equals(BoStatus.bost_Open) ? "Open" : "Closed";
                    if (oDoc.Lines.BatchNumbers.Count > 0)
                    {
                        Clases.Lote[] lotes = new Clases.Lote[oDoc.Lines.BatchNumbers.Count];
                        for (int k = 0; k < oDoc.Lines.BatchNumbers.Count; k++)
                        {
                            oDoc.Lines.BatchNumbers.SetCurrentLine(k);
                            Clases.Lote lote = new Clases.Lote();
                            lote.BatchNumber = oDoc.Lines.BatchNumbers.BatchNumber;
                            lote.Quantity = oDoc.Lines.BatchNumbers.Quantity;
                            lotes[k] = lote;
                        }
                        detalle.Lotes = lotes;
                    }
                    det[i] = detalle;
                }
                documento.Detalle = det;
                resp.Estado = "OK";
                resp.Interno = oDoc.DocEntry.ToString();
                resp.Documento = oDoc.DocNum.ToString();
                resp.ErrCode = null;
                resp.ErrMsg = null;
                resp.DocumentoSBO = documento;
            }
            catch (Exception ex)
            {
                resp.Estado = "ERR";
                resp.Interno = null;
                resp.Documento = null;
                resp.ErrCode = -1;
                resp.ErrMsg = ex.Message;
                resp.DocumentoSBO = null;
            }
            return resp;
        }

        public Clases.Respuesta ObtenerTraslado(string DocEntry)
        {
            Clases.Respuesta resp = new Clases.Respuesta();
            Clases.Traslado documento = new Clases.Traslado();
            try
            {
                //SBO.ConexionDIAPI.oCompany.XmlExportType = BoXmlExportTypes.xet_ExportImportMode;
                var oDoc = (SAPbobsCOM.StockTransfer)SBO.ConexionDIAPI.oCompany.GetBusinessObject(BoObjectTypes.oStockTransfer);
                //SBO.ConexionDIAPI.oCompany.XMLAsString = true;
                string key = @"<StockTransferParams><DocEntry>" + DocEntry + @"</DocEntry></StockTransferParams>";
                oDoc.Browser.GetByKeys(key);
                //var lol = oDoc.GetAsXML();
                if (oDoc.DocEntry == null || oDoc.DocEntry.Equals(0))
                {
                    throw new Exception("Documento no encontrado");
                }
                documento.DocEntry = oDoc.DocEntry;
                documento.DocNum = oDoc.DocNum;
                documento.DocDate = oDoc.DocDate;
                documento.TaxDate = oDoc.TaxDate;
                documento.CardCode = oDoc.CardCode;
                documento.Comments = oDoc.Comments;
                documento.U_SDG_DTE_TT = oDoc.UserFields.Fields.Item("U_SDG_DTE_TT").Value.ToString();
                documento.U_SDG_DTE_CHOFER = oDoc.UserFields.Fields.Item("U_SDG_DTE_CHOFER").Value.ToString();
                documento.U_SDG_DTE_PATENTE = oDoc.UserFields.Fields.Item("U_SDG_DTE_PATENTE").Value.ToString();
                documento.U_SDG_DTE_RUT_CHOFER = oDoc.UserFields.Fields.Item("U_SDG_DTE_RUT_CHOFER").Value.ToString();
                //documento.U_SDG_DTE_FACRESERVA = oDoc.UserFields.Fields.Item("U_SDG_DTE_FACRESERVA").Value.ToString();
                documento.U_SDG_DTE_AUTO_PRN = oDoc.UserFields.Fields.Item("U_SDG_DTE_AUTO_PRN").Value.ToString();
                documento.U_SDG_DTE_PRINTER = oDoc.UserFields.Fields.Item("U_SDG_DTE_PRINTER").Value.ToString();
                documento.U_SDG_DTE_MAIL_PDF = oDoc.UserFields.Fields.Item("U_SDG_DTE_MAIL_PDF").Value.ToString();
                documento.U_Email_Chofer = oDoc.UserFields.Fields.Item("U_Email_Chofer").Value.ToString();
                documento.U_CLIFINAL = oDoc.UserFields.Fields.Item("U_CLIFINAL").Value.ToString();
                Clases.DetalleTraslado[] det = new Clases.DetalleTraslado[oDoc.Lines.Count];
                for (int i = 0; i < oDoc.Lines.Count; i++)
                {
                    oDoc.Lines.SetCurrentLine(i);
                    Clases.DetalleTraslado detalle = new Clases.DetalleTraslado();
                    detalle.BaseDocNum = null;
                    detalle.DocEntry = oDoc.Lines.DocEntry;
                    detalle.LineNum = oDoc.Lines.LineNum;
                    detalle.ItemCode = oDoc.Lines.ItemCode;
                    detalle.Quantity = oDoc.Lines.Quantity;
                    detalle.FromWhsCod = oDoc.Lines.FromWarehouseCode;
                    detalle.WhsCode = oDoc.Lines.WarehouseCode;
                    //detalle.LineStatus = oDoc.Lines.LineStatus.Equals(BoStatus.bost_Open) ? "Open" : "Closed";
                    if (oDoc.Lines.BatchNumbers.Count > 0)
                    {
                        Clases.Lote[] lotes = new Clases.Lote[oDoc.Lines.BatchNumbers.Count];
                        for (int k = 0; k < oDoc.Lines.BatchNumbers.Count; k++)
                        {
                            oDoc.Lines.BatchNumbers.SetCurrentLine(k);
                            Clases.Lote lote = new Clases.Lote();
                            lote.BatchNumber = oDoc.Lines.BatchNumbers.BatchNumber;
                            lote.Quantity = oDoc.Lines.BatchNumbers.Quantity;
                            lotes[k] = lote;
                        }
                        detalle.Lotes = lotes;
                    }
                    det[i] = detalle;
                }
                documento.Detalle = det;
                resp.Estado = "OK";
                resp.Interno = oDoc.DocEntry.ToString();
                resp.Documento = oDoc.DocNum.ToString();
                resp.ErrCode = null;
                resp.ErrMsg = null;
                resp.TrasladoSBO = documento;
            }
            catch (Exception ex)
            {
                resp.Estado = "ERR";
                resp.Interno = null;
                resp.Documento = null;
                resp.ErrCode = -1;
                resp.ErrMsg = ex.Message;
                resp.TrasladoSBO = null;
            }
            return resp;
        }

        public Clases.Maestro ObtenerItem(string ItemCode)
        {
            return SBO.ConsultasSBO.ObtenerItem(ItemCode);
        }

        public List<Clases.Maestro> ObtenerItems()
        {
            return SBO.ConsultasSBO.ObtenerItems();
        }

        public Clases.Maestro ObtenerAlmacen(string WhsCode)
        {
            return SBO.ConsultasSBO.ObtenerAlmacen(WhsCode);
        }

        public List<Clases.Maestro> ObtenerAlmacenes()
        {
            return SBO.ConsultasSBO.ObtenerAlmacenes();
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
                oDoc.UserFields.Fields.Item("U_SDG_DTE_RUT_CHOFER").Value = documento.U_SDG_DTE_RUT_CHOFER;
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
                    if (item.BaseEntry != null && !item.BaseEntry.Equals(0))
                    {
                        var BaseType = item.BaseType != null ? (int)item.BaseType : -1;
                        var BaseEntry = (int)item.BaseEntry;
                        var BaseLine = item.BaseLine != null ? (int)item.BaseLine : linea;
                        oDoc.Lines.BaseType = BaseType;
                        oDoc.Lines.BaseEntry = BaseEntry;
                        oDoc.Lines.BaseLine = BaseLine;
                    }
                    else
                    {
                        if (item.BaseDocNum != null && !item.BaseDocNum.Equals(0))
                        {
                            var BaseType = item.BaseType != null ? (int)item.BaseType : -1;
                            var BaseEntry = SBO.ConsultasSBO.ObtenerNumeroInternoSBO(BaseType, item.BaseDocNum, documento.CardCode);
                            var BaseLine = item.BaseLine != null ? (int)item.BaseLine : linea;
                            oDoc.Lines.BaseType = BaseType;
                            oDoc.Lines.BaseEntry = BaseEntry;
                            oDoc.Lines.BaseLine = BaseLine;
                        }
                    }
                    oDoc.Lines.ItemCode = item.ItemCode;
                    oDoc.Lines.Quantity = item.Quantity;
                    oDoc.Lines.WarehouseCode = item.WhsCode;
                    if (item.Lotes != null)
                    {
                        int linealotes = -1;
                        foreach (var lote in item.Lotes)
                        {
                            linealotes = linealotes + 1;
                            if (linealotes > 0)
                            {
                                oDoc.Lines.BatchNumbers.Add();
                            }
                            oDoc.Lines.BatchNumbers.BatchNumber = lote.BatchNumber;
                            oDoc.Lines.BatchNumbers.Quantity = lote.Quantity;
                        }
                    }
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

        public Clases.Respuesta IntegrarEntregaDraft(Clases.Documento documento)
        {
            Clases.Respuesta resp = new Clases.Respuesta();
            try
            {
                var oDoc = (SAPbobsCOM.Documents)SBO.ConexionDIAPI.oCompany.GetBusinessObject(BoObjectTypes.oDrafts);
                oDoc.DocObjectCode = SAPbobsCOM.BoObjectTypes.oDeliveryNotes;
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
                oDoc.UserFields.Fields.Item("U_SDG_DTE_RUT_CHOFER").Value = documento.U_SDG_DTE_RUT_CHOFER;
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
                    if (item.BaseEntry != null && !item.BaseEntry.Equals(0))
                    {
                        var BaseType = item.BaseType != null ? (int)item.BaseType : -1;
                        var BaseEntry = (int)item.BaseEntry;
                        var BaseLine = item.BaseLine != null ? (int)item.BaseLine : linea;
                        oDoc.Lines.BaseType = BaseType;
                        oDoc.Lines.BaseEntry = BaseEntry;
                        oDoc.Lines.BaseLine = BaseLine;
                    }
                    else
                    {
                        if (item.BaseDocNum != null && !item.BaseDocNum.Equals(0))
                        {
                            var BaseType = item.BaseType != null ? (int)item.BaseType : -1;
                            var BaseEntry = SBO.ConsultasSBO.ObtenerNumeroInternoSBO(BaseType, item.BaseDocNum, documento.CardCode);
                            var BaseLine = item.BaseLine != null ? (int)item.BaseLine : linea;
                            oDoc.Lines.BaseType = BaseType;
                            oDoc.Lines.BaseEntry = BaseEntry;
                            oDoc.Lines.BaseLine = BaseLine;
                        }
                    }
                    oDoc.Lines.ItemCode = item.ItemCode;
                    oDoc.Lines.Quantity = item.Quantity;
                    oDoc.Lines.WarehouseCode = item.WhsCode;
                    if (item.Lotes != null)
                    {
                        int linealotes = -1;
                        foreach (var lote in item.Lotes)
                        {
                            linealotes = linealotes + 1;
                            if (linealotes > 0)
                            {
                                oDoc.Lines.BatchNumbers.Add();
                            }
                            oDoc.Lines.BatchNumbers.BatchNumber = lote.BatchNumber;
                            oDoc.Lines.BatchNumbers.Quantity = lote.Quantity;
                        }
                    }
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
                    oDoc = (SAPbobsCOM.Documents)SBO.ConexionDIAPI.oCompany.GetBusinessObject(BoObjectTypes.oDrafts);
                    string key = @"<DocumentParams><DocEntry>" + nuevo_doc + @"</DocEntry></DocumentParams>";
                    oDoc.Browser.GetByKeys(key);
                    resp.Estado = "OK";
                    resp.Interno = oDoc.DocEntry.ToString();
                    resp.Documento = oDoc.DocNum.ToString();
                    resp.ErrCode = null;
                    resp.ErrMsg = null;
                    EnviarAlerta(oDoc.DocEntry.ToString(), oDoc.DocNum.ToString(), oDoc.CardCode.ToString());
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

        public Clases.Respuesta IntegrarTraslado(Clases.Traslado documento)
        {
            Clases.Respuesta resp = new Clases.Respuesta();
            try
            {
                var oDoc = (SAPbobsCOM.StockTransfer)SBO.ConexionDIAPI.oCompany.GetBusinessObject(BoObjectTypes.oStockTransfer);
                oDoc.DocDate = documento.DocDate != null ? documento.DocDate : DateTime.Now;
                oDoc.TaxDate = documento.TaxDate != null ? documento.TaxDate : DateTime.Now;
                //oDoc.DocType = BoDocumentTypes.dDocument_Items;
                oDoc.CardCode = documento.CardCode;
                oDoc.FromWarehouse = documento.Filler;
                oDoc.ToWarehouse = documento.ToWhsCode;
                oDoc.Comments = documento.Comments;
                oDoc.UserFields.Fields.Item("U_SDG_DTE_TT").Value = documento.U_SDG_DTE_TT;
                oDoc.UserFields.Fields.Item("U_SDG_DTE_CHOFER").Value = documento.U_SDG_DTE_CHOFER;
                oDoc.UserFields.Fields.Item("U_SDG_DTE_PATENTE").Value = documento.U_SDG_DTE_PATENTE;
                oDoc.UserFields.Fields.Item("U_SDG_DTE_RUT_CHOFER").Value = documento.U_SDG_DTE_RUT_CHOFER;
                //oDoc.UserFields.Fields.Item("U_SDG_DTE_FACRESERVA").Value = documento.U_SDG_DTE_FACRESERVA;
                oDoc.UserFields.Fields.Item("U_SDG_DTE_AUTO_PRN").Value = documento.U_SDG_DTE_AUTO_PRN;
                oDoc.UserFields.Fields.Item("U_SDG_DTE_PRINTER").Value = documento.U_SDG_DTE_PRINTER;
                oDoc.UserFields.Fields.Item("U_SDG_DTE_MAIL_PDF").Value = documento.U_SDG_DTE_MAIL_PDF;
                oDoc.UserFields.Fields.Item("U_Email_Chofer").Value = documento.U_Email_Chofer;
                if (!string.IsNullOrEmpty(documento.U_CLIFINAL))
                {
                    oDoc.UserFields.Fields.Item("U_CLIFINAL").Value = documento.U_CLIFINAL;
                }
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
                    oDoc.Lines.FromWarehouseCode = item.FromWhsCod;
                    oDoc.Lines.WarehouseCode = item.WhsCode;
                    if (item.Lotes != null)
                    {
                        int linealotes = -1;
                        foreach (var lote in item.Lotes)
                        {
                            linealotes = linealotes + 1;
                            if (linealotes > 0)
                            {
                                oDoc.Lines.BatchNumbers.Add();
                            }
                            oDoc.Lines.BatchNumbers.BatchNumber = lote.BatchNumber;
                            oDoc.Lines.BatchNumbers.Quantity = lote.Quantity;
                        }
                    }
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
                    oDoc = (SAPbobsCOM.StockTransfer)SBO.ConexionDIAPI.oCompany.GetBusinessObject(BoObjectTypes.oStockTransfer);
                    string key = @"<StockTransferParams><DocEntry>" + nuevo_doc + @"</DocEntry></StockTransferParams>";
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

        public List<Clases.Consulta> ObtenerListaOrdenesVenta(string CardCode)
        {
            return SBO.ConsultasSBO.ObtenerListaOrdenesVentaSN(CardCode);
        }

        public List<Clases.Consulta> ObtenerListaFacturas(string CardCode)
        {
            return SBO.ConsultasSBO.ObtenerListaFacturasSN(CardCode);
        }

        public List<Clases.SocioNegocio> ObtenerListaSociosNegocio(string ClienteAduana)
        {
            return SBO.ConsultasSBO.ObtenerListaSociosNegocio(ClienteAduana);
        }

        public Clases.Message ObtenerLCSocioNegocio(string CardCode, double MontoTrx)
        {
            return SBO.ConsultasSBO.ObtenerLCSocioNegocio(CardCode, MontoTrx);
        }

        public Clases.Message ObtenerEntregaDraftStatus(string DocEntry)
        {
            return SBO.ConsultasSBO.ObtenerEntradaDraftStatus(DocEntry);
        }

        public Clases.Respuesta IntegrarEntregaDraftFinal(string DocEntry)
        {
            Clases.Respuesta resp = new Clases.Respuesta();
            try
            {
                var oDoc = (SAPbobsCOM.Documents)SBO.ConexionDIAPI.oCompany.GetBusinessObject(BoObjectTypes.oDrafts);
                string key = @"<DocumentParams><DocEntry>" + DocEntry + @"</DocEntry></DocumentParams>";
                oDoc.Browser.GetByKeys(key);
                if (oDoc.DocEntry == null || oDoc.DocEntry.Equals(0))
                {
                    throw new Exception("Documento no encontrado");
                }

                int errCode = 0;
                string errMsg = string.Empty;
                int retDoc = oDoc.SaveDraftToDocument();
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
                    key = @"<DocumentParams><DocEntry>" + nuevo_doc + @"</DocEntry></DocumentParams>";
                    oDoc.Browser.GetByKeys(key);
                    resp.Estado = "OK";
                    resp.Interno = oDoc.DocEntry.ToString();
                    resp.Documento = oDoc.DocNum.ToString();
                    resp.ErrCode = null;
                    resp.ErrMsg = null;

                    oDoc = null;
                    oDoc = (SAPbobsCOM.Documents)SBO.ConexionDIAPI.oCompany.GetBusinessObject(BoObjectTypes.oDrafts);
                    key = @"<DocumentParams><DocEntry>" + DocEntry + @"</DocEntry></DocumentParams>";
                    oDoc.Browser.GetByKeys(key);
                    retDoc = oDoc.Remove();
                    if (retDoc != 0)
                    {
                        SBO.ConexionDIAPI.oCompany.GetLastError(out errCode, out errMsg);
                    }
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

        private static void EnviarAlerta(string DocEntry, string DocNum, string CardCode)
        {
            SAPbobsCOM.CompanyService oCmpSrv;
            SAPbobsCOM.MessagesService oMessageService;
            SAPbobsCOM.Message oMessage = null;
            SAPbobsCOM.Messages oMessages = null;
            SAPbobsCOM.MessageDataColumns pMessageDataColumns = null;
            SAPbobsCOM.MessageDataColumn pMessageDataColumn = null;
            SAPbobsCOM.MessageDataLines oLines = null;
            SAPbobsCOM.MessageDataLine oLine = null;
            SAPbobsCOM.RecipientCollection oRecipientCollection = null;

            oCmpSrv = SBO.ConexionDIAPI.oCompany.GetCompanyService();
            oMessageService = (SAPbobsCOM.MessagesService)oCmpSrv.GetBusinessService(ServiceTypes.MessagesService);
            oMessage = (SAPbobsCOM.Message)oMessageService.GetDataInterface(MessagesServiceDataInterfaces.msdiMessage);
            oMessages = (SAPbobsCOM.Messages)SBO.ConexionDIAPI.oCompany.GetBusinessObject(BoObjectTypes.oMessages);

            oMessage.User = SBO.ConsultasSBO.ObtenerIDConnectedUser(SBO.ConexionDIAPI.oCompany.UserName);
            oMessage.Subject = "Control de Tiempo: Solicitud para aprobar generación de documento";
            oMessage.Text = DateTime.Now.ToString() + " " + "Solicitud para aprobar generación de documento";

            SAPbobsCOM.RecipientCollection RecipientCollection = oMessage.RecipientCollection;
            RecipientCollection.Add();
            RecipientCollection.Item(0).SendInternal = BoYesNoEnum.tYES;
            RecipientCollection.Item(0).SendEmail = BoYesNoEnum.tYES;
            RecipientCollection.Item(0).UserCode = "cyc";

            SAPbobsCOM.MessageDataColumns MessageDataColumns = null;
            SAPbobsCOM.MessageDataColumn MessageDataColumn = null;
            MessageDataColumns = oMessage.MessageDataColumns;

            MessageDataColumn = MessageDataColumns.Add();
            MessageDataColumn.ColumnName = "Borrador Entrega";
            MessageDataColumn.Link = BoYesNoEnum.tYES;
            oLine = MessageDataColumn.MessageDataLines.Add();
            oLine.Value = DocNum;
            oLine.Object = "112";
            oLine.ObjectKey = DocEntry;

            MessageDataColumn = MessageDataColumns.Add();
            MessageDataColumn.ColumnName = "Cliente";
            MessageDataColumn.Link = BoYesNoEnum.tYES;
            oLine = MessageDataColumn.MessageDataLines.Add();
            oLine.Value = CardCode;
            oLine.Object = "2";
            oLine.ObjectKey = CardCode;

            oMessages.Add();
            oMessageService.SendMessage(oMessage);
        }
    }
}