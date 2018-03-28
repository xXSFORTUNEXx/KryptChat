using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace Client
{
    public static class IncomingData
    {
        public static void HandleIncomingData(object peer)
        {
            NetIncomingMessage incMSG;
            while ((incMSG = Program.netClient.ReadMessage()) != null)
            {
                switch (incMSG.MessageType)
                {
                    case NetIncomingMessageType.DiscoveryResponse:
                        HandleDiscoveryResponse(incMSG, Program.netClient);
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        HandleStatusChange(incMSG, Program.netClient);
                        break;

                    case NetIncomingMessageType.Data:
                        switch (incMSG.ReadByte())
                        {
                            case (byte)Packet.Connection:

                                break;
                        }
                        break;
                }
                Program.netClient.Recycle(incMSG);
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

        private static void HandleStatusChange(NetIncomingMessage incMSG, NetClient g_Client)
        {
            Program.login.slblStatus.Text = "Server Status: " + incMSG.SenderConnection.Status;
        }
    }
}
