using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace dotLAND_ASKIT_serviceUser
{
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface IService1
    {
        [OperationContract]
        bool myLogin(string userID, string password);

        [OperationContract]
        void SignUp(string query);

        [OperationContract]
        ChatUser ClientConnect(string userName);

        [OperationContract]
        List<ChatMessge> GetNewMessages(ChatUser user);
        
        [OperationContract]
        void SendNewMessage(ChatMessge newMessage);

        [OperationContract]
        List<ChatUser> GetAllUsers();
        
        [OperationContract]
        void RemoveUser(ChatUser user);

        [OperationContract]
        void Signup2(string ID, string Nama, string NIM, string Jurusan, string Angkatan, string Kelas, string Phone, string Alamat, DateTime Birthdate, string Sex, string file);

        [OperationContract]
        DataProfile GetData(string userID);

        [OperationContract]
        MemoryStream ReadImage(string userID);

        //[OperationContract]
        //void Edit(string ID, string Nama, string NIM, string Jurusan, string Angkatan, string Kelas, string Phone, string Alamat, DateTime Birthdate, string Sex, string file);
    }

    [DataContract]
    public class ChatMessge
    {
        private ChatUser user;
        private string message;
        private DateTime date;

        [DataMember]
        public ChatUser User
        {
            get { return user; }
            set { user = value; }
        }

        [DataMember]
        public string Messaage
        {
            get { return message; }
            set { message = value; }
        }

        [DataMember]
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
    }

    [DataContract]
    public class ChatUser
    {
        private string userName, _nama, ipAddress, hostName;

        [DataMember]
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        [DataMember]
        public string Nama
        {
            get { return _nama; }
            set { _nama = value; }
        }

        [DataMember]
        public string IpAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }

        [DataMember]
        public string HostName
        {
            get { return hostName; }
            set { hostName = value; }
        }
        public override string ToString()
        {
            {
                return this.UserName;
            }
        }
    }

    [DataContract]
    public class DataProfile
    {
        private string nama;
        private string nim;
        private string jurusan;
        private int angkatan;
        private string kelas;
        private string alamat;
        private string nohp;
        private DateTime date;

        [DataMember(Order = 1)]
        public string Nama
        {
            get { return nama; }
            set { nama = value; }
        }

        [DataMember(Order = 2)]
        public string NIM
        {
            get { return nim; }
            set { nim = value; }
        }

        [DataMember(Order = 3)]
        public string Jurusan
        {
            get { return jurusan; }
            set { jurusan = value; }
        }

        [DataMember(Order = 4)]
        public int Angkatan
        {
            get { return angkatan; }
            set { angkatan = value; }
        }

        [DataMember(Order = 5)]
        public string Kelas
        {
            get { return kelas; }
            set { kelas = value; }
        }

        [DataMember(Order = 6)]
        public string Alamat
        {
            get { return alamat; }
            set { alamat = value; }
        }

        [DataMember(Order = 7)]
        public string NoHP
        {
            get { return nohp; }
            set { nohp = value; }
        }

        [DataMember(Order = 8)]
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
    }
}
