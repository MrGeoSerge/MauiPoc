using System.Text;
using System.Net.Http;
using System.Windows.Forms;
using Newtonsoft.Json;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.SignalR.Client;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace SendPushNotificationPoc
{
    public partial class Form1 : Form
    {
        private HubConnection _hubConnection;
        public Form1()
        {
            InitializeComponent();
            InitializeSignalR();
        }

        private async void InitializeSignalR()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5281/dataHub")
                .Build();

            _hubConnection.On<string>("ReceiveData", (data) =>
            {
                // Handle the received data
                UpdateTextBox(data);
                MessageBox.Show($"Received data: {data}");
            });

            try
            {
                await _hubConnection.StartAsync();
                MessageBox.Show("Connected to SignalR Hub");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to SignalR Hub: {ex.Message}");
            }
        }
        private async void SendPushButton_Click(object sender, EventArgs e)
        {
            var message = new FirebaseAdmin.Messaging.Message()
            {
                Notification = new Notification
                {
                    Title = txtNotificationTitle.Text,
                    Body = txtNotificationBody.Text,
                },
                //Data = new Dictionary<string, string>()
                //{
                //    ["CustomData"] = "Hello, how are you doing?"
                //},
                Token = txtDeviceToken.Text
            };

            var messaging = FirebaseMessaging.DefaultInstance;
            var result = await messaging.SendAsync(message);

            if (!string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Notification sent successfully!");
            }
            else
            {
                MessageBox.Show("Error sending notification.");
            }
        }

        private void UpdateTextBox(string token)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateTextBox), token);
            }
            else
            {
                txtDeviceToken.Text = token;
            }
        }

        protected async override void OnFormClosed(FormClosedEventArgs e)
        {
            if (_hubConnection != null)
            {
                _hubConnection.StopAsync().Wait();
                await _hubConnection.DisposeAsync();
            }

            base.OnFormClosed(e);
        }
    
    }
}
