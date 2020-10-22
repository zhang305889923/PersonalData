using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
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
            IPAddress iP = IPAddress.Parse(serverIp);
            IPEndPoint point = new IPEndPoint(iP, int.Parse(listenPoint));
            #region 第一中方式
            //Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ////serverSocket.Connect(point);
            //Console.WriteLine("已连接主机：" + serverIp);
            //while (true)
            //{
            //    Console.WriteLine("请输入需要发送的信息：");
            //    string sendMessage = Console.ReadLine();
            //    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(sendMessage);
            //    serverSocket.Send(byteArray, SocketFlags.None);
            //    byte[] by = new byte[100];
            //    int i = serverSocket.Receive(by, SocketFlags.None);
            //    if (i < 0)
            //    {
            //        Console.WriteLine("已与服务端断开连接，是否尝试重新连接(Y/N)?");
            //        string result = Console.ReadLine();
            //        if (result.Equals("Y"))
            //        {
            //            serverSocket.Connect(point);
            //        }
            //        else
            //        {
            //            break;
            //        }
            //    }
            //    string message = Encoding.UTF8.GetString(by);
            //    Console.WriteLine( message); 
            //}
            //serverSocket.Close();
            #endregion
            #region 第二种方式

            Console.WriteLine("Client Running ...");
            TcpClient client = new TcpClient();
            try
            {
                client.Connect(point);      // 与服务器连接
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //return;
            }
            // 打印连接到的服务端信息
            Console.WriteLine("Server Connected！{0} --> {1}",
                client.Client.LocalEndPoint, client.Client.RemoteEndPoint);

            while (true)
            {
                Console.WriteLine("请输入需要发送的信息：");
                string sendMessage = Console.ReadLine();
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(sendMessage);
                NetworkStream networkStream = client.GetStream();
                try
                {
                    networkStream.Write(byteArray, 0, byteArray.Length);
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("是否进行重试？（Y/N）");
                    ConsoleKeyInfo consoleKey = Console.ReadKey(false);
                    if (consoleKey.Key == ConsoleKey.Y|| consoleKey.Key ==ConsoleKey.Y)
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }

                }
            }
            client.Dispose();
            Console.WriteLine("已退出对话！");
            Console.ReadLine();
            #endregion
        }
    }
}
