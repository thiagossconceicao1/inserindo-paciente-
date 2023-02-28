using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsultorioMedico
{
    public partial class frmPesquisar : Form
    {
        public frmPesquisar()
        {
            InitializeComponent();
            txtDescricao.Enabled = false;
            rdbCodigo.Checked = false;
            rdbNome.Checked = false;
        }

        private void rdbCodigo_CheckedChanged(object sender, EventArgs e)
        {
            txtDescricao.Enabled = true;
            txtDescricao.Focus();
        }

        private void rdbNome_CheckedChanged(object sender, EventArgs e)
        {
            txtDescricao.Enabled = true;
            txtDescricao.Focus();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtDescricao.Enabled = false;
            rdbNome.Checked = false;
            rdbCodigo.Checked = false;
            txtDescricao.Clear();
            ltbItensPesquisados.Items.Clear();

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (rdbCodigo.Checked)
            {
                ltbItensPesquisados.Items.Clear();
                ltbItensPesquisados.Items.Add(txtDescricao.Text);

            }
            if (rdbNome.Checked)
            {
                //ltbItensPesquisados.Items.Clear();
                ltbItensPesquisados.Items.Add(txtDescricao.Text);
            }
        }

        private void btnTeste_Click(object sender, EventArgs e)
        {
            //Pegando o indice selecionado

            //int i = ltbItensPesquisados.SelectedIndex;

            //MessageBox.Show("O indice selecionado foi " + i);

            //Pegando o valor selecionado

            //string valor = ltbItensPesquisados.SelectedItem.ToString();

            // MessageBox.Show("O valor selecionado foi: " + valor);
        }

        private void ltbItensPesquisados_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valor = ltbItensPesquisados.SelectedItem.ToString();
        }
    }
}
