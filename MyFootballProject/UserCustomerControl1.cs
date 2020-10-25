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
    }
}
