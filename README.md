
# MAUI Push Notification POC

This project demonstrates how to send push notifications from a WinForms desktop application to a specific device using .NET 8, MAUI, and ASP.NET WebAPI.

## Prerequisites

- .NET 8 SDK
- Visual Studio 2022 or later
- Firebase account
- Apple Developer account (for iOS)
- Git

## Project Structure

- **MAUI Application**: Receives push notifications.
- **WinForms Application**: Sends push notifications.
- **ASP.Net WebAPI**: Receives device tokens from the MAUI application.

## Setup Instructions

### Clone the Repository

First, clone the GitHub repository to your local machine:

```bash
git clone https://github.com/MrGeoSerge/MauiPoc.git
cd MauiPoc/SendPushNotificationPoc
```

### A) Setting Up the MAUI Application

#### 1. Create a MAUI Application

Open Visual Studio and create a new .NET MAUI App project.

#### 2. Add Firebase Logic to the App

1. Add the `Xamarin.Firebase.Messaging` package to your MAUI project:
    ```bash
    dotnet add package Xamarin.Firebase.Messaging
    ```

2. Configure Firebase in your MAUI project:

    - Download the `google-services.json` (for Android) and `GoogleService-Info.plist` (for iOS) files from your Firebase project.
    - Place these files in the root directory.

3. Initialize Firebase in your MAUI project:
    ```csharp
    private static MauiAppBuilder RegisterFirebaseServices(this MauiAppBuilder builder)
    {
        builder.ConfigureLifecycleEvents(events => {
#if IOS
            events.AddiOS(iOS => iOS.WillFinishLaunching((app, launchOptions) => {
                CrossFirebase.Initialize();
                FirebaseCloudMessagingImplementation.Initialize();
                return true;
            }));
#elif ANDROID
            events.AddAndroid(android => android.OnCreate((activity, _) =>
            {
                CrossFirebase.Initialize(activity);
                FirebaseAnalyticsImplementation.Initialize(activity);
            }));
#endif
        });

        CrossFirebaseCloudMessaging.Current.NotificationTapped += Current_NotificationTapped;


        return builder;
    }

    ```

#### 3. Setup Firebase Console

1. Go to the [Firebase Console](https://console.firebase.google.com/).
2. Add a new project or use an existing one.
3. Register your Android and iOS apps with the package name `com.companyname.mauipocpushnotification`.
4. Download the `google-services.json` and `GoogleService-Info.plist` files as mentioned above.

#### 4. Setup Apple Certificates and Provisioning Profiles

1. Log into Apple Developer Portal
    - Log into your Apple Developer Portal account.
    - Navigate to Certificates, Identifiers & Profiles.
2. Create an App ID
    - Go to Identifiers and click the add button (+).
    - Select App IDs and click Continue.
3. Register Your App ID
    - Choose App as the type and click Continue.
    - Enter a Description and Bundle ID (this should match the ApplicationId in your .csproj file).
    - Enable Push Notifications in the Capabilities list.
    - Click Continue.
    - Confirm your App ID details and click Register.
4. Create a Provisioning Profile
    - Create a provisioning profile for the newly created App ID:
    - Ensure you add your physical device to the provisioning profile if using a development certificate.
5. Configure Push Notifications
    - Go back to the newly created App ID and configure Push Notifications.
    - Create an additional certificate for Push Notifications:
    - Click Create Certificate and follow the setup steps.
    - Download the certificate and double-click it to add it to your Keychain.
6. Create an APNs Authentication Key
    - Go to Keys and click Create a key.
    - Enter a Key Name and select Apple Push Notifications service (APNs).
    - Click Continue.
    - Download the APNs authentication key and upload it to your project in the Firebase Console.
7. Enable Push Notifications in Your Project
    - Switch back to your Visual Studio.
    - Add an Entitlement.plist file to your iOS platform folder.
    - Enable Push Notifications in the Entitlement.plist file.

### B) Setting Up the WinForms Application

1. Open the WinForms project in Visual Studio.
2. Add the required NuGet packages for sending push notifications. For example, `FirebaseAdmin` for Firebase:
    ```bash
    dotnet add package FirebaseAdmin
    ```

3. Initialize Firebase Admin SDK in your WinForms application and use it to send push notification:
    ```csharp
        private async void SendPushButton_Click(object sender, EventArgs e)
        {
            var message = new FirebaseAdmin.Messaging.Message()
            {
                Notification = new Notification
                {
                    Title = txtNotificationTitle.Text,
                    Body = txtNotificationBody.Text,
                },
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
    ```

### C) Setting Up the ASP.Net WebAPI

1. Open the ASP.Net WebAPI project in Visual Studio.
2. Create a controller to receive device tokens:
    ```csharp
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DataModel dataModel)
        {
            var token = dataModel.Token;
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Data cannot be null or empty.");
            }

            await _hubContext.Clients.All.SendAsync("ReceiveData", token);

            return Ok($"Received data: {token}");
        }
    ```

3. Configure the WebAPI project to use HTTPS.

## Running the Project

1. **Start the ASP.Net WebAPI**:
    - Run the WebAPI project TokenReceiverWebApi from Visual Studio. Ensure it is running and accessible.

2. **Run the MAUI Application**:
    - Run the MAUI application on your Android or iOS device. Ensure it registers with Firebase and obtains a device token.

3. **Send Push Notification using WinForms**:
    - Run the WinForms application.
    - Use the FirebaseMessaging to send a push notification to the device token obtained from the MAUI application.

With these steps, you should have a working MAUI application that can receive push notifications, a WinForms application that can send push notifications, and a WebAPI that receives device tokens.
