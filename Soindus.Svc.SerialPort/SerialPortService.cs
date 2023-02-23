using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using EBCSoftLib = EBCSoft.SerialPortCapturer.Library;

namespace Soindus.Svc.SerialPort
{
    public partial class SerialPortService : ServiceBase
    {
        private Local.ParametrosSvc Parametros;
        private string txtLog;
        private Timer timer0;

        public SerialPortService()
        {
            InitializeComponent();
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("Svc.SerialPort"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "Svc.SerialPort", "Soindus");
            }
            eventLog1.Source = "Svc.SerialPort";
            eventLog1.Log = "Soindus";
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("Servicio Svc.SerialPort Iniciando...", EventLogEntryType.Information, Convert.ToInt32(Local.Enumeradores.ProcesosSvcSerialPort.Inicio));
            Parametros = new Local.ParametrosSvc();
            Timer timer = new Timer();
            timer.Interval = Parametros.Params.TimerMS;
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();

            timer0 = new Timer();
            timer0.Interval = TimeSpan.FromSeconds(10).TotalMilliseconds;
            timer0.Elapsed += new ElapsedEventHandler(this.OnTimer0);
            timer0.Start();

            eventLog1.WriteEntry("Servicio Svc.SerialPort iniciado correctamente.", EventLogEntryType.Information, Convert.ToInt32(Local.Enumeradores.ProcesosSvcSerialPort.Inicio));
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("Servicio Svc.SerialPort detenido correctamente.", EventLogEntryType.Information, Convert.ToInt32(Local.Enumeradores.ProcesosSvcSerialPort.Stop));
        }

        protected override void OnPause()
        {
            eventLog1.WriteEntry("Servicio Svc.SerialPort pausado correctamente.", EventLogEntryType.Information, Convert.ToInt32(Local.Enumeradores.ProcesosSvcSerialPort.Pausa));
        }

        protected override void OnContinue()
        {
            eventLog1.WriteEntry("Servicio Svc.SerialPort re-iniciado correctamente.", EventLogEntryType.Information, Convert.ToInt32(Local.Enumeradores.ProcesosSvcSerialPort.Re_Inicio));
        }

        private void AlertOnProcess(string message, int process)
        {
            eventLog1.WriteEntry(string.Format("Servicio Svc.SerialPort Alerta - {0}", message), EventLogEntryType.Warning, process);
        }

        private void StopService()
        {
            Stop();
        }

        private void Proceso()
        {
            try
            {
                EBCSoftLib.PuertoSerie puertoSerie = new EBCSoftLib.PuertoSerie();
                System.Threading.Thread.Sleep(Parametros.Params.TimerMS);
                ApiRestSBO.RegistrarCaptura regCap = new ApiRestSBO.RegistrarCaptura() { 
                    UrlApi = Parametros.Params.UrlApiRestSBO,
                    Com_ID = Parametros.Params.ComID, 
                    User_ID = Parametros.Params.UserID, 
                    StrValue = puertoSerie.respuesta };
                string result = regCap.Registrar();
            }
            catch (Exception ex)
            {
                AlertOnProcess("(Procesando...) " + ex.Message, Convert.ToInt32(Local.Enumeradores.ProcesosSvcSerialPort.Proceso));
            }
        }

        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            Proceso();
        }

        public void OnTimer0(object sender, ElapsedEventArgs args)
        {
            timer0.Stop();
            Proceso();
        }
    }
}
