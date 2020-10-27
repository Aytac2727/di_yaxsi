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
    public partial class RoomControl1 : UserControl
    {
        FootballProjectEntities db = new FootballProjectEntities();
        Room selectedRoom;
        public RoomControl1()
        {
            InitializeComponent();
        }

        #region FillDataRoom
        public void FillDataRoom()
        {
            dtgRoom.DataSource = db.Rooms.Select(x => new
            {
                x.Id,
                x.Name,
                x.Count
            }).ToList();
            dtgRoom.Columns[0].Visible = false;
            db.SaveChanges();
        }
        #endregion

        #region ClearAllData
        public void ClearAllData ()
        {
            foreach (var txt in this.Controls)
            {
                if (txt is TextBox)
                {
                    TextBox tx = (TextBox)txt;
                    tx.Text = "";
                }
            }

        }
        #endregion

        #region btnAddRoom
        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            string roomName = txtRoomName.Text;
            int roomCount = Convert.ToInt32(txtRoomCount.Text);
            Room newRoom = new Room
            {
                Name = roomName,
                Count = roomCount
            };
            db.Rooms.Add(newRoom);
            db.SaveChanges();
            FillDataRoom();
            ClearAllData();
        }
        #endregion

        #region Load 
        private void RoomControl1_Load(object sender, EventArgs e)
        {
            FillDataRoom();
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

        #region btnEdit
        private void btnEdit_Click(object sender, EventArgs e)
        {
            IsBtnVisible("delete");
            string roomName = txtRoomName.Text;
      
            if (roomName != string.Empty)
            {
                selectedRoom.Name = roomName;          
                db.SaveChanges();
                FillDataRoom();
            }
        }
        #endregion

        #region btnDelete
        private void btnDelete_Click(object sender, EventArgs e)
        {
            IsBtnVisible("delete");
            var res = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (res == DialogResult.Yes)
            {

                db.Rooms.Remove(selectedRoom);
                db.SaveChanges();
                FillDataRoom();
            };
        }
        #endregion

        #region RowHeaderMouseDoubleClick
        private void dtgRoom_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            IsBtnVisible("edit");
            int roomId = (int)dtgRoom.Rows[e.RowIndex].Cells[0].Value;
            selectedRoom = db.Rooms.First(x => x.Id == roomId);
            txtRoomName.Text = selectedRoom.Name;
           
        }
        #endregion
    }
}
