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
using System.Globalization;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;



namespace ConsultorioMedico
{
    public partial class frmPacientes : Form
    {
        //Criando variáveis para controle do menu
        const int MF_BYCOMMAND = 0X400;
        [DllImport("user32")]
        static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);
        [DllImport("user32")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32")]
        static extern int GetMenuItemCount(IntPtr hWnd);


        public void desabilitarCampos()
        {
            txtCodigo.Enabled = false;
            txtNome.Enabled = false;
            txtBairro.Enabled = false;
            txtCidade.Enabled = false;
            txtEmail.Enabled = false;
            txtEndereco.Enabled = false;
            mskCEP.Enabled = false;
            mskCPF.Enabled = false;
            mskTelefone.Enabled = false;
            cbbEstado.Enabled = false;
            btnCadastrar.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnLimpar.Enabled = false;
            txtComplemento.Enabled = false;
            txtNum.Enabled = false;
        }

        public void habilitarCampos()
        {
            txtNome.Enabled = true;
            txtBairro.Enabled = true;
            txtCidade.Enabled = true;
            txtEmail.Enabled = true;
            txtEndereco.Enabled = true;
            mskCEP.Enabled = true;
            mskCPF.Enabled = true;
            mskTelefone.Enabled = true;
            cbbEstado.Enabled = true;
            btnCadastrar.Enabled = true;
            btnLimpar.Enabled = true;
            txtComplemento.Enabled = true;
            txtNum.Enabled = true;
            txtNome.Focus();
        }

        public void limparCampos()
        {
            txtNome.Text = "";
            txtBairro.Text = "";
            txtCidade.Text = "";
            txtEmail.Text = "";
            txtEndereco.Text = "";
            mskCEP.Text = "";
            mskCPF.Text = "";
            mskTelefone.Text = "";
            cbbEstado.Text = "";
            txtNum.Clear();
            txtComplemento.Clear();
        }

        //Método para carregar a comboBox estado
        public void carregarComboBox()
        {
            cbbEstado.Items.Add("");
            cbbEstado.Items.Add("SP");
            cbbEstado.Items.Add("RJ");
            cbbEstado.Items.Add("BH");
            cbbEstado.Items.Add("BA");
            cbbEstado.Items.Add("RN");
        }

        //Construtor da classe
        public frmPacientes()
        {
            InitializeComponent();
            desabilitarCampos();
            carregarComboBox();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            frmMenuPrincipal abrir = new frmMenuPrincipal();
            abrir.Show();
            this.Hide();
        }

        private void frmPacientes_Load(object sender, EventArgs e)
        {
            IntPtr hMenu = GetSystemMenu(this.Handle, false);
            int MenuCount = GetMenuItemCount(hMenu) - 1;
            RemoveMenu(hMenu, MenuCount, MF_BYCOMMAND);

        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            habilitarCampos();
            btnNovo.Enabled = false;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            frmPesquisar abrir = new frmPesquisar();
            abrir.ShowDialog();

        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            //Executando o método verificarCampo
            verificarCampo();
            //Executar o metodo de cadastrar paciente
            cadastrarpaciente();

        }

        public void cadastrarpaciente()
        {
            MySqlCommand comm = new MySqlCommand();

            comm.CommandText = "insert into tbPaciente(nome,email,telefone,cpf,endereco," +
                "numero,cep,complemento,bairro,cidade,siglaEst) " +
                "values(@nome,@email,@telefone,@cpf,@endereco,@numero,@cep,@complemento,@bairro,@cidade,@siglaEst);";
            comm.CommandType = CommandType.Text;
            comm.Parameters.Clear();
            comm.Parameters.Add("@nome",MySqlDbType.VarChar, 100).Value = txtNome.Text;
            comm.Parameters.Add("@email", MySqlDbType.VarChar, 100).Value =txtEmail.Text;
            comm.Parameters.Add("@telefone", MySqlDbType.VarChar, 14).Value = mskTelefone.Text;
            comm.Parameters.Add("@cpf", MySqlDbType.VarChar, 14).Value = mskCPF.Text;
            comm.Parameters.Add("@endereco", MySqlDbType.VarChar, 100).Value = txtEndereco;
            comm.Parameters.Add("@numero", MySqlDbType.VarChar, 10).Value = txtNum.Text;
            comm.Parameters.Add("@cep", MySqlDbType.VarChar, 8).Value = mskCEP.Text;
            comm.Parameters.Add("@complemento", MySqlDbType.VarChar, 50).Value =txtComplemento.Text;
            comm.Parameters.Add("@bairro", MySqlDbType.VarChar, 50).Value = txtBairro.Text;
            comm.Parameters.Add("@cidade", MySqlDbType.VarChar, 50).Value = txtCidade;
            comm.Parameters.Add("@siglaEst", MySqlDbType.VarChar, 50).Value = cbbEstado.Text;

            comm.Connection = Conexao.obterConexao();

            int i = comm.ExecuteNonQuery();

            MessageBox.Show("Paciente cadastro com sucesso!!!" + i);

            Conexao.fecharConexao();
            
        }

        //Criando o método para verificar campo vazio
        public void verificarCampo()
        {
            //if (txtNome.Text == "")
            //{
            //    MessageBox.Show("Favor inserir valores");
            //}
            //else if (txtEmail.Text == "")
            //{
            //    MessageBox.Show("Favor inserir valores");
            //}

            if (txtNome.Text.Equals("") || txtEmail.Text.Equals("")
                || mskTelefone.Text.Equals("(  )      -")
                || mskCPF.Text.Equals("   .   .   -")
                || txtEndereco.Text.Equals("") || mskCEP.Text.Equals("     -")
                || txtBairro.Text.Equals("") || txtCidade.Text.Equals("")
                || cbbEstado.Text.Equals(""))
            {
                MessageBox.Show("Favor inserir valores!!!",
                    "Mensagem do Sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
                txtNome.Focus();
            }
            else
            {
                MessageBox.Show("Cadastrado com sucesso!!!",
                    "Mensagem do Sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
                desabilitarCampos();
                limparCampos();
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            limparCampos();
            txtNome.Focus();
        }
        private void mskCEP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                buscaCEP(mskCEP.Text);

            }
        }
        //Acessar o serviço de correio pelo endereço
        //https://apps.correios.com.br/SigepMasterJPA/AtendeClienteService/AtendeCliente?wsdl

        public void buscaCEP(string numCep)
        {
            WSCorreios.AtendeClienteClient ws = new WSCorreios.AtendeClienteClient();

            try
            {
                WSCorreios.enderecoERP end = ws.consultaCEP(numCep);

                txtEndereco.Text = end.end;
                txtBairro.Text = end.bairro;
                txtCidade.Text = end.cidade;
                cbbEstado.Text = end.uf;
            }
            catch (Exception)
            {
                MessageBox.Show("Insira CEP válido!!!",
                    "Mensagem do Sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
                mskCEP.Clear();
                mskCEP.Focus();

            }
        }
        //https://learn.microsoft.com/pt-br/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format

        public static bool validaEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {

                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                    RegexOptions.None, TimeSpan.FromMilliseconds(200));

                string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();

                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;

            }
            catch (ArgumentException)
            {
                return false;
            }
            try
            {   //Expressão regular

                /*
                 * O que esperava de cada trecho:
                 * [a-z0-9.]+ - parte antes do @ do e-mail, nome do usuário;
                 * @ - caractere de arroba obrigatório;
                 * [a-z0-9]+ - parte depois do @ do e-mail, nome do provedor;
                 * \. - caractere de ponto depois do nome do provedor;
                 * [a-z]+ - geralmente onde é colocado o .com;
                 * \. - caractere de ponto depois do .com, só deveria ser obrigatório caso haja por exemplo um .br ou a abreviação de qualquer outro país no final do e-mail;
                 * ([a-z]+)? - geralmente onde é colocado a abreviação do país.
                 */

                return Regex.IsMatch(email,
                  @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                  RegexOptions.IgnoreCase,
                  TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        //https://dicasdeprogramacao.com.br/algoritmo-para-validar-cpf/

        public static bool validaCPF(string vrCPF)
        {
            string valor = vrCPF.Replace(".", "");

            valor = valor.Replace("-", "");

            if (valor.Length != 11)
                return false;

            bool igual = true;

            for (int i = 1; i < 11 && igual; i++)
                if (valor[i] != valor[0])
                    igual = false;

            if (igual || valor == "12345678909")
                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(
                  valor[i].ToString());

            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else if (numeros[10] != 11 - resultado)
                return false;
            return true;
        }


        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Tab))
            {
                bool valida = validaEmail(txtEmail.Text);

                if (valida == true)
                {
                    mskCEP.Focus();
                }
                else
                {
                    MessageBox.Show("Insira e-mail válido",
                    "Mensagem do Sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
                    txtEmail.Clear();
                    txtEmail.Focus();
                }
            }
        }

        private void mskCPF_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bool valida = validaCPF(mskCPF.Text);

                if (valida == true)
                {
                    mskCEP.Focus();
                }
                else
                {
                    MessageBox.Show("Insira CPF válido",
                   "Mensagem do Sistema",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error,
                   MessageBoxDefaultButton.Button1);
                    mskCPF.Clear();
                    mskCPF.Focus();
                }

            }
        }
    }

}
