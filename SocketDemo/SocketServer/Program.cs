using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketServerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] byteCache = new byte[1024];
            string serverIp = ConfigurationManager.AppSettings.Get("Ip");
            string listenPoint = ConfigurationManager.AppSettings.Get("Point");
            IPAddress iP = IPAddress.Parse(serverIp);//转换为IP地址格式
            IPEndPoint point = new IPEndPoint(iP, int.Parse(listenPoint));
            #region 第一种开启服务监听方式
            //Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //serverSocket.Bind(point);
            ////Console.WriteLine("服务已启动！");
            ////serverSocket.Connect(point);
            //serverSocket.Listen(2);    //开始监听
            ////Console.WriteLine("正在监听端口：" + listenPoint);
            //Socket temp = serverSocket.Accept();//为新建连接创建新的socket
            //while (true)
            //{
            //    //接受到client连接，为此连接建立新的socket，并接受信息
            //    //Console.WriteLine("已与客户端建立连接，等待消息传输！");
            //    string recvStr = "";
            //    byte[] recvBytes = new byte[1024];
            //    int bytes;

            //    try
            //    {
            //        bytes = temp.Receive(recvBytes, recvBytes.Length, 0);//从客户端接受信息
            //    }
            //    catch (System.Net.Sockets.SocketException ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //        temp = serverSocket.Accept();
            //        continue;
            //    }
            //    if (bytes < 0)
            //    {
            //        Console.WriteLine("与客户端连接已断开！");
            //    }
            //    recvStr += Encoding.UTF8.GetString(recvBytes, 0, bytes);
            //    //Console.WriteLine("接收到客户端消息：" + recvStr);
            //    //Thread.Sleep(1000);
            //    recvStr = "服务端响应：" + recvStr.ToUpper();
            //    recvBytes = Encoding.UTF8.GetBytes(recvStr);
            //    temp.Send(recvBytes, SocketFlags.None);
            //} 

            #endregion

            #region 第二种开启服务监听方式

            TcpListener tcpListener = new TcpListener(point);
            tcpListener.Start(10);
            Console.WriteLine("Start Listening ...");
            TcpClient tcpClient = tcpListener.AcceptTcpClient();
            do
            {
                NetworkStream networkStream = tcpClient.GetStream();
                int size = networkStream.Read(byteCache, 0, 2);
                if (size <= 0)
                {
                    Console.WriteLine("读取完成！");

                }
            } while (true);
            #endregion
        }
    }
}
