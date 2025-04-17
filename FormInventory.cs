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
    public partial class FormInventory : Form
    {
        // قائمة لتخزين بيانات المخزون
        private DataTable inventoryData;

        public FormInventory()
        {
            InitializeComponent();
            
            // تهيئة جدول بيانات المخزون
            InitializeInventoryTable();
        }

        private void InitializeInventoryTable()
        {
            // إنشاء جدول لبيانات المخزون
            inventoryData = new DataTable();
            inventoryData.Columns.Add("رقم المنتج", typeof(int));
            inventoryData.Columns.Add("الباركود", typeof(string));
            inventoryData.Columns.Add("اسم المنتج", typeof(string));
            inventoryData.Columns.Add("الفئة", typeof(string));
            inventoryData.Columns.Add("الكمية الحالية", typeof(int));
            inventoryData.Columns.Add("الحد الأدنى", typeof(int));
            inventoryData.Columns.Add("تاريخ انتهاء الصلاحية", typeof(DateTime));
            inventoryData.Columns.Add("رقم التشغيلة", typeof(string));
            inventoryData.Columns.Add("آخر تحديث", typeof(DateTime));

            // ربط الجدول بعنصر عرض البيانات
            dgvInventory.DataSource = inventoryData;
            dgvInventory.AutoResizeColumns();
        }

        private void FormInventory_Load(object sender, EventArgs e)
        {
            // تحميل بيانات المخزون
            LoadInventoryData();
            
            // تحميل قائمة الفئات
            LoadCategories();
        }

        private void LoadInventoryData()
        {
            // في التطبيق الحقيقي، يجب استرداد بيانات المخزون من قاعدة البيانات
            // هنا نستخدم بيانات تجريبية للعرض فقط
            
            // مسح البيانات الحالية
            inventoryData.Rows.Clear();
            
            // إضافة بيانات تجريبية
            inventoryData.Rows.Add(1, "1234567890123", "باراسيتامول 500 ملغ", "مسكنات الألم", 100, 20, DateTime.Parse("2026-12-31"), "LOT123456", DateTime.Now.AddDays(-5));
            inventoryData.Rows.Add(2, "2345678901234", "أموكسيسيلين 500 ملغ", "مضادات حيوية", 80, 15, DateTime.Parse("2026-06-30"), "LOT234567", DateTime.Now.AddDays(-10));
            inventoryData.Rows.Add(3, "3456789012345", "فيتامين سي 1000 ملغ", "فيتامينات ومكملات", 50, 10, DateTime.Parse("2027-01-31"), "LOT345678", DateTime.Now.AddDays(-15));
            inventoryData.Rows.Add(4, "4567890123456", "أتورفاستاتين 20 ملغ", "أدوية القلب والأوعية الدموية", 30, 5, DateTime.Parse("2026-09-30"), "LOT456789", DateTime.Now.AddDays(-20));
            inventoryData.Rows.Add(5, "5678901234567", "لوراتادين 10 ملغ", "مسكنات الألم", 60, 10, DateTime.Parse("2026-10-31"), "LOT567890", DateTime.Now.AddDays(-25));
            
            // تحديث عرض البيانات
            dgvInventory.Refresh();
            
            // تحديث عدد المنتجات
            lblTotalProducts.Text = inventoryData.Rows.Count.ToString();
            
            // تحديث عدد المنتجات التي وصلت للحد الأدنى
            int lowStockCount = 0;
            foreach (DataRow row in inventoryData.Rows)
            {
                if ((int)row["الكمية الحالية"] <= (int)row["الحد الأدنى"])
                {
                    lowStockCount++;
                }
            }
            lblLowStock.Text = lowStockCount.ToString();
            
            // تحديث عدد المنتجات التي اقتربت من انتهاء الصلاحية
            int nearExpiryCount = 0;
            DateTime threeMonthsFromNow = DateTime.Now.AddMonths(3);
            foreach (DataRow row in inventoryData.Rows)
            {
                if ((DateTime)row["تاريخ انتهاء الصلاحية"] <= threeMonthsFromNow)
                {
                    nearExpiryCount++;
                }
            }
            lblNearExpiry.Text = nearExpiryCount.ToString();
        }

        private void LoadCategories()
        {
            // في التطبيق الحقيقي، يجب استرداد قائمة الفئات من قاعدة البيانات
            // هنا نستخدم بيانات تجريبية للعرض فقط
            
            // مسح القائمة الحالية
            cmbCategory.Items.Clear();
            
            // إضافة خيار "الكل"
            cmbCategory.Items.Add("الكل");
            
            // إضافة فئات تجريبية
            cmbCategory.Items.Add("مضادات حيوية");
            cmbCategory.Items.Add("مسكنات الألم");
            cmbCategory.Items.Add("فيتامينات ومكملات");
            cmbCategory.Items.Add("أدوية القلب والأوعية الدموية");
            cmbCategory.Items.Add("أدوية الجهاز الهضمي");
            
            // تحديد الخيار الافتراضي
            cmbCategory.SelectedIndex = 0;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // البحث في بيانات المخزون
            SearchInventory();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            // إذا تم الضغط على مفتاح الإدخال (Enter)
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // منع صوت التنبيه
                
                // البحث في بيانات المخزون
                SearchInventory();
            }
        }

        private void SearchInventory()
        {
            // في التطبيق الحقيقي، يجب البحث في قاعدة البيانات
            // هنا نستخدم بحث بسيط في البيانات المحلية
            
            string searchText = txtSearch.Text.Trim().ToLower();
            string categoryFilter = cmbCategory.SelectedItem.ToString();
            
            // إذا كان نص البحث فارغًا والفئة هي "الكل"، عرض جميع البيانات
            if (string.IsNullOrEmpty(searchText) && categoryFilter == "الكل")
            {
                LoadInventoryData();
                return;
            }
            
            // إنشاء جدول جديد لنتائج البحث
            DataTable searchResults = inventoryData.Clone();
            
            // البحث في البيانات
            foreach (DataRow row in inventoryData.Rows)
            {
                bool matchesSearch = string.IsNullOrEmpty(searchText) ||
                                    row["الباركود"].ToString().ToLower().Contains(searchText) ||
                                    row["اسم المنتج"].ToString().ToLower().Contains(searchText) ||
                                    row["رقم التشغيلة"].ToString().ToLower().Contains(searchText);
                
                bool matchesCategory = categoryFilter == "الكل" ||
                                      row["الفئة"].ToString() == categoryFilter;
                
                if (matchesSearch && matchesCategory)
                {
                    searchResults.ImportRow(row);
                }
            }
            
            // عرض نتائج البحث
            dgvInventory.DataSource = searchResults;
            dgvInventory.Refresh();
            
            // تحديث عدد النتائج
            lblTotalProducts.Text = searchResults.Rows.Count.ToString();
        }

        private void btnAddStock_Click(object sender, EventArgs e)
        {
            // التحقق من تحديد منتج
            if (dgvInventory.SelectedRows.Count == 0)
            {
                MessageBox.Show("يرجى تحديد منتج لإضافة مخزون له", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            // في التطبيق الحقيقي، يجب فتح نموذج لإضافة مخزون
            // هنا نستخدم مربع حوار بسيط لإدخال الكمية
            string input = Microsoft.VisualBasic.Interaction.InputBox("أدخل الكمية المراد إضافتها:", "إضافة مخزون", "1");
            
            // التحقق من صحة الإدخال
            if (int.TryParse(input, out int quantity) && quantity > 0)
            {
                // الحصول على الصف المحدد
                DataRowView selectedRow = (DataRowView)dgvInventory.SelectedRows[0].DataBoundItem;
                
                // تحديث الكمية
                int currentQuantity = (int)selectedRow["الكمية الحالية"];
                selectedRow["الكمية الحالية"] = currentQuantity + quantity;
                selectedRow["آخر تحديث"] = DateTime.Now;
                
                // عرض رسالة نجاح
                MessageBox.Show($"تم إضافة {quantity} وحدة بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // تحديث عرض البيانات
                dgvInventory.Refresh();
                
                // في التطبيق الحقيقي، يجب تحديث قاعدة البيانات
            }
            else
            {
                MessageBox.Show("يرجى إدخال كمية صحيحة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateExpiry_Click(object sender, EventArgs e)
        {
            // التحقق من تحديد منتج
            if (dgvInventory.SelectedRows.Count == 0)
            {
                MessageBox.Show("يرجى تحديد منتج لتحديث تاريخ انتهاء الصلاحية", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            // في التطبيق الحقيقي، يجب فتح نموذج لتحديث تاريخ انتهاء الصلاحية
            // هنا نستخدم مربع حوار بسيط لإدخال التاريخ
            
            // الحصول على الصف المحدد
            DataRowView selectedRow = (DataRowView)dgvInventory.SelectedRows[0].DataBoundItem;
            
            // عرض نموذج تحديد التاريخ
            using (Form dateForm = new Form())
            {
                dateForm.Text = "تحديث تاريخ انتهاء الصلاحية";
                dateForm.Size = new Size(300, 150);
                dateForm.StartPosition = FormStartPosition.CenterParent;
                dateForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                dateForm.MaximizeBox = false;
                dateForm.MinimizeBox = false;
                
                DateTimePicker dtp = new DateTimePicker();
                dtp.Format = DateTimePickerFormat.Short;
                dtp.Value = (DateTime)selectedRow["تاريخ انتهاء الصلاحية"];
                dtp.Location = new Point(75, 20);
                dtp.Width = 150;
                
                Button btnOk = new Button();
                btnOk.Text = "موافق";
                btnOk.DialogResult = DialogResult.OK;
                btnOk.Location = new Point(75, 60);
                btnOk.Width = 150;
                
                dateForm.Controls.Add(dtp);
                dateForm.Controls.Add(btnOk);
                
                if (dateForm.ShowDialog() == DialogResult.OK)
                {
                    // تحديث تاريخ انتهاء الصلاحية
                    selectedRow["تاريخ انتهاء الصلاحية"] = dtp.Value.Date;
                    selectedRow["آخر تحديث"] = DateTime.Now;
                    
                    // عرض رسالة نجاح
                    MessageBox.Show("تم تحديث تاريخ انتهاء الصلاحية بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // تحديث عرض البيانات
                    dgvInventory.Refresh();
                    
                    // في التطبيق الحقيقي، يجب تحديث قاعدة البيانات
                }
            }
        }

        private void btnExpiryReport_Click(object sender, EventArgs e)
        {
            // في التطبيق الحقيقي، يجب فتح تقرير المنتجات التي اقتربت من انتهاء الصلاحية
            // هنا نعرض رسالة فقط
            MessageBox.Show("جاري فتح تقرير المنتجات التي اقتربت من انتهاء الصلاحية...", "تقرير", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLowStockReport_Click(object sender, EventArgs e)
        {
            // في التطبيق الحقيقي، يجب فتح تقرير المنتجات التي وصلت للحد الأدنى
            // هنا نعرض رسالة فقط
            MessageBox.Show("جاري فتح تقرير المنتجات التي وصلت للحد الأدنى...", "تقرير", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // إعادة تحميل بيانات المخزون
            LoadInventoryData();
            
            // مسح نص البحث
            txtSearch.Clear();
            
            // إعادة تعيين فلتر الفئة
            cmbCategory.SelectedIndex = 0;
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            // في التطبيق الحقيقي، يجب تصدير البيانات إلى ملف Excel
            // هنا نعرض رسالة فقط
            MessageBox.Show("جاري تصدير البيانات إلى ملف Excel...", "تصدير", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            // في التطبيق الحقيقي، يجب طباعة تقرير المخزون
            // هنا نعرض رسالة فقط
            MessageBox.Show("جاري طباعة تقرير المخزون...", "طباعة", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
