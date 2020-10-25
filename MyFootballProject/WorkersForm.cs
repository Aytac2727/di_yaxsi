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
    
    public partial class WorkersForm : Form
    {
        FootballProjectEntities db = new FootballProjectEntities();
        private AllWorker ActiveWorker;
        
      
        public WorkersForm(AllWorker wor)
        {
            ActiveWorker = wor;
            InitializeComponent();
        }

        public void FillDataRezerv()
        {
            dtgRezervation.DataSource = db.Rezervs.Select(re => new
            {
                re.Id,
                Customer_Name = re.Customer.Fullname,
                Stadium_Name = re.Stadion.Name,      
                Room_Count = re.Room.Name,
                re.Date,
                re.DateFrom,
                re.DateTo,
                re.Price

            }).ToList();
            dtgRezervation.Columns[0].Visible = false;
            db.SaveChanges();
        }
      
        public void FillComboCustomer()
        {
            cmbCustomer.Items.AddRange(db.Customers.Select(c => c.Fullname).ToArray());
        }

        public void FillComboRoom()
        {
            cmbRoom.Items.AddRange(db.Rooms.Select(c => c.Name).ToArray());
        }

        public void FillComboStadium()
        {
            cmbStadium.Items.AddRange(db.Stadions.Select(c => c.Name).ToArray());
        }

        private void WorkersForm_Load(object sender, EventArgs e)
        {
            FillComboCustomer();
            FillComboStadium();
            FillComboRoom();
            FillDataRezerv();
        }

        private void btnAddRezerv_Click(object sender, EventArgs e)
        {

            string cusName = cmbCustomer.Text;         
            string stadiumName = cmbStadium.Text;
           
            decimal price = numPrice.Value;
            DateTime dateF = dateFrom.Value;
            DateTime dateT = dateTo.Value;
            DateTime dateFull = dateTime.Value;
            string[] myArr = new string[] { cusName, stadiumName};
            if(extentions.IsEmpty(myArr, string.Empty))
            {
                string RName = cmbRoom.Text;
                Rezerv selRoom = db.Rezervs.First(m => m.Room.Name == RName);
                if (!checkRoom.Items.Contains(RName))
                {
                    checkRoom.Items.Add(RName, true);
                }
                int stId = db.Stadions.First(s => s.Name == stadiumName).Id;
                int cusId = db.Customers.First(c => c.Fullname == cusName).Id;
                Rezerv newRezerv = new Rezerv()
                {
                    RoomId = selRoom.Id,
                    StadiumId = stId,
                    CustomerId = cusId,
                    Date = dateFull,
                    DateFrom = dateF,
                    DateTo = dateT,
                    Price = price
                };
                db.Rezervs.Add(newRezerv);
                db.SaveChanges();
                FillDataRezerv();
                AddRoom();
            }
        }

        private void checkRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selected = checkRoom.SelectedIndex;
            if (selected != -1)
            {
                checkRoom.Items.RemoveAt(selected);
            }
        }
        public void AddRoom()
        {
            for (int i = checkRoom.Items.Count - 1; i >= 0; i--)
            {
                string RName = checkRoom.Items[i].ToString();
                Room selectedRoom = db.Rooms.FirstOrDefault(ro => ro.Name == RName);
                int roomId;
                if (selectedRoom != null)
                {
                    roomId = selectedRoom.Id;
                    db.Rezervs.Add(new Rezerv()
                    {
                        
                        RoomId = roomId
                    });
                }       
                db.SaveChanges();
                
            }
        }

        private void cmbRoom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                string roomName = cmbRoom.Text;
                if(!checkRoom.Items.Contains(roomName))
                {
                    checkRoom.Items.Add(roomName);
                }
            }
        }

        /*public void AddCheckedListFill()
        {
            string RName = cmbRoom.Text;
            Rezerv selRoom = db.Rezervs.First(m => m.Room.Name == RName);
            if (!checkRoom.Items.Contains(RName))
            {
                checkRoom.Items.Add(RName, true);
            }
        }*/
    }
}
