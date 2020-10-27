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
                Room_Name = re.Room.Name,
                re.Date,
                re.DateFrom,
                re.DateTo,
                re.Price

            }).ToList();
            dtgRezervation.Columns[0].Visible = false;
            db.SaveChanges();
            /*for (var i = 0; i < dtgRezervation.Rows.Count; i++)
            {
                DateTime dtTime = (DateTime)dtgRezervation.Rows[i].Cells[dtgRezervation.Columns.Count - 3].Value;
                DateTime dtFrom = (DateTime)dtgRezervation.Rows[i].Cells[dtgRezervation.Columns.Count - 3].Value;
                DateTime dtTo = (DateTime)dtgRezervation.Rows[i].Cells[dtgRezervation.Columns.Count - 2].Value;
                string Sta = dtgRezervation.Rows[i].Cells[2].Value.ToString();
            }*/
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
            string roomName = cmbRoom.Text;
            decimal price = numPrice.Value;
            DateTime dateF = dateFrom.Value;
            DateTime dateT = dateTo.Value;
            DateTime dateFull = dateTime.Value;
            string[] myArr = new string[] { cusName, stadiumName};
            if(extentions.IsEmpty(myArr, string.Empty))
            {   
                    int roId = db.Rooms.First(r => r.Name == roomName).Id;
                    int stId = db.Stadions.First(s => s.Name == stadiumName).Id;
                    int cusId = db.Customers.First(c => c.Fullname == cusName).Id;
                
                    Rezerv newRezerv = new Rezerv()
                    {
                        RoomId = roId,
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

            }
        }

     /*  
        public void AddRoom(int cusId)
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
                        CustomerId = cusId,
                        RoomId = roomId
                    });
                }       
                db.SaveChanges();
                
            }
        }

        public void AddCheckedListFill()
        {
            string RName = cmbRoom.Text;
            Rezerv selRoom = db.Rezervs.First(m => m.Room.Name == RName);
            if (!checkRoom.Items.Contains(RName))
            {
                checkRoom.Items.Add(RName, true);
            }
        }

        private void cmbRoom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                string roomName = cmbRoom.Text;
                if (!checkRoom.Items.Contains(roomName))
                {
                    checkRoom.Items.Add(roomName);
                }              
            }
           
        }
        public void AddCusWithRoom(int cusId)
        {
            for (int i = checkRoom.Items.Count-1; i >=0; i--)
            {
                string roomName = checkRoom.Items[i].ToString();
                Room selectedRoom = db.Rooms.FirstOrDefault(ro => ro.Name == roomName);
                int roomId = selectedRoom.Id;
                db.Customer_To_Room.Add(new Customer_To_Room()
                {
                    CustomerId = cusId,
                    RoomId = roomId
                });
                db.SaveChanges();
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
     */

        private void picExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
