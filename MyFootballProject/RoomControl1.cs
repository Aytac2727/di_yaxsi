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

    }
}
