using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace SendPushNotificationPoc
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            CreateFirebaseApp();
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }

        static void CreateFirebaseApp()
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "mauipocpushnotification-firebase-adminsdk.json")),
            });
        }
    }
}