using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyApp
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // في التطبيق الحقيقي، يجب التحقق من اسم المستخدم وكلمة المرور
            // هنا نستخدم قيم ثابتة للتجربة فقط
            if (txtUsername.Text == "admin" && txtPassword.Text == "admin123")
            {
                // تسجيل الدخول بنجاح
                MessageBox.Show("تم تسجيل الدخول بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // فتح النموذج الرئيسي
                FormMain mainForm = new FormMain();
                this.Hide();
                mainForm.ShowDialog();
                this.Close();
            }
            else
            {
                // فشل تسجيل الدخول
                MessageBox.Show("اسم المستخدم أو كلمة المرور غير صحيحة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // إغلاق التطبيق
            Application.Exit();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            // تعيين التركيز على حقل اسم المستخدم
            txtUsername.Focus();
        }
    }
}
