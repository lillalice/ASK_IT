using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.ServiceModel;
using dotLAND_ASKIT_serviceUser;

namespace abc
{
    public partial class ParentForm : MetroForm
    {
        /* CONSTRUCTOR */
        public ParentForm()
        {
            InitializeComponent();
        }

        /* LOAD METHOD */
        private void ParentForm_Load(object sender, EventArgs e)
        {
            Login nyan = new Login();
            nyan.MdiParent = this;
            nyan.Show();
        }
    }
}
