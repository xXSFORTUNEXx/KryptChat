using System;
using Lidgren.Network;
using static System.Console;
using static System.Threading.Thread;
using System.Data.SqlClient;

namespace Server
{
    public class IncomingData
    {
        public void HandleIncomingData(NetServer netServer, Account[] accounts)
        {
            NetIncomingMessage incMSG;

            if ((incMSG = netServer.ReadMessage()) != null)
            {
                switch (incMSG.MessageType)
                {
                    case NetIncomingMessageType.DiscoveryRequest:
                        HandleDiscoveryRequest(incMSG, netServer);
                        break;

                    case NetIncomingMessageType.ConnectionApproval:
                        HandleConnectionApproval(incMSG, netServer);
                        break;

                    case NetIncomingMessageType.Data:
                        switch (incMSG.ReadByte())
                        {
                            case (byte)Packet.Registration:
                                HandleRegistrationRequest(incMSG, netServer, accounts);
                                break;
                            case (byte)Packet.Test:
                                WriteLine("This is a test");
                                break;
                        }
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        HandleStatusChange(incMSG, netServer);
                        break;
                }
            }
            netServer.Recycle(incMSG);
        }
        private void HandleRegistrationRequest(NetIncomingMessage incMSG, NetServer netServer, Account[] accounts)
        {
            string name = incMSG.ReadString();
            string pass = incMSG.ReadString();
            string email = incMSG.ReadString();

            if (!MSSQL.AccountExist(name))
            {
                int openSlot = OpenSlot(accounts);
                if (openSlot < (Globals.MAX_ACCOUNTS + 1))
                {
                    accounts[openSlot].Name = name;
                    accounts[openSlot].Password = pass;
                    accounts[openSlot].EmailAddress = email;
                    accounts[openSlot].CreateAccountInDatabase();
                    Logging.WriteMessage("Username: " + name + " Email: " + email);
                }
                else { Logging.WriteMessage("Server is full!"); return; }
            }
            else { Logging.WriteMessage("Account already exists!"); return; }
        }
        private void HandleDiscoveryRequest(NetIncomingMessage incMSG, NetServer netServer)
        {
            Logging.WriteMessage("Client Discovered @ " + incMSG.SenderEndPoint.ToString());
            NetOutgoingMessage outMSG = netServer.CreateMessage();
            outMSG.Write("Krypt Chat Server");
            netServer.SendDiscoveryResponse(outMSG, incMSG.SenderEndPoint);
        }
        private void HandleConnectionApproval(NetIncomingMessage incMSG, NetServer netServer)
        {
            if (incMSG.ReadByte() == (byte)Packet.Connection)
            {
                string Connect = incMSG.ReadString();
                if (Connect == "kryptChat")
                {
                    incMSG.SenderConnection.Approve();
                    Sleep(500);
                    NetOutgoingMessage outMSG = netServer.CreateMessage();
                    outMSG.Write((byte)Packet.Connection);
                    netServer.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
                }
                else { incMSG.SenderConnection.Deny(); }
            }
        }
        private void HandleStatusChange(NetIncomingMessage incMSG, NetServer netServer)
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
