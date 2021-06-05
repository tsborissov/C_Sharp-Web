using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WebServer.Server.Http;
using WebServer.Server.Routing;

namespace WebServer.Server
{
    public class HttpServer
    {
        private readonly IPAddress ipAddress;
        private readonly int port;
        private readonly TcpListener listener;
        private int requestId;

        public HttpServer(string ipAddress, int port, Action<IRoutingTable> routingTable)
        {
            this.ipAddress = IPAddress.Parse(ipAddress);
            this.port = port;

            this.listener = new TcpListener(this.ipAddress, this.port);
        }

        public HttpServer(int port, Action<IRoutingTable> routingTable) 
            : this("127.0.0.1", port, routingTable)
        {
        }

        public HttpServer(Action<IRoutingTable> routingTable)
            : this(5000 , routingTable)
        {
        }


        public async Task Start()
        {
            this.listener.Start();

            Console.WriteLine($"Server started on {ipAddress}:{port}...");

            this.requestId = 0;

            while (true)
            {
                Console.Write("Waiting for a connection...");

                var connection = await this.listener.AcceptTcpClientAsync();

                Console.WriteLine(" -> Connected.");
                Console.WriteLine();

                var networkStream = connection.GetStream();
                
                Console.WriteLine($"Client request with id: {requestId} received:");
                Console.WriteLine();

                var requestText = await ReadRequest(networkStream);
                Console.WriteLine(requestText);

                // var request = HttpRequest.Parse(requestText);

                Console.WriteLine($"Request with id: {this.requestId} was processed.");
                Console.WriteLine(new string('-', 50));

                await WriteResponse(networkStream);

                connection.Close();

                this.requestId++;
            }
        }

        private async Task<string> ReadRequest(NetworkStream networkStream)
        {
            var bufferSize = 1024;
            var buffer = new byte[bufferSize];
            var sbRequest = new StringBuilder();

            var totalBytes = 0;

            do
            {
                var bytesRead = await networkStream.ReadAsync(buffer, 0, bufferSize);

                totalBytes += bytesRead;

                if (totalBytes > 10 * 1024)
                {
                    throw new InvalidOperationException("Request is too large!");
                }

                sbRequest.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            }
            while (networkStream.DataAvailable);

            return sbRequest.ToString();
        }

        private async Task WriteResponse(NetworkStream networkStream)
        {
            var contentType = @"text/html; charset=utf-8";
            var content = $"<h1>Hello from the server.</h1><br><h2>Request ID: {this.requestId}</h2>";
            var contentLength = Encoding.UTF8.GetByteCount(content);

            var sbResponse = new StringBuilder();

            sbResponse.AppendLine($"HTTP/1.1 200 OK");
            sbResponse.AppendLine($"Date: {DateTime.UtcNow.ToString("r")}");
            sbResponse.AppendLine($"Server: Web Server");
            sbResponse.AppendLine($"Content-Type: text/html; charset=utf-8");
            sbResponse.AppendLine($"Content-Length: {contentLength}");
            sbResponse.AppendLine();
            sbResponse.AppendLine(content);

            var response = sbResponse.ToString();
            var responseBytes = Encoding.UTF8.GetBytes(response);

            await networkStream.WriteAsync(responseBytes);
        }
    }
}
