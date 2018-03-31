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

        public static void TestData()
        {
            NetOutgoingMessage outMSG = Program.netClient.CreateMessage();
            outMSG.Write((byte)Packet.Test);
            Program.netClient.SendMessage(outMSG, NetDeliveryMethod.ReliableOrdered);
            Program.netClient.FlushSendQueue();
        }
    }
}
