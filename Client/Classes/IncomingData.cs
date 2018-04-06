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
                                //MessageBox.Show("Connected!");
                                break;

                            case (byte)Packet.ErrorMessage:
                                HandleErrorMessage(incMSG);
                                break;

                            case (byte)Packet.ActivateAccount:
                                HandleActivateAccount(incMSG);
                                break;

                            case (byte)Packet.Login:
                                HandleLogin(incMSG);
                                break;

                            case (byte)Packet.WhosOnline:
                                HandleWhosOnline(incMSG);
                                break;

                            case (byte)Packet.Message:
                                HandleMessage(incMSG);
                                break;
                        }
                        break;
                }
                Program.netClient.Recycle(incMSG);
            }
        }

        public static void HandleMessage(NetIncomingMessage incMSG)
        {
            string message = incMSG.ReadString();

            Program.krypt.txtGlobalChat.AppendText("\n" + message);
        }

        private static void HandleWhosOnline(NetIncomingMessage incMSG)
        {
            Program.krypt.lstOnline.Items.Clear();
            Program.krypt.lstOnline.Items.Add("Online:");
            for (int i = 0; i < Globals.MAX_ACCOUNTS; i++)
            {
                Program.accounts[i].ID = incMSG.ReadVariableInt32();
                Program.accounts[i].Name = incMSG.ReadString();
                Program.krypt.lstOnline.Items.Add(Program.accounts[i].Name);
            }

        }

        private static void HandleLogin(NetIncomingMessage incMSG)
        {
            Program.tempName = incMSG.ReadString();
            Program.krypt = new Krypt();
            Program.krypt.Show();
        }

        private static void HandleActivateAccount(NetIncomingMessage incMSG)
        {
            Program.tempName = incMSG.ReadString();
            Program.tempSlot = incMSG.ReadVariableInt32();
            Activate activate = new Activate();
            activate.ShowDialog();
        }

        private static void HandleErrorMessage(NetIncomingMessage incMSG)
        {
            string title = incMSG.ReadString();
            string message = incMSG.ReadString();

            MessageBox.Show(message, title);
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
