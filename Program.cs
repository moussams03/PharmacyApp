using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // تعيين اللغة الافتراضية للتطبيق (العربية)
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-DZ");
            
            // تعيين اتجاه النص من اليمين إلى اليسار للغة العربية
            Application.CurrentInputLanguage = System.Globalization.CultureInfo.CreateSpecificCulture("ar-DZ").KeyboardLayoutId;
            
            // بدء التطبيق بنموذج تسجيل الدخول
            Application.Run(new FormLogin());
        }
    }
}
