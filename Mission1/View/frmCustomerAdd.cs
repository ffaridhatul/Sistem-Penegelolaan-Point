using System;
using System.Windows.Forms;
using Mission1.Business;
using Mission1.Model;

namespace Mission1.View
{
    public partial class frmCustomerAdd : Form
    {
        public frmCustomerAdd()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Buat objek Customer
            var customer = new Customer
            {
                PhoneNo = txtPhoneNo.Text,
                Name = txtName.Text,
                Balance = 0,
                VisitCount = 0,
                LastVisitDate = DateTime.Now,
            };

            // Dapatkan instance PointWorkBiz
            var pointWorkBiz = PointWorkBiz.GetInstance();

            // Tambahkan pelanggan ke sistem
            pointWorkBiz.AddCustomer(customer);

            DialogResult = DialogResult.OK;
        }
    }
}
