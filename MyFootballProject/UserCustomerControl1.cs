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
    public partial class UserCustomerControl1 : UserControl
    {
        FootballProjectEntities db = new FootballProjectEntities();
        
        public UserCustomerControl1()
        {
            InitializeComponent();
        }
        #region FillDataCustomer
        public void FillDataCustomer()
        {
            dtgCustomer.DataSource = db.Customers.Select(c => new
            {
                c.Id,
                c.Fullname,
                c.Phone,
                c.Address
            }).ToList();
            dtgCustomer.Columns[0].Visible = false;
            db.SaveChanges();
        }
        #endregion

        #region btnAddCustomer
        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            string fullname = txtFullName.Text;
            
            string address = txtAddress.Text;
            string phone = txtPhone.Text;
            string[] myArr = new string[] { fullname,  phone, address };
            if (extentions.IsEmpty(myArr, string.Empty))
            {
                Customer newCus = new Customer
                {
                    Fullname = fullname,
                    
                    Phone = Convert.ToInt32(phone),
                    Address = address
                };
                db.Customers.Add(newCus);
                db.SaveChanges();
                FillDataCustomer();
                ClearAllData();
            }
        }
        #endregion

        #region ClearAllData
        public void ClearAllData ()
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

        #region txtPhone_KeyPress
        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57 || txtPhone.Text.Length >= 10) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
        #endregion

        private void UserCustomerControl1_Load(object sender, EventArgs e)
        {
            FillDataCustomer();
        }

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
        /*
        private void btnEdit_Click(object sender, EventArgs e)
        {
            IsBtnVisible("delete");
            string cusName = txtFullName.Text;
            int cusPhone = Convert.ToInt32(txtPhone.Text);
            string cusAdress = txtAddress.Text;
            string[] myArr = new string[] { cusName, cusAdress };
            if (extentions.IsEmpty(myArr, string.Empty))
            {
                int CustId = db.Customers.First(x => x.Fullname == cusName).Id;
                selectedCustomer.Fullname = cusName;
                selectedCustomer.Phone = cusPhone;
                selectedCustomer.Address = cusAdress;       
                db.SaveChanges();
                FillDataCustomer();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            IsBtnVisible("delete");
            var res = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (res == DialogResult.Yes)
            {

                db.Customers.Remove(selectedCustomer);
                db.SaveChanges();
                FillDataCustomer();
            };
        }

        private void dtgCustomer_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            IsBtnVisible("edit");
            int cusId = (int)dtgCustomer.Rows[e.RowIndex].Cells[0].Value;
            selectedCustomer = db.Customers.First(x => x.Id == cusId);
            txtFullName.Text = selectedCustomer.Fullname;
            txtAddress.Text = selectedCustomer.Address;
            txtPhone.Text = selectedCustomer.Phone.ToString();
        }*/
    }
}
