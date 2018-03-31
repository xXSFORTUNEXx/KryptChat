using Lidgren.Network;
using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using static System.Console;
using static System.Environment;

namespace Server
{
    public class Program
    {
        public static NetServer netServer;
        static void Main(string[] args)
        {
            Title = "Krypt Chat Server";
            Logging.WriteMessage("Loading Please Wait...");

            NetPeerConfiguration netConfig = new NetPeerConfiguration("kryptChat")
            {
                Port = Globals.SERVER_PORT,
                UseMessageRecycling = true,
                MaximumConnections = Globals.MAX_ACCOUNTS,
                EnableUPnP = false,
                ConnectionTimeout = Globals.CONNECTION_TIMEOUT,
            };
            Logging.WriteMessage("Configuration Complete");

            netConfig.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            netConfig.EnableMessageType(NetIncomingMessageType.ConnectionLatencyUpdated);
            netConfig.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);
            Logging.WriteMessage("Message Types Enabled: ConnectionApproval, Latency, Request");

            MSSQL.DatabaseExists();
            Logging.WriteMessage("SQL Database Complete");

            netServer = new NetServer(netConfig);
            netServer.Start();
            Logging.WriteMessage("Configuration Applied, Server Started");

            Server.Loop();
        }
    }

    public static class Server
    {
        static int lastTick;
        static int lastFrameRate;
        static int frameRate;
        public static Account[] accounts = new Account[Globals.MAX_ACCOUNTS];
        public static void Loop()
        {
            IncomingData incData = new IncomingData();
            Logging.WriteMessage("Listening For Connections...");
            Thread inputThread = new Thread(() => UserInput());
            inputThread.Start();
            InitializeArrays();
            while (true)
            {
                Title = "Krypt Chat Server - CPS: " + CalculateCyclesPerSecond();
                incData.HandleIncomingData();
                Thread.Sleep(10);
            }
        }

        private static void InitializeArrays()
        {
            for (int i = 0; i < Globals.MAX_ACCOUNTS; i++)
            {
                accounts[i] = new Account();
            }
        }

        private static void UserInput()
        {
            string input;
            while (true)
            {
                Write(">");
                input = ReadLine();

                switch (input.ToLower())
                {
                    case "time":
                        string time = "Local Time: " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff");
                        WriteLine(time);
                        Logging.WriteLog(time);
                        break;

                    case "stats":
                        string stats = Program.netServer.Statistics.ToString();
                        WriteLine(stats);
                        Logging.WriteLog(stats);
                        break;

                    case "ifconfig":
                        string localIP = NetUtility.Resolve(Dns.GetHostName()).ToString();
                        string broadcastAddress = Program.netServer.Configuration.BroadcastAddress.ToString();
                        string port = Program.netServer.Configuration.Port.ToString();
                        string netInfo = string.Format("Public IP: {0}\nLocal IP: {1}\nBroadcast Address: {2}\nPort: {3}", GetPublicIPAddress(), localIP, broadcastAddress, port);
                        WriteLine(netInfo);
                        Logging.WriteLog(netInfo);
                        break;

                    case "help":
                        string commands = "Commands:\ntime - shows local time\nstats - shows network statistics\nifconfig - shows network info\nhelp - shows commands\nexit - closes the server";
                        WriteLine(commands);
                        break;

                    case "exit":
                        Program.netServer.Shutdown("Shutdown");
                        Exit(0);
                        break;
                }
            }
        }

        private static int CalculateCyclesPerSecond()
        {
            if (TickCount - lastTick >= 1000)
            {
                lastFrameRate = frameRate;
                frameRate = 0;
                lastTick = TickCount;
            }
            frameRate++;
            return lastFrameRate;
        }

        private static string GetPublicIPAddress()
        {
            try
            {
                string externalIP;
                externalIP = (new WebClient()).DownloadString("http://checkip.dyndns.org/");
                externalIP = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}")).Matches(externalIP)[0].ToString();
                return externalIP;
            }
            catch { return null; }
        }
    }

    public static class Globals
    {
        public const byte NO = 0;
        public const byte YES = 1;
        public const int MAX_ACCOUNTS = 5;
        public const string IP_ADDRESS = "10.16.0.8";
        public const int SERVER_PORT = 14242;
        public const float CONNECTION_TIMEOUT = 5.0f;
    }

    public enum Packet : byte
    {
        Connection,
        Registration,
        ErrorMessage,
        Login,
        ActivateAccount
    }
}
