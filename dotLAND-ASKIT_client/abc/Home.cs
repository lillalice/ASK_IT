using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using MetroFramework.Forms;
using dotLAND_ASKIT_serviceUser; //References dari service

namespace abc
{
    public partial class Home : MetroForm
    {
        Service1 serv = new Service1();
        Login nyan = new Login();

        private ChannelFactory<IService1> remoteFactory;
        private IService1 remoteProxy;
        private ChatUser clientUser;
        private string _username;
        private DataTable table;

        /* CONSTRUCTOR */
        public Home(string UserName)
        {
            InitializeComponent();
            this._username = UserName;
        }

        /* LOAD METHOD */
        private void Home_Load(object sender, EventArgs e)
        {
            activiti.SelectTab(0);

            welcome.Text = "Selamat datang " + _username + "!";
            

            try
            {
                remoteFactory = new ChannelFactory<IService1>("ChatConfig");
                remoteProxy = remoteFactory.CreateChannel();
                clientUser = remoteProxy.ClientConnect(_username);

                userTimer.Enabled = true;
                messageTimer.Enabled = true;
                bt_Send.Enabled = true;
                tb_message.Enabled = true;

            }
            catch (FaultException fx)
            {
                MessageBox.Show(fx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ada Error!! Ga bisa connect\nMessage:" + ex.Message,
                    "FATAL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /*--------------------------------- TAB 01 - CHAT ---------------------------------*/
        /* ONLINE USER SENDER */
        private void userTimer_Tick(object sender, EventArgs e)
        {
            try
            {

                List<ChatUser> listUsers = remoteProxy.GetAllUsers();
                onlineuser.DataSource = listUsers;
            }
            catch
            {
                MessageBox.Show("Koneksi sedang putus...");
            }
        }

        /* MESSAGES SENDER */
        private void messageTimer_Tick_1(object sender, EventArgs e)
        {
            try
            {
                List<ChatMessge> Messages = remoteProxy.GetNewMessages(clientUser);

                if (Messages != null)
                    foreach (var message in Messages)
                        InsertMessage(message);
            }
            catch
            {
                MessageBox.Show("Koneksi sedang putus...");
            }
        }

        /* SEND BUTTON */
        private void bt_Send_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tb_message.Text))
            {
                ChatMessge newMessage = new ChatMessge()
                {
                    Date = DateTime.Now,
                    Messaage = tb_message.Text,
                    User = clientUser
                };
                remoteProxy.SendNewMessage(newMessage);
                InsertMessage(newMessage);
                tb_message.Text = String.Empty;
            }
        }

        /* INSERT MESSAGE TO TEXTBOX */
        private void InsertMessage(ChatMessge message)
        {
            chat.AppendText("\n" + message.User.UserName + " says (" + message.Date + "):\n" + message.Messaage + "\n");
        }

        /* SETTINGS */
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(bt_setting, new Point(0, bt_setting.Height));
        }

        /* SEARCH USER */
        //private void cb_search_TextChanged(object sender, EventArgs e)
        //{
        //    cb_search.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //    cb_search.AutoCompleteSource = AutoCompleteSource.ListItems;

        //    cb_search.DataSource = serv.Search(cb_search.Text, cb_search.Text);

        //    //DataTable temp;
        //    //DataTable bank;

        //    //cb_search.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //    //cb_search.AutoCompleteSource = AutoCompleteSource.ListItems;

        //    //temp = DbRdRw.SqlDbRead("Select * from BankMaster", "BankMaster");

        //    //DataView dtview = new DataView(temp);
        //    //dtview.Sort = "BankName DESC";
        //    //bank = dtview.ToTable();

        //    //cb_search.DataSource = bank;
        //    //cb_search.ValueMember = "BankName";
        //    //cb_search.DisplayMember = "BankName";
        //}


        /*--------------------------------- TAB 02 - PROFILE ---------------------------------*/
        /* USER PROFILE */
        private void userProfileToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            activiti.SelectTab(1);

            serv.GetData(_username);

            DataProfile data = new DataProfile();
            lb_nama.Text = serv.GetData(_username).Nama + "\n(" + serv.GetData(_username).NIM + ")";
            lb_jurusan.Text = serv.GetData(_username).Jurusan;
            lb_angkatan.Text = Convert.ToString(serv.GetData(_username).Angkatan);
            lb_kelas.Text = serv.GetData(_username).Kelas;
            lb_alamat.Text = serv.GetData(_username).Alamat;
            lb_nope.Text = serv.GetData(_username).NoHP;
            lb_birthdate.Text = Convert.ToString(serv.GetData(_username).Date);

            Bitmap bit = new Bitmap(serv.ReadImage(_username));
            pic_user.Image = bit;

        }

        /* BACK TO HOME BUTTON */
        private void pictureBox22_Click(object sender, EventArgs e)
        {
            activiti.SelectTab(0);
        }

        /* LOG OUT */
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            serv.RemoveUser(clientUser);
            this.Close();
            nyan.MdiParent = ParentForm;
            nyan.Show();
        }
    }
}
