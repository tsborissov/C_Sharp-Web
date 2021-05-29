using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebServer
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            //const string NewLine = "\r\n";

            var ipAddress = IPAddress.Loopback;
            var port = 8888;

            var serverListener = new TcpListener(ipAddress, port);

            serverListener.Start();

            Console.WriteLine($"Server started on {ipAddress}:{port}...");

            var requestId = 0;

            while (true)
            {
                Console.Write("Waiting for a connection...");

                var connection = await serverListener.AcceptTcpClientAsync();

                Console.WriteLine(" -> Connected.");

                var contentType = @"text/plain; charset=utf-8";
                var content = $"Hello from the server. Request ID is: {requestId}";
                var contentLength = Encoding.UTF8.GetByteCount(content);

                var sb = new StringBuilder();

                sb.AppendLine($"HTTP/1.1 {HttpStatusCode.OK}");
                sb.AppendLine($"Server: Web Server");
                sb.AppendLine($"Content-Type: {contentType}");
                sb.AppendLine($"Content-Length: {contentLength}");
                sb.AppendLine();
                sb.AppendLine(content);

                var response = sb.ToString();
                var responseBytes = Encoding.UTF8.GetBytes(response);

                using var networkStream = connection.GetStream();
                await networkStream.WriteAsync(responseBytes);

                await Task.Delay(1);

                connection.Close();

                Console.WriteLine($"Request with id: {requestId} was processed.");

                requestId++;
            }
        }
    }
}
