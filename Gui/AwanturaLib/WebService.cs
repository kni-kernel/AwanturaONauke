using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using Newtonsoft.Json;


namespace AwanturaLib {

    public class WebService {

        private TcpListener m_server;
        private GameState m_state;
        private Thread m_listener;

        public WebService(int port = 80) {

            m_server = new TcpListener(IPAddress.Any, port);
            m_server.Start();
            Console.WriteLine(
                "Server has started on port {0}.{1}Waiting for a connection...",
                port, Environment.NewLine);

            m_listener = new Thread(Listen);
            m_listener.Start("Listening");
        }

        public void UpdateGameState(GameState state) {

            Interlocked.Exchange(ref m_state, state);
        }

        private void Listen(object data) {

            while(true) {

                TcpClient client = m_server.AcceptTcpClient();

                NetworkStream stream = client.GetStream();
                Byte[] bytes = new Byte[client.Available];

                stream.Read(bytes, 0, bytes.Length);
                String request = Encoding.UTF8.GetString(bytes);
                Console.WriteLine(request);

                if(new Regex("^GET").IsMatch(request)) {

                    StreamWriter writer = new StreamWriter(client.GetStream());

                    SendObject(writer, m_state);


                    Console.WriteLine("Data has been sent.");
                }
                else {

                    Console.WriteLine("Nope. Only GETs!!");
                }

                client.Close();
            }
        }

        private void SendObject<T>(StreamWriter s, T o) {
            //s.WriteLine(JsonConvert.SerializeObject(o));
            s.Flush();
        }
    }
}
