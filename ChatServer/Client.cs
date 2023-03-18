using ChatServer.Net.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    class Client
    {

        public string  userName { get; set; }
        public Guid UID { get; set; }
        public TcpClient clientSocket { get; set; }

        PacketReader _packetReader;
        public Client(TcpClient client)
        {
            
            clientSocket = client;
            UID = new Guid();
            _packetReader = new PacketReader(clientSocket.GetStream());

            var opcode = _packetReader.ReadByte();
            userName = _packetReader.ReadMessage();

            Console.WriteLine($"[{DateTime.Now}:Client has connected with user name : {userName}");
        }
    }
}
