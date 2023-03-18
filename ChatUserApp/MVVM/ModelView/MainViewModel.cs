using ChatClient.MVVM.Core;
using ChatClient.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.MVVM.ModelView
{
    class MainViewModel
    {
        public string userName { get; set; }
        public RelayCommand connectToServerCommand { get; set; }
        public Server _server { get; set; }
        public MainViewModel()
        {
            _server = new Server();
            connectToServerCommand = new RelayCommand(o => _server.ConnectToServer(userName), o=> !string.IsNullOrEmpty(userName));
        }
    }
}
