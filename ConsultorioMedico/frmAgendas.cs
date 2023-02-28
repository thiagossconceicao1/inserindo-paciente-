using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
//Importando a classe de conexão do MySQL
using MySql.Data.MySqlClient;

namespace ConsultorioMedico
{
    public partial class frmAgendas : Form
    {

        //Criando variáveis para controle do menu
        const int MF_BYCOMMAND = 0X400;
        [DllImport("user32")]
        static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);
        [DllImport("user32")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32")]
        static extern int GetMenuItemCount(IntPtr hWnd);
        public frmAgendas()
        {
            InitializeComponent();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            frmMenuPrincipal abrir = new frmMenuPrincipal();
            abrir.Show();
            this.Hide();
        }

        private void frmAgendas_Load(object sender, EventArgs e)
        {
            IntPtr hMenu = GetSystemMenu(this.Handle, false);
            int MenuCount = GetMenuItemCount(hMenu) - 1;
            RemoveMenu(hMenu, MenuCount, MF_BYCOMMAND);

        }

        private void btnTestarConexao_Click(object sender, EventArgs e)
        {
            //instanciando a classe de conexão para o banco de dados
            MySqlConnection con = new MySqlConnection();
            
            //inserir a conexão com o banco de dados
            con.ConnectionString = "Server=localhost;Port=3306;Database=dbClinicaMedica;Uid=senac;Pwd=123456";

            try { 
            //abrir o banco de dados
            con.Open();
            MessageBox.Show("Banco de dados aberto");
            }
            catch (MySqlException) {
                MessageBox.Show("Banco de dados não conectado.");
            }
            //fechar o banco de dados
            con.Close();
            MessageBox.Show("Banco de dados fechado");

        }
    }
}
