using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.Data.SqlClient;
using System.IO;
using System.ServiceModel;
using dotLAND_ASKIT_serviceUser; //References dari service

namespace abc
{
    public partial class Login : MetroForm
    {
        Service1 serv = new Service1();

        /* CONSTRUCTOR */
        public Login()
        {
            InitializeComponent();
        }

        /* LOAD METHOD */
        private void Login_Load(object sender, EventArgs e)
        {
            login_tab.SelectTab(0);
        }

        /*--------------------------------- TAB 01 - LOGIN ---------------------------------*/
        /* LOGIN BUTTON */
        private void bt_login_Click(object sender, EventArgs e)
        {
            try
            {
                if (serv.myLogin(txt_userID.Text, txt_pass.Text) == true)
                {
                    Home nyan = new Home(txt_userID.Text);

                    nyan.MdiParent = ParentForm;
                    this.Hide();
                    nyan.Show();
                }
                else
                {
                    errorProvider1.SetError(txt_userID, "Username atau password salah!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Koneksi anda sedang putus", "Koneksi Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                MessageBox.Show(ex.Message);
            }
        }

        /* ERROR PROVIDER CLEANER */
        private void txt_userID_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }
        private void txt_pass_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        /* SIGN UP */
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            login_tab.SelectTab(1);
        }


        /*--------------------------------- TAB 02 - SIGN UP 1 ---------------------------------*/
        /* LOGIN TABLE SIGN UP */
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "INSERT INTO dbo.Login(userID,userPassword) VALUES ('" + txt_username.Text + "','" + txt_password.Text + "')";
                try
                {
                    serv.SignUp(query);
                    login_tab.SelectTab(2);
                }
                catch (NoNullAllowedException nx)
                {
                    errorProvider1.SetError(bt_signup1, "Data yang diisi tidak boleh kosong!");
                }
                catch (SqlException sx)
                {
                    errorProvider1.SetError(txt_username, "Username sudah digunakan!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Koneksi anda sedang putus", "Koneksi Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        /*--------------------------------- TAB 03 - SIGN UP 2 ---------------------------------*/
        /* IMAGE UPLOADER */
        private void imageUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            // image filters
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box
                imageUpload.Image = new Bitmap(open.FileName);
                // image file path
                imagePath.Text = open.FileName;
            }
        }

        /* BACK BUTTON */
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM dbo.Login WHERE userID='" + txt_username.Text + "'";

            try
            {
                serv.SignUp(query);
                login_tab.SelectTab(1);
            }
            catch
            {
                MessageBox.Show("Koneksi anda sedang putus", "Koneksi Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /* SIGN UP BUTTON */
        private void bt_signup2_Click(object sender, EventArgs e)
        {
            try
            {
                serv.Signup2(txt_username.Text, txt_name.Text, txt_nim.Text, txt_jurusan.Text, txt_angkatan.Text,
                    txt_kelas.Text, txt_phone.Text, txt_alamat.Text, txt_date.Value, txt_sex.Text, imagePath.Text);
                
                MessageBox.Show("Registrasi Berhasil");

                Home nyan = new Home(txt_username.Text);
                nyan.MdiParent = ParentForm;
                this.Hide();
                nyan.Show();
            }
            catch (SqlException sx)
            {
                MessageBox.Show(sx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Koneksi anda sedang putus", "Koneksi Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                MessageBox.Show(ex.Message);
            }
        }
    }
}
