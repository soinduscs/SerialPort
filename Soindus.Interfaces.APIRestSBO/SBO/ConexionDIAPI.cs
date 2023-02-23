using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAPbobsCOM;

namespace Soindus.Interfaces.APIRestSBO.SBO
{
    public class ConexionDIAPI
    {
        public static SAPbobsCOM.Company oCompany = null;

        public ConexionDIAPI()
        {
            // Conectar DIAPI
            ConectarCompany();
        }

        /// <summary>
        /// Metodo para conectar a la compañia SBO (DIAPI)
        /// </summary>
        public static void ConectarCompany()
        {
            oCompany = new SAPbobsCOM.Company();
            Local.Configuracion configuracion = new Local.Configuracion();
            switch (configuracion.Params.DbServerType)
            {
                case "HANA":
                    oCompany.DbServerType = BoDataServerTypes.dst_HANADB;
                    break;
                case "2005":
                    oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2005;
                    break;
                case "2008":
                    oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2008;
                    break;
                case "2012":
                    oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2012;
                    break;
                case "2014":
                    oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2014;
                    break;
                case "2016":
                    oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2016;
                    break;
                case "2017":
                    oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2017;
                    break;
                default:
                    oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2017;
                    break;
            }
            oCompany.Server = configuracion.Params.Server;
            oCompany.LicenseServer = configuracion.Params.LicenseServer;
            oCompany.CompanyDB = configuracion.Params.CompanyDB;
            oCompany.UserName = configuracion.Params.UserName;
            oCompany.Password = configuracion.Params.Password;
            oCompany.DbUserName = configuracion.Params.DbUserName;
            oCompany.DbPassword = configuracion.Params.DbPassword;
            oCompany.UseTrusted = false;
            oCompany.language = BoSuppLangs.ln_Spanish_La;

            if (!oCompany.Connected)
            {
                var ret = oCompany.Connect();
                if (!oCompany.Connected)
                {
                    throw new Exception("No se pudo establecer la conexión DIAPI.");
                }
            }
        }

        /// <summary>
        /// Metodo para desconeconectar la compañia SBO (DIAPI)
        /// </summary>
        public static void DesconectarCompany()
        {
            try
            {
                if (oCompany.Connected)
                {
                    oCompany.Disconnect();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}