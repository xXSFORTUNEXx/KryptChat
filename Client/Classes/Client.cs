﻿using System;
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
            int port = 14242;

            netClient.Start();
            netClient.DiscoverLocalPeers(port);
        }

        public static void Disconnect()
        {
            netClient.Disconnect("Shutdown by user"); 
        }
    }

    public enum Packet : byte
    {
        Connection
    }
}