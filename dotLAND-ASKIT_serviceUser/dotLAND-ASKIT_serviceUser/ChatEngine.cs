using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace dotLAND_ASKIT_serviceUser
{
    class ChatEngine
    {
        private List<ChatUser> connectedUsers = new List<ChatUser>();
        
        private Dictionary<string, List<ChatMessge>> incomingMessages = new Dictionary<string, List<ChatMessge>>();

        public List<ChatUser> ConnectedUsers
        {
            get { return connectedUsers; }
            set { connectedUsers = value; }
        }

        public ChatUser AddNewChatUser(ChatUser newUser)
        {
            var exists =
                from ChatUser e in this.connectedUsers
                where e.UserName == newUser.UserName
                select e;
            if (exists.Count() == 0)
            {
                this.connectedUsers.Add(newUser);
                incomingMessages.Add(newUser.UserName, new List<ChatMessge>()
                {
                    new ChatMessge() {User = newUser, Date= DateTime.Now, Messaage="Welcome to WCF Simple Chat" }

                });
                Console.WriteLine("\nNew User Connected: " + newUser.UserName);
                return newUser;

            }
            else

                return null;
        }

        public void AddNewMessage(ChatMessge newMesage)
        {
            Console.Write(newMesage.User.UserName + " Says :" + newMesage.Messaage + " at " + newMesage.Date);

            foreach (var user in this.ConnectedUsers)
            {
                if (!newMesage.User.UserName.Equals(user.UserName))
                {
                    incomingMessages[user.UserName].Add(newMesage);
                }
            }
        }

        public List<ChatMessge> GetNewMessages(ChatUser user)
        {
            List<ChatMessge> myNewMessage = incomingMessages[user.UserName];
            incomingMessages[user.UserName] = new List<ChatMessge>();

            if (myNewMessage.Count > 0)
                return myNewMessage;
            else
                return null;
        }

        public void RemoveUser(ChatUser user)
        {
            this.ConnectedUsers.RemoveAll(u => u.UserName == user.UserName);
        }
    }
}
