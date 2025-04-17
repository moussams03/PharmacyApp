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
    public partial class FormSales : Form
    {
        // قائمة لتخزين عناصر الفاتورة
        private DataTable invoiceItems;

        public FormSales()
        {
            InitializeComponent();
            
            // تهيئة جدول عناصر الفاتورة
            InitializeInvoiceItemsTable();
        }

        private void InitializeInvoiceItemsTable()
        {
            // إنشاء جدول لعناصر الفاتورة
            invoiceItems = new DataTable();
            invoiceItems.Columns.Add("رقم المنتج", typeof(int));
            invoiceItems.Columns.Add("الباركود", typeof(string));
            invoiceItems.Columns.Add("اسم المنتج", typeof(string));
            invoiceItems.Columns.Add("سعر الوحدة", typeof(decimal));
            invoiceItems.Columns.Add("الكمية", typeof(int));
            invoiceItems.Columns.Add("الخصم", typeof(decimal));
            invoiceItems.Columns.Add("الإجمالي", typeof(decimal));

            // ربط الجدول بعنصر عرض البيانات
            dgvInvoiceItems.DataSource = invoiceItems;
            dgvInvoiceItems.AutoResizeColumns();
        }

        private void FormSales_Load(object sender, EventArgs e)
        {
            // تعيين تاريخ الفاتورة إلى التاريخ الحالي
            dtpInvoiceDate.Value = DateTime.Now;
            
            // تعيين رقم فاتورة افتراضي
            txtInvoiceNumber.Text = GenerateInvoiceNumber();
            
            // تحديث إجماليات الفاتورة
            UpdateInvoiceTotals();
        }

        private string GenerateInvoiceNumber()
        {
            // في التطبيق الحقيقي، يجب استرداد آخر رقم فاتورة من قاعدة البيانات وزيادته
            // هنا نستخدم التاريخ والوقت الحالي لإنشاء رقم فاتورة فريد
            return "INV-" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            // التحقق من صحة البيانات المدخلة
            if (string.IsNullOrEmpty(txtBarcode.Text) || string.IsNullOrEmpty(txtProductName.Text) || 
                string.IsNullOrEmpty(txtUnitPrice.Text) || string.IsNullOrEmpty(txtQuantity.Text))
            {
                MessageBox.Show("يرجى إدخال جميع بيانات المنتج", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // التحقق من صحة الكمية والسعر
            decimal unitPrice;
            int quantity;
            decimal discount = 0;

            if (!decimal.TryParse(txtUnitPrice.Text, out unitPrice) || unitPrice <= 0)
            {
                MessageBox.Show("يرجى إدخال سعر صحيح", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out quantity) || quantity <= 0)
            {
                MessageBox.Show("يرجى إدخال كمية صحيحة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!string.IsNullOrEmpty(txtDiscount.Text))
            {
                if (!decimal.TryParse(txtDiscount.Text, out discount) || discount < 0)
                {
                    MessageBox.Show("يرجى إدخال خصم صحيح", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // حساب إجمالي سعر المنتج
            decimal total = (unitPrice * quantity) - discount;

            // إضافة المنتج إلى جدول الفاتورة
            invoiceItems.Rows.Add(
                invoiceItems.Rows.Count + 1,  // رقم المنتج في الفاتورة
                txtBarcode.Text,
                txtProductName.Text,
                unitPrice,
                quantity,
                discount,
                total
            );

            // مسح حقول إدخال المنتج
            ClearProductInputFields();

            // تحديث إجماليات الفاتورة
            UpdateInvoiceTotals();

            // تركيز المؤشر على حقل الباركود للإدخال التالي
            txtBarcode.Focus();
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            // التحقق من تحديد صف في جدول الفاتورة
            if (dgvInvoiceItems.SelectedRows.Count == 0)
            {
                MessageBox.Show("يرجى تحديد منتج لحذفه من الفاتورة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // حذف المنتج المحدد من جدول الفاتورة
            dgvInvoiceItems.Rows.RemoveAt(dgvInvoiceItems.SelectedRows[0].Index);

            // إعادة ترقيم المنتجات في الفاتورة
            for (int i = 0; i < invoiceItems.Rows.Count; i++)
            {
                invoiceItems.Rows[i]["رقم المنتج"] = i + 1;
            }

            // تحديث إجماليات الفاتورة
            UpdateInvoiceTotals();
        }

        private void btnScanBarcode_Click(object sender, EventArgs e)
        {
            // في التطبيق الحقيقي، يجب فتح نافذة لمسح الباركود
            // هنا نعرض رسالة فقط
            MessageBox.Show("جاري فتح ماسح الباركود...", "مسح الباركود", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // محاكاة قراءة باركود
            txtBarcode.Text = "5678901234567";
            
            // البحث عن المنتج في قاعدة البيانات (محاكاة)
            LookupProduct(txtBarcode.Text);
        }

        private void LookupProduct(string barcode)
        {
            // في التطبيق الحقيقي، يجب البحث عن المنتج في قاعدة البيانات
            // هنا نستخدم بيانات تجريبية للعرض فقط
            
            // محاكاة العثور على المنتج
            txtProductName.Text = "لوراتادين 10 ملغ";
            txtUnitPrice.Text = "75.00";
            txtQuantity.Text = "1";
            txtDiscount.Text = "0";
        }

        private void btnSearchProduct_Click(object sender, EventArgs e)
        {
            // في التطبيق الحقيقي، يجب فتح نافذة للبحث عن المنتجات
            // هنا نعرض رسالة فقط
            MessageBox.Show("جاري فتح نافذة البحث عن المنتجات...", "بحث عن منتج", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // محاكاة اختيار منتج من نافذة البحث
            txtBarcode.Text = "3456789012345";
            txtProductName.Text = "فيتامين سي 1000 ملغ";
            txtUnitPrice.Text = "55.00";
            txtQuantity.Text = "1";
            txtDiscount.Text = "0";
        }

        private void btnSaveInvoice_Click(object sender, EventArgs e)
        {
            // التحقق من وجود منتجات في الفاتورة
            if (invoiceItems.Rows.Count == 0)
            {
                MessageBox.Show("لا يمكن حفظ فاتورة فارغة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // في التطبيق الحقيقي، يجب حفظ الفاتورة في قاعدة البيانات
            // هنا نعرض رسالة نجاح فقط
            MessageBox.Show("تم حفظ الفاتورة بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // طباعة الفاتورة
            if (MessageBox.Show("هل ترغب في طباعة الفاتورة؟", "طباعة", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                PrintInvoice();
            }

            // إنشاء فاتورة جديدة
            NewInvoice();
        }

        private void btnNewInvoice_Click(object sender, EventArgs e)
        {
            // التحقق من وجود منتجات في الفاتورة الحالية
            if (invoiceItems.Rows.Count > 0)
            {
                if (MessageBox.Show("هل أنت متأكد من إنشاء فاتورة جديدة؟ سيتم فقدان الفاتورة الحالية.", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    NewInvoice();
                }
            }
            else
            {
                NewInvoice();
            }
        }

        private void NewInvoice()
        {
            // مسح جدول عناصر الفاتورة
            invoiceItems.Rows.Clear();

            // مسح حقول إدخال المنتج
            ClearProductInputFields();

            // تعيين رقم فاتورة جديد
            txtInvoiceNumber.Text = GenerateInvoiceNumber();

            // تعيين تاريخ الفاتورة إلى التاريخ الحالي
            dtpInvoiceDate.Value = DateTime.Now;

            // مسح حقول العميل
            txtCustomerName.Clear();
            txtCustomerPhone.Clear();

            // تحديث إجماليات الفاتورة
            UpdateInvoiceTotals();

            // تركيز المؤشر على حقل الباركود
            txtBarcode.Focus();
        }

        private void PrintInvoice()
        {
            // في التطبيق الحقيقي، يجب طباعة الفاتورة
            // هنا نعرض رسالة فقط
            MessageBox.Show("جاري طباعة الفاتورة...", "طباعة", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ClearProductInputFields()
        {
            // مسح حقول إدخال المنتج
            txtBarcode.Clear();
            txtProductName.Clear();
            txtUnitPrice.Clear();
            txtQuantity.Text = "1";
            txtDiscount.Text = "0";
        }

        private void UpdateInvoiceTotals()
        {
            decimal subtotal = 0;
            decimal totalDiscount = 0;

            // حساب المجموع الفرعي ومجموع الخصومات
            foreach (DataRow row in invoiceItems.Rows)
            {
                subtotal += (decimal)row["سعر الوحدة"] * (int)row["الكمية"];
                totalDiscount += (decimal)row["الخصم"];
            }

            // عرض المجموع الفرعي ومجموع الخصومات
            txtSubtotal.Text = subtotal.ToString("0.00");
            txtTotalDiscount.Text = totalDiscount.ToString("0.00");

            // حساب الضريبة (إذا كانت مطبقة)
            decimal taxRate = 0; // في الجزائر، قد تختلف نسبة الضريبة حسب نوع المنتج
            decimal taxAmount = subtotal * (taxRate / 100);
            txtTax.Text = taxAmount.ToString("0.00");

            // حساب المجموع النهائي
            decimal grandTotal = subtotal - totalDiscount + taxAmount;
            txtGrandTotal.Text = grandTotal.ToString("0.00");
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            // إذا تم الضغط على مفتاح الإدخال (Enter)
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // منع صوت التنبيه

                // البحث عن المنتج باستخدام الباركود
                if (!string.IsNullOrEmpty(txtBarcode.Text))
                {
                    LookupProduct(txtBarcode.Text);
                }
            }
        }
    }
}
