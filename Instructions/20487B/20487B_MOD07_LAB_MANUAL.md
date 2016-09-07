# Module 7: Windows Azure Service Bus


# Lab: Microsoft Azure Service Bus

#### Scenario

The IT team at Blue Yonder Airlines complained that from the time the company opened its booking services to other companies, they had to make many changes to the network&#39;s firewall in order to open inbound ports. This, of course, limits their ability to secure the company&#39;s internal network properly. To resolve this issue, the company has decided to change the way external applications, including the Travel Companion back-end services, connect to the WCF booking service.  Now all the communication with the on-premises service will be directed through a Azure Service Bus Relay. In this lab, you will create a Service Bus Relay in Azure, configure the WCF booking service to register with the Service Bus, and update the Travel Companion back-end service to send messages to the WCF service through the relay.

In addition, Blue Yonder Airlines wishes to improve the service offered to Travel Companion users by sending users&#39; updated information about changes made to their booked flights directly to their client app. To provide immediate feedback to the end user who updates the flight schedules, it was decided that the notifications will not be sent during the update process but rather be sent by a background process. To interact with the background process, the ASP.NET Web API service will use Service Bus Queues, and the background process itself will run in a Azure Worker Role. For the notifications, the worker role will use Windows Push Notification Services (WNS).

In this lab, you will update the ASP.NET Web API services to use Azure Service Bus Queues, and create a new Azure Worker Role to perform background processing.

#### Objectives

After completing this lab, you will be able to:

- Use Microsoft Azure Service Bus Relays.
- Use Azure Service Bus Queues.

#### Lab Setup

Estimated Time: **60 Minutes.**

Virtual Machine: **20487B-SEA-DEV-A**, **20487B-SEA-DEV-C**

User name: **Administrator**, **Admin**

Password: **Pa$$w0rd**, **Pa$$w0rd**

For this lab, you will use the available virtual machine environment. Before you begin this lab, you must complete the following steps:

1. On the host computer, click **Start** , point to **Administrative Tools** , and then click **Hyper-V Manager**.
2. In Hyper-V Manager, click **MSL-TMG1** , and then in the **Actions** pane, click **Start**.
3. In Hyper-V Manager, click **20487B-SEA-DEV-A** , and then in the **Actions** pane, click **Start**.
4. In the **Actions** pane, click **Connect**. Wait until the virtual machine starts.
5. Sign in using the following credentials:

    - User name: **Administrator**
    - Password: **Pa$$w0rd**

6. Return to Hyper-V Manager, click **20487B-SEA-DEV-C** , and then in the **Actions** pane, click **Start**.
7. In the **Actions** pane, click **Connect**. Wait until the virtual machine starts.
8. Sign in using the following credentials:

  - User name: **Admin**
  - Password: **Pa$$w0rd**

9. Verify that you received the credentials to sign in to the Azure portal from your training provider. These credentials and the Azure account will be used in all the labs of this course.

### Exercise 1: Using a Service Bus Relay for the WCF Booking Service

#### Scenario

In this exercise, you will create a Service Bus namespace, configure the on-premises WCF Booking service to use a Service Bus Relay, and then configure the ASP.NET Web API services running in Azure to communicate with the on-premises services by using the newly created relay.

The main tasks for this exercise are as follows:

1. Create the Service Bus namespace by using the Azure Portal.

2. Add a new WCF Endpoint with a relay binding.

3. Configure the ASP.NET Web API back-end service to use the new relay endpoint.

4. Test the WCF service.

#### Task 1: Create the Service Bus namespace by using the Azure Portal

1. In the **20487B-SEA-DEV-A** virtual machine, run the **Setup.cmd** file from **D:\AllFiles\Mod07\LabFiles\Setup** , and then note the name of the cloud service created by the script.

   >**Note:** You might see warnings in yellow indicating a mismatch in the versions and setting deprecations. These warnings may appear if there is a newer version of the Azure PowerShell cmdlets. If this message is followed by a red error message, please inform the instructor, otherwise you can ignore the warnings. ** **

