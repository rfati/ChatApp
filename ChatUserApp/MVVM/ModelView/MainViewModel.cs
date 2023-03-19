using ChatClient.MVVM.Core;
using ChatClient.MVVM.Model;
using ChatClient.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChatClient.MVVM.ModelView
{
    class MainViewModel
    {
        public ObservableCollection<UserModel> Users { get; set; }
        public string userName { get; set; }
        public RelayCommand connectToServerCommand { get; set; }
        public Server _server { get; set; }
        public MainViewModel()
        {
            Users = new ObservableCollection<UserModel>();
            _server = new Server();
            _server.connectedEvent += UserConnected;
            connectToServerCommand = new RelayCommand(o => _server.ConnectToServer(userName), o => !string.IsNullOrEmpty(userName));
        }
        private void UserConnected()
        {
            var user = new UserModel()
            {
                userName = _server._packetReader.ReadMessage(),
                UID = _server._packetReader.ReadMessage()
            };

            if (Users.Any(x => x.UID == user.UID))
            {
                Application.Current.Dispatcher.Invoke(() => Users.Add(user));
            }
        }
    }
}
