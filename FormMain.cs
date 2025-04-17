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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            // فتح نموذج إدارة المنتجات
            FormProducts productsForm = new FormProducts();
            productsForm.ShowDialog();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            // فتح نموذج المبيعات
            FormSales salesForm = new FormSales();
            salesForm.ShowDialog();
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            // فتح نموذج إدارة المخزون
            FormInventory inventoryForm = new FormInventory();
            inventoryForm.ShowDialog();
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            // فتح نموذج إدارة العملاء
            FormCustomers customersForm = new FormCustomers();
            customersForm.ShowDialog();
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            // فتح نموذج إدارة الموردين
            FormSuppliers suppliersForm = new FormSuppliers();
            suppliersForm.ShowDialog();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            // فتح نموذج التقارير
            FormReports reportsForm = new FormReports();
            reportsForm.ShowDialog();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            // فتح نموذج الإعدادات
            FormSettings settingsForm = new FormSettings();
            settingsForm.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // تسجيل الخروج والعودة إلى نموذج تسجيل الدخول
            if (MessageBox.Show("هل أنت متأكد من تسجيل الخروج؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                FormLogin loginForm = new FormLogin();
                this.Hide();
                loginForm.ShowDialog();
                this.Close();
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // تحديث معلومات لوحة المعلومات
            UpdateDashboard();
        }

        private void UpdateDashboard()
        {
            // في التطبيق الحقيقي، يجب استرداد هذه المعلومات من قاعدة البيانات
            // هنا نستخدم قيم ثابتة للتجربة فقط
            lblTotalProducts.Text = "1250";
            lblLowStock.Text = "15";
            lblTodaySales.Text = "25000 دج";
            lblMonthSales.Text = "750000 دج";
        }

        private void timerDashboard_Tick(object sender, EventArgs e)
        {
            // تحديث الوقت والتاريخ
            lblDateTime.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }
    }
}
