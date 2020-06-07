using System;
using System.Net;
using System.Net.Sockets;

namespace ntpClient
{
    internal class Program
    {
        public static DateTime GetTime()
        {
            const string ntpServer = "time.windows.com";
            const int port = 123;
            const byte serverReplyTime = 40;
            
            var ntpData = new byte[48];
            ntpData[0] = 0x1B;
            
            var ntpAddresses = Dns.GetHostEntry(ntpServer).AddressList;
            var ipEndPoint = new IPEndPoint(ntpAddresses[0], port);
            
            using(var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                socket.Connect(ipEndPoint);
                socket.ReceiveTimeout = 3000;     
            
                socket.Send(ntpData);
                socket.Receive(ntpData);
                socket.Close();
            }

            ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);
            ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);
            
            intPart = SwapEndianness(intPart);
            fractPart = SwapEndianness(fractPart);
            
            var milliseconds = intPart * 1000 + fractPart * 1000 / 0x100000000L;
            var networkDateTime = (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds((long)milliseconds);
            
            return networkDateTime.ToLocalTime();
        }
        
        static uint SwapEndianness(ulong x)
        {
            return (uint) (((x & 0x000000ff) << 24) +
                           ((x & 0x0000ff00) << 8) +
                           ((x & 0x00ff0000) >> 8) +
                           ((x & 0xff000000) >> 24));
        }
        
        public static void Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            GetTime();
            GetTime();
            GetTime();
            GetTime();
            GetTime();
            watch.Stop();
            var delay = watch.ElapsedMilliseconds / 5;
            
            while (true)
            {
                Console.Write("Get time? (yes/no): ");
                if (Console.ReadLine() == "yes")
                {
                    Console.WriteLine(GetTime().AddMilliseconds(delay));
                    Console.WriteLine();
                }
                else
                {
                    break;
                }
            }
        }
    }
}