using Mission1.Model;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Mission1.View
{
    public partial class frmPointHistory : Form
    {
        public Customer Customer;

        public frmPointHistory()
        {
            InitializeComponent();
        }

        private void frmPointHistory_Load(object sender, EventArgs e)
        {
            lblName.Text = Customer.Name;
            lblBalance.Text = Customer.Balance.ToString("#,0");

            dgvPointHistory.AutoGenerateColumns = false;
            dgvPointHistory.DataSource = Customer.PointRecords.Select(p => new
            {
                p.RecordDate,
                PointRecordType = (p.PointRecordType == PointRecordTypeEnum.Earn) ? "Tambahkan" : "Gunakan",
                p.Amount
            }).ToList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
