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
        AllWorker selectedWorker;
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
                w.WorkerEmail,
                w.WorkerPassword
                
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

        #region btnAddCustomer
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
        #endregion

        #region IsBtnVisible
        public void IsBtnVisible(string txt)
        {
            if (txt == "edit")
            {
                btnDelete.Visible = true;
                btnEdit.Visible = true;
                btnAdd.Visible = false;
                panel5.Visible = false;
            }
            else
            {
                btnDelete.Visible = false;
                btnEdit.Visible = false;
                btnAdd.Visible = true;
                panel5.Visible = true;
            }
        }
        #endregion

        #region btnEditWorker
        private void btnEdit_Click(object sender, EventArgs e)
        {
            IsBtnVisible("delete");
            string workerfirstName = txtFirstName.Text;
            string workerlastname = txtLastname.Text;
            string workerEmail = txtEmail.Text;
            string workerPas = txtPassword.Text;
            string[] myArr = new string[] { workerfirstName, workerlastname, workerEmail, workerPas };
            if (extentions.IsEmpty(myArr,string.Empty))
            {
                selectedWorker.Firstname = workerfirstName;
                selectedWorker.Lastname = workerlastname;
                selectedWorker.WorkerEmail = workerEmail;
                selectedWorker.WorkerPassword = workerPas;
                db.SaveChanges();
                FillDataWorker();
            }
        }
        #endregion

        #region btnDeleteWorker
        private void btnDelete_Click(object sender, EventArgs e)
        {
            IsBtnVisible("delete");
            var res = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (res == DialogResult.Yes)
            {

                db.AllWorkers.Remove(selectedWorker);
                db.SaveChanges();
                FillDataWorker();
            };
        }
        #endregion

        #region dtgWorker_RowHeaderMouseDoubleClick
        private void dtgWorker_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            IsBtnVisible("edit");
            int workerId = (int)dtgWorker.Rows[e.RowIndex].Cells[0].Value;
            selectedWorker = db.AllWorkers.First(x => x.Id == workerId);
            txtEmail.Text = selectedWorker.WorkerEmail;
            txtFirstName.Text = selectedWorker.Firstname;
            txtLastname.Text = selectedWorker.Lastname;
            txtPassword.Text = selectedWorker.WorkerPassword;
        }

        #endregion

        #region Load
        private void WorkerControl1_Load(object sender, EventArgs e)
        {
            FillDataWorker();
        }
        #endregion
    }
}
