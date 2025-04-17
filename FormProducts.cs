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
    public partial class FormProducts : Form
    {
        public FormProducts()
        {
            InitializeComponent();
        }

        private void FormProducts_Load(object sender, EventArgs e)
        {
            // تحميل بيانات المنتجات عند فتح النموذج
            LoadProducts();
        }

        private void LoadProducts()
        {
            // في التطبيق الحقيقي، يجب استرداد البيانات من قاعدة البيانات
            // هنا نستخدم بيانات تجريبية للعرض فقط
            DataTable dt = new DataTable();
            dt.Columns.Add("رقم المنتج", typeof(int));
            dt.Columns.Add("الباركود", typeof(string));
            dt.Columns.Add("اسم المنتج", typeof(string));
            dt.Columns.Add("الفئة", typeof(string));
            dt.Columns.Add("سعر الشراء", typeof(decimal));
            dt.Columns.Add("سعر البيع", typeof(decimal));
            dt.Columns.Add("الكمية", typeof(int));
            dt.Columns.Add("تاريخ انتهاء الصلاحية", typeof(DateTime));

            // إضافة بيانات تجريبية
            dt.Rows.Add(1, "1234567890123", "باراسيتامول 500 ملغ", "مسكنات", 50.00m, 65.00m, 100, DateTime.Now.AddMonths(12));
            dt.Rows.Add(2, "2345678901234", "أموكسيسيلين 500 ملغ", "مضادات حيوية", 80.00m, 100.00m, 75, DateTime.Now.AddMonths(18));
            dt.Rows.Add(3, "3456789012345", "فيتامين سي 1000 ملغ", "فيتامينات", 40.00m, 55.00m, 120, DateTime.Now.AddMonths(24));
            dt.Rows.Add(4, "4567890123456", "أسبرين 100 ملغ", "مسكنات", 30.00m, 45.00m, 200, DateTime.Now.AddMonths(36));
            dt.Rows.Add(5, "5678901234567", "لوراتادين 10 ملغ", "مضادات الحساسية", 60.00m, 75.00m, 50, DateTime.Now.AddMonths(24));

            // عرض البيانات في جدول البيانات
            dgvProducts.DataSource = dt;
            dgvProducts.AutoResizeColumns();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // التحقق من صحة البيانات المدخلة
            if (string.IsNullOrEmpty(txtBarcode.Text) || string.IsNullOrEmpty(txtName.Text) || 
                string.IsNullOrEmpty(txtBuyPrice.Text) || string.IsNullOrEmpty(txtSellPrice.Text) || 
                string.IsNullOrEmpty(txtQuantity.Text))
            {
                MessageBox.Show("يرجى إدخال جميع البيانات المطلوبة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // في التطبيق الحقيقي، يجب إضافة المنتج إلى قاعدة البيانات
            // هنا نعرض رسالة نجاح فقط
            MessageBox.Show("تمت إضافة المنتج بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // مسح حقول الإدخال
            ClearInputFields();

            // إعادة تحميل البيانات
            LoadProducts();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // التحقق من تحديد صف في جدول البيانات
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("يرجى تحديد منتج لتحديثه", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // التحقق من صحة البيانات المدخلة
            if (string.IsNullOrEmpty(txtBarcode.Text) || string.IsNullOrEmpty(txtName.Text) || 
                string.IsNullOrEmpty(txtBuyPrice.Text) || string.IsNullOrEmpty(txtSellPrice.Text) || 
                string.IsNullOrEmpty(txtQuantity.Text))
            {
                MessageBox.Show("يرجى إدخال جميع البيانات المطلوبة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // في التطبيق الحقيقي، يجب تحديث المنتج في قاعدة البيانات
            // هنا نعرض رسالة نجاح فقط
            MessageBox.Show("تم تحديث المنتج بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // مسح حقول الإدخال
            ClearInputFields();

            // إعادة تحميل البيانات
            LoadProducts();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // التحقق من تحديد صف في جدول البيانات
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("يرجى تحديد منتج لحذفه", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // طلب تأكيد الحذف
            if (MessageBox.Show("هل أنت متأكد من حذف هذا المنتج؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // في التطبيق الحقيقي، يجب حذف المنتج من قاعدة البيانات
                // هنا نعرض رسالة نجاح فقط
                MessageBox.Show("تم حذف المنتج بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // مسح حقول الإدخال
                ClearInputFields();

                // إعادة تحميل البيانات
                LoadProducts();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // مسح حقول الإدخال
            ClearInputFields();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // التحقق من إدخال نص البحث
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                MessageBox.Show("يرجى إدخال نص البحث", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // في التطبيق الحقيقي، يجب البحث عن المنتج في قاعدة البيانات
            // هنا نعرض رسالة فقط
            MessageBox.Show("جاري البحث عن: " + txtSearch.Text, "بحث", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnScanBarcode_Click(object sender, EventArgs e)
        {
            // في التطبيق الحقيقي، يجب فتح نافذة لمسح الباركود
            // هنا نعرض رسالة فقط
            MessageBox.Show("جاري فتح ماسح الباركود...", "مسح الباركود", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            // عرض بيانات المنتج المحدد في حقول الإدخال
            if (dgvProducts.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvProducts.SelectedRows[0];
                txtBarcode.Text = row.Cells["الباركود"].Value.ToString();
                txtName.Text = row.Cells["اسم المنتج"].Value.ToString();
                cmbCategory.Text = row.Cells["الفئة"].Value.ToString();
                txtBuyPrice.Text = row.Cells["سعر الشراء"].Value.ToString();
                txtSellPrice.Text = row.Cells["سعر البيع"].Value.ToString();
                txtQuantity.Text = row.Cells["الكمية"].Value.ToString();
                dtpExpiryDate.Value = (DateTime)row.Cells["تاريخ انتهاء الصلاحية"].Value;
            }
        }

        private void ClearInputFields()
        {
            // مسح جميع حقول الإدخال
            txtBarcode.Clear();
            txtName.Clear();
            cmbCategory.SelectedIndex = -1;
            txtBuyPrice.Clear();
            txtSellPrice.Clear();
            txtQuantity.Clear();
            dtpExpiryDate.Value = DateTime.Now.AddYears(1);
            txtSearch.Clear();
        }
    }
}
