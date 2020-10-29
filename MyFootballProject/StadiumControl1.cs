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
    public partial class StadiumControl1 : UserControl
    {
        FootballProjectEntities db = new FootballProjectEntities();
        
        public StadiumControl1()
        {
            InitializeComponent();
        }

        #region FillDataStadium
        public void FillDataStadium()
        {
            dtgStadium.DataSource = db.Stadions.Select(st => new
            {
                st.Id,
                st.Name            
            }).ToList();
            dtgStadium.Columns[0].Visible = false;
            db.SaveChanges();
        }
        #endregion

        #region ClearAllData
        public void ClearAllData()
        {
            foreach (var tx in this.Controls)
            {
                if (tx is TextBox)
                {
                    TextBox t = (TextBox)tx;
                    t.Text = "";
                }
            }
        }
        #endregion

        #region btnAdd
        private void btnAddStadium_Click(object sender, EventArgs e)
        {

            string stName = txtStadium.Text;
            if (stName != string.Empty)
            {
                Stadion newSt = new Stadion
                {
                    Name = stName
                };
                db.Stadions.Add(newSt);
                db.SaveChanges();
                FillDataStadium();
                ClearAllData();
            }

        }
        #endregion

      /*  #region RowHeaderMouseDoubleClick
        private void dtgStadium_RowHeaderMouseDoubleClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            IsBtnVisible("edit");
            int stadiumId = (int)dtgStadium.Rows[e.RowIndex].Cells[0].Value;
            selectedstadium = db.Stadions.First(x => x.Id == stadiumId);
            txtStadium.Text = selectedstadium.Name;

        }
        #endregion

       

        #region btnDelete
        private void btnDelete_Click(object sender, EventArgs e)
        {
            IsBtnVisible("delete");
            var res = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
           
            if (res == DialogResult.Yes)
            {

                db.Stadions.Remove(selectedstadium);
                db.SaveChanges();
                FillDataStadium();
            };
        }
        #endregion

        #region btnEdit
        private void btnEdit_Click(object sender, EventArgs e)
        {
            IsBtnVisible("delete");
            string stadName = txtStadium.Text;
            if (stadName != string.Empty)
            {
                selectedstadium.Name = stadName;
                db.SaveChanges();
                FillDataStadium();
            }
        }
        #endregion*/

        #region IsBtnVisible
        public void IsBtnVisible(string txt)
        {
            if(txt == "edit")
            {
                btnDelete.Visible = true;
                btnEdit.Visible = true;
                btnAdd.Visible = false;
                pic1.Visible = false;
                pic2.Visible = false;
            }
            else
            {
                btnDelete.Visible = false;
                btnEdit.Visible = false;
                btnAdd.Visible = true;
                pic1.Visible = true;
                pic2.Visible = true;
            }
        }
        #endregion

        #region Load
         private void StadiumControl1_Load(object sender, EventArgs e)
        {
            FillDataStadium();
        }
         #endregion
     
    }
}