2. Open the Microsoft Azure portal ( **http://manage.windowsazure.com** )
3. Create a new Azure Service Busnamespace named **BlueYonderServerLab07**** YourInitials** (Replace _YourInitials_ with your initials).

    - Select a region closest to your location.
    - Select the newly created Service Bus namespace, click **CONFIGURE** , and then copy the primary key for the **RootManageSharedAccessKey** policy to the clipboard

#### Task 2: Add a new WCF Endpoint with a relay binding

1. Open the **BlueYonder.Server** solution file from the **D:\AllFiles\Mod07\LabFiles\begin\BlueYonder.Server** folder.
2. Use the Package Manager Console window to install the **3.1.1** version of the **WindowsAzure.ServiceBus** NuGet package in the **BlueYonder.Server.Booking.WebHost**

   >**Note:** Other projects in the solution already reference a specific version of the **WindowsAzure.ServiceBus** NuGet package. Therefore, you are required to use the **Package Manager Console** to install the same version of the NuGet package.

3. In the **BlueYonder.Server.Booking.WebHost** project, change the endpoint configuration of the booking service:

    - Open the **Web.config** file
    - Locate the endpoint of the service named **BookingTcp** , and then change its **binding** attribute to **netTcpRelayBinding**.
    - Add an address attribute with the following value: **sb://BlueYonderServerLab07**** YourInitials. ****servicebus.windows.net/booking** (Replace _YourInitials_ with your initials).

4. Add a new endpoint behavior named **sbTokenProvider** to the endpoint behaviors configuration.

    - Add a new **&lt;endpointBehaviors&gt;** element to the **&lt;behaviors&gt;** element that is under the **&lt;system.serviceModel&gt;** section.
    - In the new **&lt;endpointBehaviors&gt;** element, adda new **&lt;behavior&gt;** element, and set its **name** attribute to **sbTokenProvider**.
    - In the new **&lt;behavior&gt;** element, add a **&lt;transportClientEndpointBehavior&gt;** behavior element to the configuration.
    - In the behavior element add a **&lt;tokenProvider&gt;** element, and in it, add a **&lt;sharedAccessSignature&gt;** element with the **keyName** attribute set to **RootManageSharedAccessKey** and the **key** attribute set to the access key of the new Service Bus you created.

   >**Note:** Visual Studio Intellisense uses built-in schemas to perform validations. Therefore, it will not recognize the **transportClientEndpointBehavior** behavior extension, and will display a warning. Disregard this warning. ** **

5. Locate the endpoint of the service, and then set it to use the new endpoint behavior.

    - Use the **behaviorConfiguration** attribute to connect the endpoint to the endpoint behavior named **sbTokenProvider**.

6. Add an **&lt;applicationInitialization&gt;** element to **&lt;system.webServer&gt;** section group, and then set the initialization page to **/Booking.svc**.

   >**Note:** Application initialization automatically sends requests to specified addresses after the Web application loads. Sending the request to the service will make the service host load and initiate the Service Bus connection.

7. Open IIS Manager, and then set the start mode of the **DefaultAppPool**.

    - Open the **Connections** pane and click the **Application Pools** node.
    - Open the **Advanced Settings** of the **DefaultAppPool**.
    - Set the **Start Mode** to **AlwaysRunning**.

   >**Note:** Setting the start mode to **AlwaysRunning** will load the application pool automatically after IIS loads. To use application initialization, the application pool must be running.

8. Enable the preload feature on the **BlueYonder.Server.Booking.WebHost**.

    - Open the **Advanced Settings** of the **BlueYonder.Server.Booking.WebHost** node.
    - Set the **Preload Enabled** option to **True**.

   >**Note:** When preload is enabled, IIS will simulate requests after the application pool starts. The list of requests is specified in the application initialization configuration that you already created.

9. Return to Visual Studio 2012, and then build the **BlueYonder.Server.Booking.WebHost** project.
10. Return to IIS, and then recycle the default application pool.

    - Click the **Application Pools** node in the **Connections** pane.
    - Select the **DefaultAppPool** and click **Recycle**.

#### Task 3: Configure the ASP.NET Web API back-end service to use the new relay endpoint

1. Open **BlueYonder.Companion.sln** from **D:\AllFiles\Mod07\LabFiles\begin\BlueYonder.Server** in a new Visual Studio 2012 instance.
2. Use the **Package Manager Console** window to install the **3.1.1** version of the **WindowsAzure.ServiceBus** NuGet package in the **BlueYonder.Companion.Host** project.
3. Configure the **BlueYonder.Companion.Host** project to consume the new relay endpoint.

    - In the **Web.config** file, locate the **&lt;client&gt;** section within the **&lt;system.serviceModel&gt;** section group.
    - Change the client endpoint configuration to use the **netTcpRelayBinding**
    - Set the value of the **address** attribute to the value **sb://BlueYonderServerLab07**** YourInitials. ****servicebus.windows.net/booking** (Replace _YourInitials_ with your initials).

4. Add a new endpoint behavior to the endpoint behaviors configuration.

    - Add a new **&lt;behaviors&gt;** element to the **&lt;system.serviceModel&gt;** section.
    - Add a new **&lt;endpointBehaviors&gt;** element to the **&lt;behaviors&gt;** element.
    - In the new **&lt;endpointBehaviors&gt;** , element adda new **&lt;behavior&gt;** element, and in it, add a **&lt;transportClientEndpointBehavior&gt;** behavior element.
    - In the behavior element add a **&lt;tokenProvider&gt;** element, and in it, add a **&lt;sharedAccessSignate&gt;** element with the **keyName** attribute set to **RootManageSharedAccessKey** and the **key** attribute set to the access key of the new Service Bus you created.

   >**Note:** Visual Studio Intellisense uses built-in schemas to perform validations. Therefore, it will not recognize **transportClientEndpointBehavior** behavior extension, and will display a warning. Disregard this warning. ** **

#### Task 4: Test the WCF service

1. Open the Azure portal ( **http://manage.windowsazure.com** )
2. Locate the **BlueYonderServerLab07**** YourInitials**(Replace _YourInitials_ with your initials) Service Bus namespace, and then verify that it contains the**booking** relay.
3. In the **BlueYonder.Companion** solution, bring back the call to the WCF service from the reservation controller.

    - In the **BlueYonder.Companion.Controllers** project, open **ReservationControll.cs** , and then locate the following comment.

      **// TODO: Lab 07, Exercise 1: Task 4.3: Bring back the call to the backend WCF service.**

     You can use the Task List window to locate the **TODO** comment.

    - Uncomment the call to the **CreateReservationOnBackendSystem** method.

4. Publish the **BlueYonder.Companion.Host.Azure** project. If you did not import your Azure subscription information yet, download your Azure credentials, and then import the downloaded publish settings file in the **Publish Windows Azure Application** dialog box.
5. Select the cloud service that matches the cloud service name you wrote down at the beginning of the lab, after running the setup script.
6. To finish the deployment process, click **Publish.**
7. In the **BlueYonder.Server** solution, open the **BookingService.cs** file from the **BlueYonder.BookingService.Implementation** project.
8. Place a breakpoint at the beginning of the **CreateReservation** method, and then start debugging the web application.
9. Sign in to the virtual machine **20487B-SEA-DEV-C** as **Admin** with the password **Pa$$w0rd**.
10. Open the **D:\AllFiles\Mod07\LabFiles\begin\BlueYonder.Companion.Client\BlueYonder.Companion.Client.sln** solution file.
11. In the **BlueYonder.Companion.Shared** project, open the **Addresses** class, and then set the **BaseUri** property to the Azure Cloud Service name you noted at the beginning of this lab.
12. Start the client application without debugging, and purchase a new trip to New York.

    - Use **Search** and start typing **New**.
    - Wait for the app to show list of flights from Seattle to New York.
    - Fill in the reservation form and click **Purchase**.

13. Go back to the **20487B-SEA-DEV-A** virtual machine to debug the WCF Web application.

    - Verify that you break on the WCF service code.
    - Continue running the application and verify that the client displays the new reservation.
    - Stop the WCF application debugging.

>**Results** : After completing this exercise, you should have successfully run the client app to book a flight, and have the ASP.NET Web API services, running in the Azure Web Role, communicate with the on-premises WCF services by using Azure Service Bus Relays.

### Exercise 2: Publishing Flight Updates to Clients by using Azure Service Bus Queues

#### Scenario

The Flight Manager Web application supports updating flight schedules with new departure times. In this exercise, you will add the push notifications feature to send flight updates directly to the clients who booked the flight being updated. Sending push notifications to multiple clients can take some time, so the notification part will be decoupled from the ASP.NET Web API service by using Service Bus Queues. In this exercise, you will create a Service Bus Queue and send two types of messages to it from the ASP.NET Web API service: client notification subscription, and flight update messages. In addition, you will create a background process running in an Azure Worker Role, which will receive messages from the queue and act according to each message type by either registering the client with the push notification server, or by sending flight update push notifications to registered clients.

The main tasks for this exercise are as follows:

1. Send flight update messages to the Service Bus Queue.

2. Create a Azure Worker role that receives messages from a Service Bus Queue.

3. Handle the subscription and update messages.

4. Test the Service Bus Queue with flight update messages.

#### Task 1: Send flight update messages to the Service Bus Queue

1. Open the Microsoft Azure portal ( **http://manage.windowsazure.com** )

    - Open the Service Bus you created in the previous exercise.
    - Click **CONNECTION INFORMATION** to open the **ACCESS CONNECTION INFORMATION** dialog box.
    - Copy the value of the **CONNECTION STRING** text box.

2. Return to the **BlueYonder.Companion** solution in Visual Studio 2012, and then add a string setting to the web role to store the Service Bus connection string.

    - Name the new setting **Microsoft.ServiceBus.ConnectionString** , and then set its value to the connection string you noted in the previous step.

3. Open the **ServiceBusQueueHelper** class located in the **BlueYonder.Companion.Controllers** project, and then implement the **ConnectToQueue** method.

    - Create a Service Bus namespace manager object by using the connection string of the Service Bus.
    - Use the **CloudConfigurationManager.GetSetting** to retrieve the connection string.
    - To create the namespace manager object, use the **CreateFromConnectionString** method of the **NamespaceManager** class.
    - Check if the Queue exists and create it by using the **CreateQueue** API if necessary.
    - Return a new **QueueClient** object for the queue by using the **CreateFromConnectionString** method of the **QueueClient** class.

   >**Note:** The Queue name is stored in a static variable named **QueueName** , and has the value of **FlightUpdatesQueue**

4. In the **FlightsController** class, add a static field for the **QueueClient** object.

    - Call the method you previously created in a static constructor to set the object.

5. In the **Put** method, after saving the changes made to the flight schedule, set the **FlightId** property of the **updatedSchedule** variable to the **id** parameter containing the updated flight id.

    - Create a new **BrokeredMessage** object with the updated schedule as the message body, set the **ContentType** property of the message to **UpdatedSchedule** , and then send the message to the queue.

6. Review the **Register** method of the **NotificationsController** class. It follows the same pattern of creating a **QueueClient** object in the static constructor, and then sending the update messages by using the **BrokeredMessage** is applied to this controller.

   >**Note:** The **Register** method subscribes clients to flight update notifications. When a flight update message is sent to the queue, every subscribed client waiting for that flight will be notified by using the Windows Push Notification Services (WNS).

#### Task 2: Create a Azure Worker role that receives messages from a Service Bus Queue

1. Create a new Worker Role named **BlueYonder.Companion.WNS.WorkerRole** in the **BlueYonder.Companion.Host.Azure** project.

    - Use the **Worker Role with Service Bus Queue** template when creating the role.

2. Copy the **Microsoft.ServiceBus.ConnectionString** setting from the **BlueYonder.Companion.Host** web role to the **BlueYonder.Companion.WNS.WorkerRole** worker role settings.

   >**Note:** The **BlueYonder.Companion.WNS.WorkerRole** role already contains the **Microsoft.ServiceBus.ConnectionString** setting. You only need to copy the connection string value from the **BlueYonder.Companion.Host** web role.

Task 3: Handle the subscription and update messages

1. Add the **BlueYonder.Companion.WNS** project from the **D:\Allfiles\Mod07\LabFiles\begin\BlueYonder.Server\BlueYonder.Companion.WNS** folder to the solution.

   >**Note:** The **BlueYonder.Companion.WNS** project includes code that handles WNS subscriptions and notifications. WNS is out of scope of this course; however, you can open the project&#39;s code and observe how WNS is used.

2. Copy the database connection string from the **BlueYonder.Companion.Host** to the **BlueYonder.Companion.WNS.WorkerRole** project:

    - Open the **Web.config** of the **BlueYonder.Companion.Host** project, and then copy the entire **&lt;connectionStrings&gt;**
    - Place the copied text in the **App.Config** file of the **BlueYonder.Companion.WNS.WorkerRole** project, under the **&lt;configuration&gt;** section.

3. In the **BlueYonder.Companion.WNS.WorkerRole** project, add the following application settings elements to the **App.config** file.

  ```cs
       <add key="ClientSecret" value="1r7Bt7zllZLfDM4W4Q7BxAZEze2qnvuN" />
       <add key="PackageSID" value="ms-app://s-1-15-2-1252400722-2342768715-2725817281-1266214681-2802664595-2493784738-901281077" />
```
   >**Note:** You can find the above configuration in the **WnsConfiguration.xml** file, under the lab&#39;s **Assets** folder.  
   The **ClientSecret** and **PackageSID** settings were retrieved by the Windows 8 client team during the upload process of the client app to the windows store.

4. In the **BlueYonder.Companion.WNS.WorkerRole** project, add reference to the following projects:

    - **BlueYonder.Companion.WNS**
    - **BlueYonder.Companion.Entities**
    - **BlueYonder.DataAccess.Interfaces**
    - **BlueYonder.DataAccess**
    - **BlueYonder.Entities**

5. Add the **MessageHandler.cs** file from the lab&#39;s **Assets** folder to the **BlueYonder.Companion.WNS.WorkerRole** project.

   >**Note:** The **MessageHandler** class contains the code to subscribe clients to WNS and send notifications to clients when their flights are rescheduled.

6. In the **BlueYonder.Companion.WNS.WorkerRole** project, locate the **OnStart** method of the **WorkerRole** class, and then add a call at the beginning of the method to the **WNSManager.Authenticate** method.
7. In the **WorkerRole** class, change the value of the **QueueName** constant from **ProcessingQueue** to **FlightUpdatesQueue**.
8. In the **Run** method, add code after the **// Process the message** comment, to handle received messages, according to the value of the received message **ContentType** property:

    - **Subscription** : Use the **receivedMessage.GetBody&lt;T&gt;** generic method to retrieve the content of the message as a **RegisterNotificationsRequest** object, and call the **MessageHandler.CreateSubscription**.
    - **UpdatedSchedule**. Use the **receivedMessage.GetBody&lt;T&gt;** generic method to retrieve the content of the message as a **FlightSchedule** object and call the **MessageHandler.Publish** method.

9. Publish the **BlueYonder.Companion.Host.Azure** project to Azure.

#### Task 4: Test the Service Bus Queue with flight update messages

1. Place the two virtual machine windows such that you work in the virtual machine **20487B-SEA-DEV-A** and see the right-hand side of **20487B-SEA-DEV-C**.
2. Run the client app in the **20487B**** -SEA-DEV-C **virtual machine. The trip you purchased in the previous exercise will show in the** Current Trip** list. Note the date of the trip.
3. Leave the client app running and return to the **20487B-SEA-DEV-A** virtual machine.
4. Open the **Companion.FlightsManager** solution file from the **D:\AllFiles\Mod07\LabFiles\begin\BlueYonder.Server** folder in a new Visual Studio 2012 instance.
5. Open the **web.config** file from the **BlueYonder.FlightsManager** project, in the **&lt;appSettings&gt;** section, locate the **webapi:BlueYonderCompanionService** key, and then update the **{CloudService}** string to the Azure Cloud Service name you noted at the beginning of the lab.
6. Run the **BlueYonder.FlightsManager** web application, find the flights from Seattle to New York, and change the departure time of your purchased trip to **9:00 AM**.

    - Verify that you see a toast notification in the client app in the **20487B-SEA-DEV-C** virtual machine (it might take the notification a few seconds to appear).

>**Results** : After completing this exercise, you should have run the Flight Manager Web application, updated the flight departure time of a flight you booked in advance in your client app, and received Windows push notifications directly to your computer.