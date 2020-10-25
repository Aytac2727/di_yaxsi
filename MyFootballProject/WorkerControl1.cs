using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyFootballProject.models;

namespace MyFootballProject
{
    public partial class WorkerControl1 : UserControl
    {
        FootballProjectEntities db = new FootballProjectEntities();
        public WorkerControl1()
        {
            InitializeComponent();
        }

        #region FillDataWorker
        public void FillDataWorker()
        {
            dtgWorker.DataSource = db.AllWorkers.Select(w => new
            {
                w.Id,
                w.Firstname,
                w.Lastname,   
                w.WorkerEmail
                
            }).ToList();
            dtgWorker.Columns[0].Visible = false;
            db.SaveChanges();
        }
        #endregion

        #region ClearAllData
        public void ClearAllData()
        {
            foreach (var txtBox in this.Controls)
            {
                if (txtBox is TextBox)
                {
                    TextBox tx = (TextBox)txtBox;
                    tx.Text = "";
                }
            }
        }
        #endregion
        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            string firstname = txtFirstName.Text;
            string lastname = txtLastname.Text;
            string email = txtEmail.Text;
            string pas = txtPassword.Text;
            string[] myArr = new string []{ firstname, lastname, email, pas };
            if(extentions.IsEmpty(myArr, string.Empty))
            {
                if(email.Contains("@") || email.Contains("@gmail.com")|| email.Contains("@mail.ru")) {
                    AllWorker newWorker = new AllWorker
                    {
                        Firstname = firstname,
                        Lastname = lastname,
                        WorkerEmail = email,
                        WorkerPassword = extentions.HashMe(pas)
                    };
                    db.AllWorkers.Add(newWorker);
                    db.SaveChanges();
                    FillDataWorker();
                    ClearAllData();
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Please, write true email!";
                }
               
            }
        }
    }
}
