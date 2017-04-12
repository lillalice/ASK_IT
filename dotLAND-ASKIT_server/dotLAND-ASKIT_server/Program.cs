using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using dotLAND_ASKIT_serviceUser;
using System.Net;

namespace dotLAND_ASKIT_server
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost hostObj = null;

            try
            {
                hostObj = new ServiceHost(typeof(Service1));
                hostObj.Open();

                Console.WriteLine("Alhamdulillah... Server is ready ^^");                
                Console.ReadLine();
                hostObj.Close();
            }
            catch (Exception ex)
            {
                hostObj = null;
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
