using MyFootballProject.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyFootballProject
{
    public partial class Login : Form
    {
        FootballProjectEntities db = new FootballProjectEntities();
        public Login()
        {
            InitializeComponent();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        public void LoginIsRemember(string email, string pas)
        {
            if (chRemember.Checked)
            {
                Properties.Settings.Default.email = email;
                Properties.Settings.Default.password = pas;
                Properties.Settings.Default.isRemember = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.email = "";
                Properties.Settings.Default.password = "";
                Properties.Settings.Default.isRemember = false;
                Properties.Settings.Default.Save();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtName.Text;
            string pas = txtPas.Text;
            Admin selectedAdmin = db.Admins.FirstOrDefault(adm => adm.AdminEmail == email);
            if (selectedAdmin != null)
            {
                if (selectedAdmin.AdminPassword == pas)
                {
                    LoginIsRemember(email, pas);
                    AdminForm adForm = new AdminForm();
                    adForm.Show();
                    this.WindowState = FormWindowState.Minimized;
                }
            }

            AllWorker selectedWorker = db.AllWorkers.FirstOrDefault(cs => cs.WorkerEmail == email);
            if (selectedWorker != null)
            {
                if (selectedWorker.WorkerPassword == extentions.HashMe(pas))
                {
                    LoginIsRemember(email, pas);
                    WorkersForm wr = new WorkersForm(selectedWorker);
                    wr.Show();
                    this.WindowState = FormWindowState.Minimized;
                }
                else
                {
                    lblError.Text = "Password doesn't correct.";
                    lblError.Visible = true;
                }
            }
            else
            {
                lblError.Text = "Email doesn't correct.";
                lblError.Visible = true;
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.isRemember)
            {
                txtName.Text = Properties.Settings.Default.email;
                txtPas.Text = Properties.Settings.Default.password;
                chRemember.Checked = true;
            }
        }
    }
}
