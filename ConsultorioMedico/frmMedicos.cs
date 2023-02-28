﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ConsultorioMedico
{
    public partial class frmMedicos : Form
    {
        //Criando variáveis para controle do menu
        const int MF_BYCOMMAND = 0X400;
        [DllImport("user32")]
        static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);
        [DllImport("user32")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32")]
        static extern int GetMenuItemCount(IntPtr hWnd);

        public frmMedicos()
        {
            InitializeComponent();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            frmMenuPrincipal abrir = new frmMenuPrincipal();
            abrir.Show();
            this.Hide();
        }

        private void frmMedicos_Load(object sender, EventArgs e)
        {
            IntPtr hMenu = GetSystemMenu(this.Handle, false);
            int MenuCount = GetMenuItemCount(hMenu) - 1;
            RemoveMenu(hMenu, MenuCount, MF_BYCOMMAND);
            //executando o método carrega especialidades
            carregaCombo();
            
        }

        //criando o método carrega especialidades
        public void carregaCombo()
        {
            cbbEspecialidades.Items.Add("Clinica Médica");
            cbbEspecialidades.Items.Add("Cardiologista");
            cbbEspecialidades.Items.Add("Pediatria");
            cbbEspecialidades.Items.Add("Psiquiatria");
            cbbEspecialidades.Items.Add("Urologia");
        }

        private void btnCadEsp_Click(object sender, EventArgs e)
        {
            frmEspecialidades abrir = new frmEspecialidades();
            abrir.ShowDialog();         

        }
    }
}
