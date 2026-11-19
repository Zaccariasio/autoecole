using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace autoecole
{
    public partial class Menu1 : Form
    {
        private autoecoleEntities mesDonneesEF;
        public Menu1()
        {
            InitializeComponent();
            this.mesDonneesEF = new autoecoleEntities();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void gérerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmEleve f = new FrmEleve(this.mesDonneesEF);
            f.MdiParent =  this;
            f.Show();

        }
    }
}
