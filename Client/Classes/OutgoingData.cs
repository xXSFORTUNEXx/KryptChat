using Lidgren.Network;

namespace Client
{
    public static class OutgoingData
    {
        public static void SendRegistration(string name, string password, string emailaddress)
        {
            NetOutgoingMessage outMSG = Program.netClient.CreateMessage();
            outMSG.Write((byte)Packet.Registration);
            outMSG.Write(name);
            outMSG.Write(password);
            outMSG.Write(emailaddress);
            Program.netClient.SendMessage(outMSG, NetDeliveryMethod.ReliableOrdered);
            Program.netClient.FlushSendQueue();
        }
        public static void SendLogin(string name, string password)
        {
            NetOutgoingMessage outMSG = Program.netClient.CreateMessage();
            outMSG.Write((byte)Packet.Login);
            outMSG.Write(name);
            outMSG.Write(password);
            Program.netClient.SendMessage(outMSG, NetDeliveryMethod.ReliableOrdered);
            Program.netClient.FlushSendQueue();
        }
        
        public static void SendCode(string code)
        {
            NetOutgoingMessage outMSG = Program.netClient.CreateMessage();
            outMSG.Write((byte)Packet.ActivateAccount);
            outMSG.Write(code);
            outMSG.WriteVariableInt32(Program.tempSlot);
            Program.netClient.SendMessage(outMSG, NetDeliveryMethod.ReliableOrdered);
            Program.netClient.FlushSendQueue();
        }

        public static void SendMessage(string message)
        {
            NetOutgoingMessage outMSG = Program.netClient.CreateMessage();
            outMSG.Write((byte)Packet.Message);
            outMSG.Write(message);
            outMSG.Write(Program.tempName);
            Program.netClient.SendMessage(outMSG, NetDeliveryMethod.ReliableOrdered);
            Program.netClient.FlushSendQueue();
        }
    }
}
