using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace Server
{
    public static class OutgoingData
    {
        public static void SendErrorMessage(string title, string message, NetConnection netConnection)
        {
            NetOutgoingMessage outMSG = Program.netServer.CreateMessage();
            outMSG.Write((byte)Packet.ErrorMessage);
            outMSG.Write(title);
            outMSG.Write(message);
            Program.netServer.SendMessage(outMSG, netConnection, NetDeliveryMethod.ReliableOrdered);
        }
        public static void SendActivateMessage(int slot, NetConnection netConnection)
        {
            NetOutgoingMessage outMSG = Program.netServer.CreateMessage();
            outMSG.Write((byte)Packet.ActivateAccount);
            outMSG.WriteVariableInt32(slot);
            Program.netServer.SendMessage(outMSG, netConnection, NetDeliveryMethod.ReliableOrdered);
        }
    }
}
