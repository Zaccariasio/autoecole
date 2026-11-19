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
    public partial class FrmEleve : Form
    {
        private autoecoleEntities mesDonneesEF;
        public FrmEleve(autoecoleEntities mesDonneesEF)
        {
            InitializeComponent();
            for (int i = 0; i < 30; i++)
                this.cmbCredits.Items.Add(i);
            this.mesDonneesEF = mesDonneesEF;
            this.bdgSourceEleve.DataSource = this.mesDonneesEF.eleves.ToList();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            FrmEleve f = new FrmEleve(this.mesDonneesEF);
            f.MdiParent = this.MdiParent;
            f.Show();

        }
        private int getNumEleve()
        {
            var reqDernier = (from el in this.mesDonneesEF.eleves
                              orderby el.id descending
                              select el);
            eleve dernierEleve = reqDernier.First();
            int n = dernierEleve.id + 1;
            return n;
        }

        private void FrmEleve_Load(object sender, EventArgs e)
        {
            this.txtNum.Text = this.getNumEleve().ToString();
        }
        private eleve neweleve()
        {
            eleve newEleve = new eleve();
            newEleve.id = Convert.ToInt16(txtNum.Text);
            newEleve.nom = txtNom.Text;
            newEleve.prenom = txtPrenom.Text;
            newEleve.adresse = txtAdresse.Text;
            newEleve.dateInscription = dtpDateInscription.Value;
            return newEleve;
        }
        private void tsbEnregistrer_Click(object sender, EventArgs e)
        {
            this.bdgSourceEleve.EndEdit();
            try
            {
                this.mesDonneesEF.eleves.Add(neweleve());
                this.mesDonneesEF.SaveChanges();
                MessageBox.Show("Enregistrement Validé");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'enregistrement : {ex.Message}");
                // vous pouvez également gérer l'enregistrement de manière plus spécifique selon vos besoins
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            using(var context = new autoecoleEntities())
            {
                var idASupprimer = Convert.ToInt16(txtNum.Text);
                var entiteASupprimer = context.eleves.FirstOrDefault(el => el.id == idASupprimer);

                if (entiteASupprimer != null)
                {
                    context.eleves.Remove(entiteASupprimer);
                    context.SaveChanges();
                    bdgSourceEleve.RemoveCurrent();
                    MessageBox.Show("Enregistrement supprimé");
                }
            }
        }
    }
}
