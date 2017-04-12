using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotLAND_ASKIT_serviceUser;
using System.Windows.Forms;

namespace abc
{
    public partial class Form1 : Form
    {
        Service1 serv = new Service1();

        public Form1()
        {
            InitializeComponent();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            //txt_username.Text, txt_name.Text, txt_nim.Text, txt_jurusan.Text, txt_angkatan.Text, txt_kelas.Text, txt_phone.Text, txt_alamat.Text, txt_date.Value, txt_sex.Text, imagePath.Text);
            serv.Edit(txt_username.Text, txt_name.Text, txt_nim.Text, txt_jurusan.Text, txt_angkatan.Text, txt_kelas.Text, txt_phone.Text, txt_alamat.Text, txt_date.Value, txt_sex.Text, imagePath.Text);
        }

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
    }
}
