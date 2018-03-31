using Lidgren.Network;
using System.Windows.Forms;

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
                        HandleDiscoveryResponse(incMSG);
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        HandleStatusChange(incMSG);
                        break;

                    case NetIncomingMessageType.Data:
                        switch (incMSG.ReadByte())
                        {
                            case (byte)Packet.Connection:
                                MessageBox.Show("Connected!");
                                break;
                        }
                        break;
                }
                Program.netClient.Recycle(incMSG);
            }
        }

        private static void HandleDiscoveryResponse(NetIncomingMessage incMSG)
        {
            NetOutgoingMessage outMSG = Program.netClient.CreateMessage();
            outMSG.Write((byte)Packet.Connection);
            outMSG.Write("kryptChat");
            Program.netClient.Connect(Globals.IP_ADDRESS, Globals.SERVER_PORT, outMSG);
        }

        private static void HandleStatusChange(NetIncomingMessage incMSG)
        {
            Program.login.slblStatus.Text = "Server Status: " + incMSG.SenderConnection.Status;
        }
    }
}
