using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lidgren.Network;

namespace Client
{
    static class Program
    {
        public static NetClient netClient;
        public static Login login;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            login = new Login();

            NetPeerConfiguration netConfig = new NetPeerConfiguration("kryptChat")
            {
                AutoFlushSendQueue = false,
            };

            netClient = new NetClient(netConfig);
            netClient.RegisterReceivedCallback(new System.Threading.SendOrPostCallback(IncomingData.HandleIncomingData));

            Application.Run(login);

            netClient.Shutdown("Shutdown");
        }
        public static void Connect()
        {
            int port = Globals.SERVER_PORT;

            netClient.Start();
            netClient.DiscoverLocalPeers(port);
        }

        public static void Disconnect()
        {
            netClient.Disconnect("Shutdown by user"); 
        }
    }

    public static class Globals
    {
        public const byte NO = 0;
        public const byte YES = 1;
        public const int MAX_ACCOUNTS = 5;
        public const string IP_ADDRESS = "10.16.0.8";
        public const int SERVER_PORT = 14242;
        public const float CONNECTION_TIMEOUT = 5.0f;
    }

    public enum Packet : byte
    {
        Connection,
        Registration,
        ErrorMessage,
        Login
    }
}
