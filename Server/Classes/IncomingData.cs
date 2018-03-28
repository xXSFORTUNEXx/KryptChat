using System;
using Lidgren.Network;
using static System.Console;
using static System.Threading.Thread;
using System.Data.SqlClient;

namespace Server.Classes
{
    public class IncomingData
    {
        public void HandleIncomingData(NetServer netServer)
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

                    case NetIncomingMessageType.StatusChanged:
                        HandleStatusChange(incMSG, netServer);
                        break;

                    case NetIncomingMessageType.Data:
                        switch (incMSG.ReadByte())
                        {

                        }
                        break;
                }
            }
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
            if (incMSG.SenderConnection.Status == NetConnectionStatus.Disconnected || incMSG.SenderConnection.Status == NetConnectionStatus.Disconnecting)
            {
                Logging.WriteMessage("Disconnected, clearing data...");
                //Clear data
                Logging.WriteMessage("Data cleared, connection now open.");
            }
        }
    }

    public enum Packet : byte
    {
        Connection,
        Register,
        Login,
        Error,
        Notification,
        CharacterData
    }
}
