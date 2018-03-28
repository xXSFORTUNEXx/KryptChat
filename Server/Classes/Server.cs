using Lidgren.Network;
using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using static System.Console;
using static System.Environment;

namespace Server
{
    class Program
    {
        static NetServer netServer;
        static void Main(string[] args)
        {
            Title = "Krypt Chat Server";
            Logging.WriteMessage("Loading Please Wait...");

            NetPeerConfiguration netConfig = new NetPeerConfiguration("kryptChat")
            {
                Port = 14242,
                UseMessageRecycling = true,
                MaximumConnections = 5,
                EnableUPnP = false,
                ConnectionTimeout = 5.0f,
            };
            Logging.WriteMessage("Configuration Complete...");

            netConfig.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            netConfig.EnableMessageType(NetIncomingMessageType.ConnectionLatencyUpdated);
            netConfig.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);
            Logging.WriteMessage("Message Types Enabled: ConnectionApproval, Latency, Request");

            netServer = new NetServer(netConfig);
            netServer.Start();
            Logging.WriteMessage("Configuration Applied, Server Started...");

            Server server = new Server();
            server.Loop(netServer);
        }
    }

    class Server
    {
        static int lastTick;
        static int lastFrameRate;
        static int frameRate;
        public void Loop(NetServer netServer)
        {
            IncomingData incData = new IncomingData();
            Logging.WriteMessage("Listening For Connections...");
            Thread inputThread = new Thread(() => UserInput(netServer));
            inputThread.Start();
            while (true)
            {
                Title = "Krypt Chat Server - CPS: " + CalculateCyclesPerSecond();
                incData.HandleIncomingData(netServer);
                Thread.Sleep(10);
            }
        }

        void UserInput(NetServer netServer)
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
                        string stats = netServer.Statistics.ToString();
                        WriteLine(stats);
                        Logging.WriteLog(stats);
                        break;

                    case "ifconfig":
                        string localIP = NetUtility.Resolve(Dns.GetHostName()).ToString();
                        string broadcastAddress = netServer.Configuration.BroadcastAddress.ToString();
                        string port = netServer.Configuration.Port.ToString();
                        string netInfo = string.Format("Public IP: {0}\nLocal IP: {1}\nBroadcast Address: {2}\nPort: {3}", GetPublicIPAddress(), localIP, broadcastAddress, port);
                        WriteLine(netInfo);
                        Logging.WriteLog(netInfo);
                        break;

                    case "help":
                        string commands = "Commands:\ntime - shows local time\nstats - shows network statistics\nifconfig - shows network info\nhelp - shows commands\nexit - closes the server";
                        WriteLine(commands);
                        break;

                    case "exit":
                        Exit(0);
                        break;
                }
            }
        }

        static int CalculateCyclesPerSecond()
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

        protected string GetPublicIPAddress()
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
}
