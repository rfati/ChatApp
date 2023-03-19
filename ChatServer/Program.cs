using System;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;
using ChatServer.Net.IO;

namespace ChatServer
{
    class Program
    {
        static List<Client> _users;
        static TcpListener _listenir;
        static void Main(string[] args)
        {

            _users = new List<Client>();
            _listenir = new TcpListener(IPAddress.Parse("127.0.0.1"), 7891);
            _listenir.Start();

            while (true)
            {
                var client = new Client(_listenir.AcceptTcpClient());
                _users.Add(client);
                BroadcastConnection();
            }
        }
        static void BroadcastConnection()
        {
            foreach (var user in _users)
            {
                foreach (var usr in _users)
                {
                    var broadcastPacket = new PacketBuilder();
                    broadcastPacket.WriteOpCode(1);
                    broadcastPacket.WriteMessage(usr.userName);
                    broadcastPacket.WriteMessage((usr.UID.ToString()));
                    user.clientSocket.Client.Send(broadcastPacket.GetPacketBytes());
                }
            }
        }
       


    }
}
