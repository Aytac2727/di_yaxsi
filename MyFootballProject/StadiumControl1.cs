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
        Stadion selectedstadium;
        public StadiumControl1()
        {
            InitializeComponent();
        }

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
        private void btnAddStadium_Click(object sender, EventArgs e)
        {
      
            string stName = txtStadium.Text;
            if(stName != string.Empty)
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

        public void ClearAllData()
        {
            foreach (var tx in this.Controls)
            {
                if(tx is TextBox)
                {
                    TextBox t = (TextBox)tx;
                    t.Text = "";
                }
            }
        }
      

        private void btnDelete_Click(object sender, EventArgs e)
        {         
            var res = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
           
            if (res == DialogResult.Yes)
            {

                db.Stadions.Remove(selectedstadium);
                db.SaveChanges();
                FillDataStadium();
            };
        }

        private void dtgStadium_RowHeaderMouseDoubleClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            btnDelete.Visible = true;
            btnEdit.Visible = true;
            btnAdd.Visible = false;
            pic1.Visible = false;
            pic2.Visible = false;
            int stadiumId = (int)dtgStadium.Rows[e.RowIndex].Cells[0].Value;
            selectedstadium = db.Stadions.First(x => x.Id == stadiumId);
            txtStadium.Text = selectedstadium.Name;

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnEdit.Visible = true;
            btnAdd.Visible = false;
            pic1.Visible = false;
            pic2.Visible = false;
            string stadName = txtStadium.Text;
            if(stadName !=string.Empty)
            {
                selectedstadium.Name = stadName;            
                db.SaveChanges();
                FillDataStadium();
            }
        }
    }
}
