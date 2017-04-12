using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace dotLAND_ASKIT_serviceUser
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Service1 : IService1
    {
        string ConnectionStringSA = "Data Source=ALICE002;Initial Catalog=AskIT;Persist Security Info=True;User ID=sa;Password=root";
        private DataTable table;

        public bool myLogin(string userID, string userPass)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConnectionStringSA);
                string query = "select userID,userPassword from [dbo].[Login] where userID= '" + userID + "' AND userPassword= '" + userPass + "'";
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    con.Close();
                    return true;
                }
                else
                {
                    con.Close();
                    return false;
                }                
            }
            catch (Exception ex)
            {                
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void SignUp(string query)
        {
            using (SqlConnection con = new SqlConnection(ConnectionStringSA))
            using (SqlCommand com = con.CreateCommand())
            {
                com.CommandText = query;
                
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }            
        }

        public void Signup2(string ID, string Nama, string NIM, string Jurusan, string Angkatan, string Kelas, string Phone, string Alamat, DateTime Birthdate, string Sex, string file)
        {
            FileStream FS = new FileStream(file, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] img = new byte[FS.Length];
            FS.Read(img, 0, Convert.ToInt32(FS.Length));
            FS.Close();

            using (SqlConnection con = new SqlConnection(ConnectionStringSA))
            using (SqlCommand com = con.CreateCommand())
            {
                //byte[] photo = GetPhoto(photoFilePath);
                com.CommandText = "INSERT INTO dbo.DataUser(userID,userNama,userNIM,userJurusan,userAngkatan,userKelas,userPhone,userAlamat,userTanggalLahir,userSex,userPicture)" +
                    " VALUES (@user,@nama,@nim,@jurusan,@angkatan,@kelas,@phone,@alamat,@birthdate,@sex,@photo)";
                com.Parameters.AddWithValue("@user", ID);
                com.Parameters.AddWithValue("@nama", Nama);
                com.Parameters.AddWithValue("@nim", NIM);
                com.Parameters.AddWithValue("@jurusan", Jurusan);
                com.Parameters.AddWithValue("@angkatan", Angkatan);
                com.Parameters.AddWithValue("@kelas", Kelas);
                com.Parameters.AddWithValue("@phone", Phone);
                com.Parameters.AddWithValue("@alamat", Alamat);
                com.Parameters.AddWithValue("@birthdate", Birthdate);
                com.Parameters.AddWithValue("@sex", Sex);
                SqlParameter prm = new SqlParameter("@photo", SqlDbType.VarBinary, img.Length, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, img);
                com.Parameters.Add(prm);
                //com.Parameters.Add("@photo", SqlDbType.Image, photo.Length).Value = photo;

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }



        public DataProfile GetData(string userID)
        {
            DataProfile profile = new DataProfile();

            SqlConnection con = new SqlConnection(ConnectionStringSA);
            SqlCommand cmd = new SqlCommand(String.Format("SELECT userNama,userNIM,userJurusan,userAngkatan,userKelas,userPhone,userAlamat,userTanggalLahir"
                + " FROM [dbo].[DataUser] WHERE userID = '{0}'", userID), con);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                profile.Nama = reader.GetString(0);
                profile.NIM = reader.GetString(1);
                profile.Jurusan = reader.GetString(2);
                profile.Angkatan = reader.GetInt32(3);
                profile.Kelas = reader.GetString(4);
                profile.NoHP = reader.GetString(5);
                profile.Alamat = reader.GetString(6);
                profile.Date = reader.GetDateTime(7);
            }
            con.Close();
            return profile;
        }

        public MemoryStream ReadImage(string userID)
        {
            SqlConnection con = new SqlConnection(ConnectionStringSA);
            SqlCommand cmd = new SqlCommand(String.Format("SELECT userPicture FROM [dbo].[DataUser]"+
                " WHERE userID = '{0}'", userID), con);

            con.Open();

            byte[] img = (byte[])cmd.ExecuteScalar();
            MemoryStream str = new MemoryStream();
            str.Write(img, 0, img.Length);
            
            con.Close();

            return str;
        }

        /******************* CHAT SERVER *******************/
        private ChatEngine mainEngine = new ChatEngine();
        public ChatUser ClientConnect(string userName)
        {
            try
            {
                return mainEngine.AddNewChatUser(new ChatUser() { UserName = userName });
            }
            catch (FaultException fx)
            {
                Console.WriteLine(fx.Message);
                return null;
            }
        }

        public List<ChatUser> GetAllUsers()
        {
            try
            {
                return mainEngine.ConnectedUsers;
            }
            catch (FaultException fx)
            {
                Console.WriteLine(fx.Message);
                return null;
            }
        }

        public List<ChatMessge> GetNewMessages(ChatUser user)
        {
            try
            {
                return mainEngine.GetNewMessages(user);
            }
            catch (FaultException fx)
            {
                Console.WriteLine(fx.Message);
                return null;
            }
        }

        public void RemoveUser(ChatUser user)
        {   
            try
            {
                mainEngine.RemoveUser(user);
            }
            catch (FaultException fx)
            {
                Console.WriteLine(fx.Message);
            }
        }

        public void SendNewMessage(ChatMessge newMessage)
        {
            try
            {
                mainEngine.AddNewMessage(newMessage);
            }
            catch (FaultException fx)
            {
                Console.WriteLine(fx.Message);
            }
        }
    }
}
