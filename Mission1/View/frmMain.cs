using Mission1.Business;
using Mission1.Model;
using System;
using System.Windows.Forms;

namespace Mission1.View
{
    public partial class frmMain : Form
    {
        private Rule rule;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            rule = FileDB.LoadRule();
            ClearCustomerInfo();
        }

        private void lblCustomerAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new frmCustomerAdd();
            if (form.ShowDialog() == DialogResult.OK)
            {
                // Optional: Lakukan tindakan tambahan setelah pelanggan ditambahkan.
                DisplayCustomerInfo();
            }
        }

        private void lblPointHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var customer = PointWorkBiz.GetInstance().GetCustomer(txtPhoneNo.Text);
            if (customer != null)
            {
                var form = new frmPointHistory { Customer = customer };
                form.ShowDialog();
            }
            else
            {
                lblMessage.Text = "Pelanggan tidak ditemukan.";
            }
        }

        private void lblCustomerList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new frmCustomerList();
            form.ShowDialog();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DisplayCustomerInfo();
        }

        private void DisplayCustomerInfo()
        {
            ClearCustomerInfo();

            var customer = PointWorkBiz.GetInstance().GetCustomer(txtPhoneNo.Text);

            if (customer == null)
            {
                lblMessage.Text = $"{txtPhoneNo.Text} Tidak ada pelanggan untuk nomor HP ini.";
                return;
            }

            lblName.Text = customer.Name;
            lblLastVisitDate.Text = customer.LastVisitDate.ToString("yyyy-MM-dd");
            lblBalance.Text = customer.Balance.ToString("#,0");
            lblVisitCount.Text = customer.VisitCount.ToString("#,0");

            btnEarn.Enabled = true;
            btnUse.Enabled = true;
            lblPointHistory.Enabled = true;
        }

        private void ClearCustomerInfo()
        {
            lblName.Text = "";
            lblLastVisitDate.Text = "";
            lblVisitCount.Text = "";
            lblBalance.Text = "";

            lblMessage.Text = "";
            numUsePoint.Minimum = rule.MinimumUsablePoint;
            numUsePoint.Value = rule.MinimumUsablePoint;

            lblPointHistory.Enabled = false;
            btnEarn.Enabled = false;
            btnUse.Enabled = false;
        }

        private void btnEarn_Click(object sender, EventArgs e)
        {
            var customer = PointWorkBiz.GetInstance().GetCustomer(txtPhoneNo.Text);
            if (customer != null)
            {
                customer.EarnPoint((int)numEarnPoint.Value);
                DisplayCustomerInfo();
                lblMessage.Text = $"Poin {numEarnPoint.Value:#,0} sudah ditambahkan.";
            }
        }

        private void btnUse_Click(object sender, EventArgs e)
        {
            var customer = PointWorkBiz.GetInstance().GetCustomer(txtPhoneNo.Text);
            if (customer != null)
            {
                if (customer.UsePoint((int)numUsePoint.Value))
                {
                    DisplayCustomerInfo();
                    lblMessage.Text = $"Poin {numUsePoint.Value:#,0} sudah digunakan.";
                }
                else
                {
                    lblMessage.Text = "Saldo poin yang tersedia tidak mencukupi.";
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
