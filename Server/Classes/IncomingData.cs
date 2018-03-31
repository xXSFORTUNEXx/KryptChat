using System;
using Lidgren.Network;
using static System.Console;
using static System.Threading.Thread;
using System.Data.SqlClient;

namespace Server
{
    public class IncomingData
    {
        public void HandleIncomingData()
        {
            NetIncomingMessage incMSG;

            if ((incMSG = Program.netServer.ReadMessage()) != null)
            {
                switch (incMSG.MessageType)
                {
                    case NetIncomingMessageType.DiscoveryRequest:
                        HandleDiscoveryRequest(incMSG);
                        break;

                    case NetIncomingMessageType.ConnectionApproval:
                        HandleConnectionApproval(incMSG);
                        break;

                    case NetIncomingMessageType.Data:
                        switch (incMSG.ReadByte())
                        {
                            case (byte)Packet.Registration:
                                HandleRegistrationRequest(incMSG);
                                break;
                        }
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        HandleStatusChange(incMSG);
                        break;
                }
            }
            Program.netServer.Recycle(incMSG);
        }
        private void HandleRegistrationRequest(NetIncomingMessage incMSG)
        {
            string name = incMSG.ReadString();
            string pass = incMSG.ReadString();
            string email = incMSG.ReadString();

            if (!MSSQL.AccountExist(name))
            {
                int openSlot = OpenSlot(Server.accounts);
                if (openSlot < (Globals.MAX_ACCOUNTS + 1))
                {
                    Server.accounts[openSlot].Name = name;
                    Server.accounts[openSlot].Password = pass;
                    Server.accounts[openSlot].EmailAddress = email;
                    Server.accounts[openSlot].CreateAccountInDatabase();
                    Logging.WriteMessage("Username: " + name + " Email: " + email);
                }
                else { Logging.WriteMessage("Server is full!"); OutgoingData.SendErrorMessage("Full", "The server is full!", incMSG.SenderConnection); return; }
            }
            else { Logging.WriteMessage("Account already exists!"); OutgoingData.SendErrorMessage("Exists", "That account already exists!", incMSG.SenderConnection); return; }
        }
        private void HandleDiscoveryRequest(NetIncomingMessage incMSG)
        {
            Logging.WriteMessage("Client Discovered @ " + incMSG.SenderEndPoint.ToString());
            NetOutgoingMessage outMSG = Program.netServer.CreateMessage();
            outMSG.Write("Krypt Chat Server");
            Program.netServer.SendDiscoveryResponse(outMSG, incMSG.SenderEndPoint);
        }
        private void HandleConnectionApproval(NetIncomingMessage incMSG)
        {
            if (incMSG.ReadByte() == (byte)Packet.Connection)
            {
                string Connect = incMSG.ReadString();
                if (Connect == "kryptChat")
                {
                    incMSG.SenderConnection.Approve();
                    Sleep(500);
                    NetOutgoingMessage outMSG = Program.netServer.CreateMessage();
                    outMSG.Write((byte)Packet.Connection);
                    Program.netServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
                }
                else { incMSG.SenderConnection.Deny(); }
            }
        }
        private void HandleStatusChange(NetIncomingMessage incMSG)
        {
            Logging.WriteMessage(incMSG.SenderConnection.ToString() + " status changed. " + incMSG.SenderConnection.Status);
        }
        private static int OpenSlot(Account[] accounts)
        {
            for (int i = 0; i < Globals.MAX_ACCOUNTS; i++)
            {
                if (accounts[i].Name == null)
                {
                    return i;
                }
            }
            return Globals.MAX_ACCOUNTS + 1;
        }
    }
}
