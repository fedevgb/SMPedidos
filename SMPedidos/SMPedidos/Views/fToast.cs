using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMPedidos.Views
{
    public partial class fToast : KryptonForm
    {
        private int time2Exit = 5;
        private string msg = "";

        public fToast(string msg_)
        {
            InitializeComponent();

            this.msg = msg_;
        }

        private void fToast_Load(object sender, EventArgs e)
        {
            Panel pnlTop = new Panel() {Height=4, Dock = DockStyle.Top, BackColor = Color.Maroon };
            Panel pnlRight = new Panel() { Width = 4, Dock = DockStyle.Right, BackColor = Color.Maroon };
            Panel pnlBottom = new Panel() { Height = 4, Dock = DockStyle.Bottom, BackColor = Color.Maroon };
            Panel pnlLeft = new Panel() { Height = 4, Dock = DockStyle.Left, BackColor = Color.Maroon };

            this.Controls.Add(pnlTop);
            this.Controls.Add(pnlRight);
            this.Controls.Add(pnlBottom);
            this.Controls.Add(pnlLeft);
        }


    }
}
