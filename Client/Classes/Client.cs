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
        static NetClient netClient;
        static Krypt krypt;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            krypt = new Krypt();

            NetPeerConfiguration netConfig = new NetPeerConfiguration("kryptChat")
            {
                AutoFlushSendQueue = false,
            };

            netClient = new NetClient(netConfig);
            netClient.RegisterReceivedCallback(new System.Threading.SendOrPostCallback(IncomingData));

            Application.Run(krypt);

            netClient.Shutdown("Shutdown");
        }

        public static void IncomingData(object peer)
        {
            NetIncomingMessage incMSG;
            while ((incMSG = netClient.ReadMessage()) != null)
            {
                switch (incMSG.MessageType)
                {
                    case NetIncomingMessageType.DiscoveryResponse:
                        HandleDiscoveryResponse(incMSG, netClient);
                        break;

                    case NetIncomingMessageType.StatusChanged:

                        break;

                    case NetIncomingMessageType.Data:
                        switch (incMSG.ReadByte())
                        {
                            case (byte)Packet.Connection:
                                krypt.lblStatus.Text = "Status: Connected";
                                break;
                        }
                        break;
                }
                netClient.Recycle(incMSG);
            }
        }

        private static void HandleDiscoveryResponse(NetIncomingMessage incMSG, NetClient netClient)
        {
            string ipAddress = "10.16.0.8";
            int port = 14242;

            NetOutgoingMessage outMSG = netClient.CreateMessage();
            outMSG.Write((byte)Packet.Connection);
            outMSG.Write("kryptChat");
            netClient.Connect(ipAddress, port, outMSG);
        }

        public static void Connect()
        {
            int port = 14242;

            netClient.Start();
            netClient.DiscoverLocalPeers(port);
        }

        public static void Shutdown()
        {
            netClient.Disconnect("Shutdown by user");
 
        }
    }

    public enum Packet : byte
    {
        Connection
    }
}
