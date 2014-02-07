using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Threading;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using fyiReporting.RdlDesign.Netsis.Rdl.Designer;
using Netsis.Framework.Persister;
using Telerik.WinControls;
using fyiReporting.RdlDesign.Netsis.Rdl.Designer.Helpers;
using fyiReporting.RdlDesign.Netsis.Rdl.Designer.Localization;
using Telerik.WinControls.UI.Localization;
using System.Diagnostics;

namespace Netsis.Rdl.Designer
{
    public class ReportDesignerApp : WindowsFormsApplicationBase
    {
        #region ..Fields..
        
        private int stepFactor = 5;

        #endregion

        #region ..Constructors..

        public ReportDesignerApp()
            : base()
        {
            IsSingleInstance = true;
        }

        #endregion

        protected override void OnCreateSplashScreen()
        {
            this.SplashScreen = new Splash() { StepFactor = stepFactor };
        }

        protected override bool OnInitialize(ReadOnlyCollection<string> commandLineArgs)
        {
            //
            return base.OnInitialize(commandLineArgs);
        }

        protected override void OnShutdown()
        {
            //
            base.OnShutdown();
        }

        protected override void OnCreateMainForm()
        {
            // force to create handle immediately
            this.ShowSplashScreen();
            Initialize();
            // create main form
            this.MainForm = new frmNetsisRdlDeginer();
        }

        #region ..Private Calls..

        private void SetSplashProgress()
        {
            var splash = ((Splash)this.SplashScreen);
            if (splash.IsHandleCreated)
                splash.BeginInvoke((MethodInvoker)delegate { splash.SetProgress(); });
        }

        private void SetSplashInfo(string value)
        {
            var splash = ((Splash)this.SplashScreen);
            if (splash.IsHandleCreated)
                splash.BeginInvoke((MethodInvoker)delegate { splash.SetProgressInfo(value); });
        }

        private void Initialize()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new System.UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            try
            {
                TryLocalizing();
                TryTraceAndNDALConfigure();
                TryConnectDB();
                TryInitializeCache();
                //TryPerformVersionCheck();
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(ex.Message + Environment.NewLine + "Program Kapanacaktır.", "Hata", MessageBoxButtons.OK, RadMessageIcon.Error, MessageBoxDefaultButton.Button1);
                Process.GetCurrentProcess().Kill();
            }
        }

        // Localization
        private void TryLocalizing()
        {
            try
            {
                SetSplashInfo("Yerelleştirme ayarları yapılandırılıyor.."); // TODO - resource
                //Thread.CurrentThread.CurrentUICulture = new CultureInfo("tr-TR", true);
                //Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;
                //TODO ARMİ :
                RadGridLocalizationProvider.CurrentProvider = new CustomRadGridLocalizationProvider();
                RadDockLocalizationProvider.CurrentProvider = new CustomRadDockLocalizationProvider();
                //
                SetSplashProgress();
            }
            catch (Exception ex)
            {                
                throw new Exception( "Yerelleştirme ayarları uyarlanırken hata oluştu. Hata detayı :" + Environment.NewLine + ex.Message);
            }
        }

        // configure NDAL
        private void TryTraceAndNDALConfigure()
        {
            try
            {
                SetSplashInfo("İzleme ve Veritabanı ayarları yapılandırılıyor.."); // TODO - resource                
                DbConfigurator.Configure();                
                SetSplashProgress();
            }
            catch (Exception ex)
            {                
                throw new Exception("Veritabanı ayarları uyarlanırken hata oluştu. Hata detayı :" + Environment.NewLine + ex.Message);
            }
        }

        // try connect to DB
        private void TryConnectDB()
        {
            try
            {
                SetSplashInfo("Veritabanı bağlantısı açılıyor.."); // TODO - resource
                using (var persister = new NPersister()) { };             
                SetSplashProgress();
            }
            catch (Exception ex)
            {                
                throw new Exception("Veritabanına bağlanırken hata oluştu. Hata detayı :" + Environment.NewLine + ex.Message);
            }
        }

        // try initialize cache
        private void TryInitializeCache()
        {
            try
            {
                SetSplashInfo("Önbellekleme yapılandırılıyor.."); // TODO - resoource
                SetSplashProgress();
            }
            catch (Exception ex)
            {                
                throw new Exception("Önbellekleme sırasında hata oluştu. Hata detayı :" + Environment.NewLine + ex.Message);
            }
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            RadMessageBox.Show(e.Exception.Message, "Hata", MessageBoxButtons.OK, RadMessageIcon.Error);
        }

        static void CurrentDomain_UnhandledException(object sender, System.UnhandledExceptionEventArgs e)
        {
            RadMessageBox.Show((e.ExceptionObject as Exception).Message, "Hata", MessageBoxButtons.OK, RadMessageIcon.Error);            
        }

        #endregion
    }
}
