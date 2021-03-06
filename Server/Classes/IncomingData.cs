﻿using System;
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

                            case (byte)Packet.Login:
                                HandleLoginRequest(incMSG);
                                break;

                            case (byte)Packet.ActivateAccount:
                                HandleActivateAccount(incMSG);
                                break;

                            case (byte)Packet.Message:
                                HandleMessage(incMSG);
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

        private void HandleMessage(NetIncomingMessage incMSG)
        {
            string message = incMSG.ReadString();
            string name = incMSG.ReadString();

            string final = "[" + DateTime.Now.ToString() + "] " + name + ": " + message;
            Logging.WriteMessage(final, "Chat");
            OutgoingData.SendMessageToAll(final);
        }
        private void HandleActivateAccount(NetIncomingMessage incMSG)
        {
            string code = incMSG.ReadString();
            int slot = incMSG.ReadVariableInt32();

            if (code == Server.accounts[slot].AccountKey)
            {
                Logging.WriteMessage("Account activated! Code: " + code + " username: " + Server.accounts[slot].Name);
                Server.accounts[slot].UpdateAccountStatusInDatabase();
                OutgoingData.SendLogin(Server.accounts[slot].Name, incMSG.SenderConnection);
                OutgoingData.SendWhosOnline(Server.accounts);
            }
            else { OutgoingData.SendErrorMessage("Error", "Invalid code please try again.", incMSG.SenderConnection); return; }
        }
        private void HandleLoginRequest(NetIncomingMessage incMSG)
        {
            string name = incMSG.ReadString();
            string pass = incMSG.ReadString();

            int openSlot = OpenSlot(Server.accounts);
            if (openSlot < Globals.MAX_ACCOUNTS + 1)
            {
                if (!IsLogged(name, Server.accounts))
                {
                    if (MSSQL.AccountExist(name) && MSSQL.CheckPassword(name, pass))
                    {
                        int id = Server.accounts[openSlot].GetIdFromDatabase(name);
                        Server.accounts[openSlot] = new Account(id, name, incMSG.SenderConnection);
                        Server.accounts[openSlot].LoadAccountFromDatabase(id);
                        Logging.WriteMessage("Id: " + id + " username: " + name + " connection: " + incMSG.SenderConnection.ToString());
                        if (!Server.accounts[openSlot].IsAccountActive()) { OutgoingData.SendActivateMessage(name, openSlot, incMSG.SenderConnection); return; }
                        OutgoingData.SendLogin(name, incMSG.SenderConnection);
                        OutgoingData.SendWhosOnline(Server.accounts);
                    }
                    else { OutgoingData.SendErrorMessage("Invalid Login", "Invalid username or password.", incMSG.SenderConnection); return; }
                }
                else { Logging.WriteMessage("Account already logged in!"); OutgoingData.SendErrorMessage("Already Logged", "This account is already logged in!", incMSG.SenderConnection); return; }
            }
            else { Logging.WriteMessage("Server is full!"); OutgoingData.SendErrorMessage("Full", "The server is full!", incMSG.SenderConnection); return; }
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
                    OutgoingData.SendActivationEmail(Server.accounts[openSlot].AccountKey, email);
                    Logging.WriteMessage("Username: " + name + " Email: " + email);
                    Server.accounts[openSlot] = new Account();
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

            if (incMSG.SenderConnection.Status == NetConnectionStatus.Disconnected || incMSG.SenderConnection.Status == NetConnectionStatus.Disconnecting)
            {
                ClearSlot(incMSG.SenderConnection, Server.accounts);
                OutgoingData.SendWhosOnline(Server.accounts);
            }
        }
        private static void ClearSlot(NetConnection netConnection, Account[] accounts)
        {
            for (int i = 0; i < Globals.MAX_ACCOUNTS; i++)
            {
                if (accounts[i] != null && accounts[i].netConnection == netConnection)
                {
                    accounts[i] = null;
                    accounts[i] = new Account();
                    break;
                }
            }
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
        private static bool IsLogged(string name, Account[] accounts)
        {
            for (int i = 0; i < Globals.MAX_ACCOUNTS; i++)
            {
                if (name == accounts[i].Name)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
