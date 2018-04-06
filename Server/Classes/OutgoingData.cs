using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using System.Net.Mail;
using System.Net;

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
        public static void SendActivateMessage(string name, int slot, NetConnection netConnection)
        {
            NetOutgoingMessage outMSG = Program.netServer.CreateMessage();
            outMSG.Write((byte)Packet.ActivateAccount);
            outMSG.Write(name);
            outMSG.WriteVariableInt32(slot);
            Program.netServer.SendMessage(outMSG, netConnection, NetDeliveryMethod.ReliableOrdered);
        }

        public static void SendActivationEmail(string code, string email)
        {
            MailMessage mail = new MailMessage("webmaster@fortune.naw", email);
            SmtpClient client = new SmtpClient();
            client.Host = Globals.SMTP_IP_ADDRESS;
            client.Port = Globals.SMTP_SERVER_PORT;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential(Globals.SMTP_USER_CREDS, Globals.SMTP_PASS_CREDS);
            mail.Subject = "Activation Code";
            mail.Body = "You activation code is: " + code;
            client.Send(mail);
        }

        public static void SendLogin(string name, NetConnection netConnection)
        {
            NetOutgoingMessage outMSG = Program.netServer.CreateMessage();
            outMSG.Write((byte)Packet.Login);
            outMSG.Write(name);
            Program.netServer.SendMessage(outMSG, netConnection, NetDeliveryMethod.ReliableOrdered);
        }

        public static void SendWhosOnline(Account[] accounts)
        {
            NetOutgoingMessage outMSG = Program.netServer.CreateMessage();
            outMSG.Write((byte)Packet.WhosOnline);
            for (int i = 0; i < Globals.MAX_ACCOUNTS; i++)
            {
                outMSG.WriteVariableInt32(accounts[i].ID);
                outMSG.Write(accounts[i].Name);
            }
            Program.netServer.SendToAll(outMSG, NetDeliveryMethod.ReliableOrdered);
        }

        public static void SendMessageToAll(string message)
        {
            NetOutgoingMessage outMSG = Program.netServer.CreateMessage();
            outMSG.Write((byte)Packet.Message);
            outMSG.Write(message);
            Program.netServer.SendToAll(outMSG, NetDeliveryMethod.ReliableOrdered);
        }
    }
}
