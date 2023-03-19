﻿using ChatClient.Net.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Net
{
    class Server
    {
        public PacketReader _packetReader;
        TcpClient _client;
        PacketBuilder _packetBuilder;
        public event Action connectedEvent;
        public Server()
        {
            _client = new TcpClient();
        }

        public void ConnectToServer(string username)
        {
            if (!_client.Connected)
            {
                _client.Connect("127.0.0.1", 7891);
                _packetReader = new PacketReader(_client.GetStream());

                if (!string.IsNullOrEmpty(username))
                {
                    var connectPacket = new PacketBuilder();
                    connectPacket.WriteOpCode(0);
                    connectPacket.WriteString(username);
                    _client.Client.Send(connectPacket.GetPacketBytes());
                }
                ReadPackets();
            }
        }

        private void ReadPackets()
        {
            Task.Run(() =>
            {
            while (true)
            {
                var opcode = _packetReader.ReadByte();
                switch (opcode)
                    {
                        case 1:
                            connectedEvent?.Invoke();
                            break;
                        default:
                            Console.WriteLine("Yes");
                            break;
                    }
                }
            });
        }
    }
}
