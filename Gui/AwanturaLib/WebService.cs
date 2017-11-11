using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using Newtonsoft.Json;


namespace AwanturaLib {

    public class WebService {

        public void OpenTcpConnection(IPAddress localAddress, int port = 80) {

            TcpListener server = new TcpListener(localAddress, port);
            server.Start();
            Console.WriteLine("Server has started on 127.0.0.1:80.{0}Waiting for a connection...", Environment.NewLine);

            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("A client connected.");

            NetworkStream stream = client.GetStream();
            Byte[] bytes = new Byte[client.Available];
            stream.Read(bytes, 0, bytes.Length);
            String data = Encoding.UTF8.GetString(bytes);

            if (new Regex("^GET").IsMatch(data)) {

                var writer = new StreamWriter(client.GetStream());

                SendObject(writer, "test\nmessage");

                Console.WriteLine("Data has been sent.");
            }
            else {

                Console.WriteLine("Nope.");
            }

            client.Close();

            System.Console.ReadKey();
        }

        void SendObject<T>(StreamWriter s, T o) {
            s.WriteLine(JsonConvert.SerializeObject(o));
            s.Flush();
        }
    }
}
