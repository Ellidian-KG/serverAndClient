using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        TcpListener server = null;
        try
        {
            Int32 port = 12345;
            IPAddress localAddr = IPAddress.Parse("192.168.0.105"); 

            server = new TcpListener(localAddr, port);

            server.Start();
            Console.WriteLine("Сервер запущен...");

            while (true) 
            {
                TcpClient client = server.AcceptTcpClient();
                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };

                Task.Run(() =>
                {
                    while (true)
                    {
                        string message = reader.ReadLine();
                        Console.WriteLine("Получено сообщение от клиента: " + message);

                        writer.WriteLine(message); 
                    }
                });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
        finally
        {
            server?.Stop();
        }
    }
}
