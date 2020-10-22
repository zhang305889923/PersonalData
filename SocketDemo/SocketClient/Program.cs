using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketClientDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string serverIp = ConfigurationManager.AppSettings.Get("Ip");
            string listenPoint = ConfigurationManager.AppSettings.Get("Point");
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress iP = IPAddress.Parse(serverIp);
            IPEndPoint point = new IPEndPoint(iP, int.Parse(listenPoint));
            serverSocket.Connect(point);
            Console.WriteLine("已连接主机：" + serverIp);
            while (true)
            {
                Console.WriteLine("请输入需要发送的信息：");
                string sendMessage = Console.ReadLine();
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(sendMessage);
                serverSocket.Send(byteArray, SocketFlags.None);
                byte[] by = new byte[1024];
                int i = serverSocket.Receive(by, SocketFlags.None);
                if (i < 0)
                {
                    Console.WriteLine("已与服务端断开连接，是否尝试重新连接(Y/N)?");
                    string result = Console.ReadLine();
                    if (result.Equals("Y"))
                    {
                        serverSocket.Connect(point);
                    }
                    else
                    {
                        break;
                    }
                }
                string message = Encoding.UTF8.GetString(by);
                Console.WriteLine("服务器返回结果：" + message);
            }
            serverSocket.Close();
        }
    }
}
