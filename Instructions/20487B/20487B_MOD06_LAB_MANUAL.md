# Module 6: Hosting Services

## Lab: Hosting Services

#### Scenario

Blue Yonder Airlines&#39; estimated research results show that in the next six months, more than 100,000 people will start to use their new Travel Companion app. Because the company does not yet have a web data center, they decided not to buy additional servers to support the new user load. Instead, they are moving their external service layer to Azure. By using Azure, they can scale-out as required, as more people start to use the new app. In this lab, you move the WCF booking service from its simple console host to IIS, and move the ASP.NET Web API services from their on-premises IIS hosting environment to a web role in Azure.

In addition, Blue Yonder Airlines wants to separate the deployment of the flights management web application from the Travel Companion back-end service. Because the web application is a small application and does not require many resources, it was decided that the application should be deployed to an Azure Web Site. In this lab, you deploy the booking management web application to an Azure Web Site.

#### Objectives

After you complete this lab, you will be able to:

- Host the WCF booking service in IIS.
- Host the destinations, flight schedules, and booking ASP.NET Web API services in an Azure web role.
- Host the booking management web application in an Azure Web Site.

#### Lab Setup

Estimated Time: **45 minutes**

Virtual Machine: **20487B-SEA-DEV-A** and **20487B-SEA-DEV-C**

User name: **Administrator** and **Admin**

Password: **Pa$$w0rd** and **Pa$$w0rd**

For this lab, you will use the available virtual machine environment. Before you begin this lab, you must complete the following steps:

1. On the host computer, click **Start** , point to **Administrative Tools** , and then click **Hyper-V Manager**.

2. In Hyper-V Manager, click **MSL-TMG1** , and in the **Actions** pane, click **Start**.
3. In Hyper-V Manager, click **20487B-SEA-DEV-A** , and in the **Actions** pane, click **Start**.
4. In the **Actions** pane, click **Connect**. Wait until the virtual machine starts.
5. Sign in using the following credentials:

  - User name: **Administrator**
  - Password: **Pa$$w0rd**

6. Verify that you received credentials to sign in to the Azure portal from your training provider; these credentials and the Azure account will be used throughout the labs of this course.

This course uses Visual Studio 2012, which still uses the old names &quot;Windows Azure&quot; and &quot;Azure Web Sites&quot;. Today, the correct names are &quot;Microsoft Azure&quot; and &quot;Azure Web Apps&quot; respectively. Therefore, you may see a mix of both old and new names, where instructions refer to both Visual Studio 2012, and the current Microsoft Azure portal.

### Exercise 1: Hosting the WCF Services in IIS

#### Scenario

To host the WCF services in IIS, you will start by creating a web application. After you create the web application and add the proper references, you will configure the web application to have the same service configuration as the self-hosted services. After you finish preparing the web application, you will host it in IIS and configure IIS to support both HTTP and NET.TCP endpoints.

The main tasks for this exercise are as follows:

1. Create a web application project

2. Configure the web application project to use IIS

3. Configure the web applications to support NET.TCP

#### Task 1: Create a web application project

1. Run the **setup.cmd** file from **D:\AllFiles\Mod06\LabFiles\Setup**.

2. Open the **BlueYonder.Server** solution file from the **D:\AllFiles\Mod06\LabFiles\begin\BlueYonder.Server** folder.
3. To the **BlueYonder.Server** solution, add a new **ASP.NET Empty Web Application** project named  **BlueYonder.Server.Booking.WebHost**.
4. Use the **Package Manager Console** window to install version **6.1.3** of the **EntityFramework** NuGet package, in the  **BlueYonder.Server.Booking.WebHost** project.

  >**Note:** Other projects in this solution use version 6.1.3 of the EntityFramework NuGet package. To prevent assembly load failures, you are required to use the **Package Manager Console** , to install that specific package version. 

5. In the **BlueYonder.Server.Booking.WebHost** project, add references to the **System.ServiceModel** assembly and to the following projects:

  - **BlueYonder.BookingService.Contracts**
  - **BlueYonder.BookingService.Implementation**
  - **BlueYonder.DataAccess**
  - **BlueYonder.Entities**

6. Copy the **FlightScheduleDatabaseInitializer.cs** file from the booking service self-host project to the new web application project.
7. Add a **Global.asax** file to the new web application, and in the **Application\_Start** method, add the required database initialization code.

    a. Initialize the database by using the database initializer you copied from the self-host project.    
    b. Refer to the database initialization code in the **Program.cs** file that is in the **BlueYonder.Server.Booking.Host** project.

#### Task 2: Configure the web application project to use IIS

1. Copy the entire content of the **App.config** file from the **BlueYonder.Server.Booking.Host** project and paste it over the content of the **Web.config** file in the **BlueYonder.Server.Booking.WebHost** project.

2. Within the **&lt;system.serviceModel&gt;** section group, add a **&lt;serviceHostingEnvironment&gt;** element with service activation configuration for the Booking service, and use the relative address **Booking.svc**.

    a. Declare a **&lt;serviceActivations&gt;** section inside the **&lt;serviceHostingEnvironment&gt;** section.    
    b. Inside the **&lt;serviceActivations&gt;** section, add an **&lt;add&gt;** tag with the following parameters.

   | Attribute | Value |
   | --- | --- |
   | service | BlueYonder.BookingService.Implementation.BookingService |
   | relativeAddress | Booking.svc |

  >**Note:** You can also refer to Lesson 1, &quot;Hosting Service On-Premises&quot;, Topic 2, &quot;Hosting WCF Service in IIS&quot; in this module, for a code example. ** **

3. Add a **&lt;system.web&gt;** element to the **&lt;configuration&gt;** section, and in it add a **&lt;compilation&gt;** element and a **&lt;httpRuntime&gt;** element.

    a. In the **&lt;compilation&gt;** element, set the debug attribute to **true** and the **targetFramework** attribute to **4.5**.  
    b. In the **&lt;httpRuntime&gt;** element, set the **targetFramework** attribute to **4.5**.
    

  >**Note:**.NET 4 and 4.5 use the same .NET Framework version for the IIS application pool. If you do not specify the target framework of your Web application in the **Web.config** file, the default version will be .NET 4.

4. Remove the addresses used in the service metadata behavior and the service endpoint.

    a. Remove the **httpGetUrl** attribute from the **&lt;serviceMetadata&gt;** element.    
    b. Remove the **address** attribute from the **&lt;endpoint&gt;** element.

  >**Note:** IIS uses the address of the web application to create the service metadata address and the service endpoint address.

5. In the **BlueYonder.Server.Booking.WebHost** project&#39;s properties, on the **Web** tab, change the server from IIS Express to the local IIS Web server, and then build the solution.

#### Task 3: Configure the web applications to support NET.TCP

1. Open IIS Manager from the Start screen and select the **Default Web Site** from the **Connections** pane. Verify that the  **net.tcp** binding is listed in the **Bindings** list.

  >**Note:** The site bindings configure which protocols are supported by the IIS Web Site and which port, host name, and IP address are used with each protocol.

2. Open the **Advanced Settings** dialog box of the **BlueYonder.Server.Booking.WebHost** web application, and set both to enable the **http** and **net.tcp** protocols. Use a comma to separate the values.

  >**Note:** In addition to adding net.tcp to the site bindings list, you also need to enable net.tcp for each Web application you host in IIS. By enabling net.tcp, WCF will automatically create an endpoint with NetTcpBinding.

3. Connect to the WCF service through the WCF Test Client application, and verify if the application is able to retrieve metadata from both services.

    a. Open the WCF Test Client application from **D:\AllFiles**.    
    b. Connect to the address **http://localhost/BlueYonder.Server.Booking.WebHost/Booking.svc**    
    c. Wait until you see the service and endpoints tree in the pane to the left.  

>**Results**: You will be able to run the WCF Test Client application and verify if the services are running properly in IIS.

### Exercise 2: Hosting the ASP.NET Web API Services in an Azure Web Role

#### Scenario

Before you deploy the ASP.NET Web API services to Azure, you need to create a cloud service where you will deploy the web application and an SQL Database server for the application to use. After you create the cloud service, you will upload several certificates to it, so the web application can use these certificates when calling the on-premises WCF service.

After you create the components in Azure, you will create the cloud project in Visual Studio 2012, and configure it to deploy the ASP.NET Web API web application to an Azure web role. Before deploying the application to Azure, you will test it locally with the Azure compute emulator, and after you verify it is running properly, you will deploy it to Azure.

The main tasks for this exercise are as follows:

1. Create a new SQL database server and a new cloud service

2. Add a cloud project to the solution

3. Deploy the cloud project to Azure

4. Test the cloud service against the client application

#### Task 1: Create a new SQL database server and a new cloud service

1. Open the Microsoft Azure portal ( **http://manage.windowsazure.com** )

2. Create a new **SQL Database** Server.

    a. On the **SQL DATABASES** page, in the **SERVERS** tab, click **ADD**.  
    b. Use the sign in name **BlueYonderAdmin** and the password **Pa$$w0rd**.  
    c. Select a **Region** that is closest to your location and create the SQL Database Server.  
    d. Ensure that the **ENABLE LATEST SQL DATABASE UPDATE (V12)** option is selected.  
    e. Note the name of the new SQL Database Server.

3. Configure the SQL Database server to allow access from any IP address by creating a rule with the following settings:

   - RULE NAME: **OpenAllIPs**
   - START IP ADDRESS: **0.0.0.0**
   - END IP ADDRESS: **255.255.255.255**

  >**Note:** As a best practice, you should allow only your IP address, or your organization&#39;s IP address range to access the database server. However, in this course, you will use this database server for future labs, and your IP address might change in the meanwhile, therefore you are required to allow access from all IP addresses.

4. Create a new Azure Cloud Service named **BlueYonderCompanionYourInitials** (_YourInitials_ contains your initials). Choose a region closest to your location for the new cloud service.
5. Upload the **CloudApp** certificate from the **D:\AllFiles\certs** folder to the new cloud service.

    a. Open the new cloud service configuration and then open the **CERTIFICATES** tab.  
    b. Use the password **1** to open the certificate file.

  >**Note:** In this lab, the ASP.NET Web API services are accessible through HTTP and HTTPS. To use HTTPS, you need to upload a certificate to the Azure cloud service.

6. Open the **BlueYonder.Companion.sln** solution file from the **D:\AllFiles\Mod06\LabFiles\begin\BlueYonder.Server** folder in a new Visual Studio 2012 instance.
7. In the **BlueYonder.Companion.Host** project, edit the connection string within the **Web.config** file, and update the SQL Database server name.

    a. Locate the two **{ServerName}** placeholders in the **connectionString** attribute  
    b. Replace the placeholders with the SQL database server name from the portal.

8. Locate the client endpoint configuration for the Booking service, and change its address to point to the web-hosted service. Use the address: **net.tcp://localhost/BlueYonder.Server.Booking.WebHost/Booking.svc**.

#### Task 2: Add a cloud project to the solution

1. Add a new cloud service project with an Azure web role for the **BlueYonder.Companion.Host** project:

    a. Right-click the **BlueYonder.Companion.Host** project and select **Add Windows Azure Cloud Service Project**.  
    b. Verify that the cloud project contains a web role named **BlueYonder.Companion.Host.Azure**.
    

  >**Note:** You can achieve the same result by adding a new Azure Cloud Service project, to the solution, and then manually adding a Web Role Project from an existing project.

2. Add the HTTPS certificate to the Web Role.

    - Use the **Certificates** tab in the role&#39;s **Properties** window, to add a certificate according to the following settings.

   | **Name** | **Value** |
   | --- | --- |
   | Name | BlueYonderCompanionSSL |
   | Store Location | LocalMachine |
   | Store Name | My |
   | Thumbprint | Click the ellipsis and then select the **BlueYonderSSLCloud** certificate |

3. On the **Certificates** tab, change the **Service Configuration** to **Local** , and then change the **BlueYonderCompanionSSL**  certificate from **BlueYonderSSLCloud** to **BlueYonderSSLDev**. Change the service configuration back to **All Configurations** when you are done.

  >**Note:** SSL certificates contain the name of the server so that clients can validate the authenticity of the server. Therefore, there are different certificates for the local deployment, and for the cloud deployment. ** **

4. Add an HTTPS endpoint to the web role.

    - Use the **Endpoints** tab in the role&#39;s **Properties** window, to add an endpoint according to the following settings:

   | **Name** | **Value** |
   | --- | --- |
   | Name | Endpoint2 |
   | Type | Input |
   | Protocol | https |
   | Public Port | 443 |
   | SSL Certificate Name | BlueYonderCompanionSSL |

5. Run the ASP.NET Web API project with the Azure compute emulator.

   - After the two web browsers open, verify they use the addresses http://127.0.0.1:81 and https://127.0.0.1:444.
   

  >**Note:** The endpoint configuration of the role uses ports 80 and 443 for the HTTP and HTTPS endpoint. However, the local IIS Web server already uses those ports, so the emulator needs to uses different ports.

6. Sign in to the virtual machine **20487B-SEA-DEV-C** as **Admin** with the password **Pa$$w0rd**.
7. Open the **BlueYonder.Companion.Client** solution file from the **D:\AllFiles\Mod06\LabFiles\begin\BlueYonder.Companion.Client**  folder.
8. The client app is already configured to use the Azure compute emulator. Run the client and verify that it can connect to the emulator by searching for a flight to New York and verifying you see a list of flights.

  >**Note:** Normally, the Azure Emulator is not accessible from other computers on the network. For purposes of testing this lab from a Windows 8 client, a routing module was installed on the server&#39;s IIS, routing the incoming traffic to the emulator.

9. Switch back to the **20487B-SEA-DEV-A** virtual machine.

#### Task 3: Deploy the cloud project to Azure

1. On the **View** menu, open the **Task List** window.

2. Switch to **Comments** and search for TO-DO items. Double-click each comment and look at the disabled WCF calls:

   - **UpdateReservationOnBackendSystem**
   - **CreateReservationOnBackendSystem**
   

  >**Note:** Prior to the deployment of the cloud project to Azure, all the on-premises WCF calls were disabled.  
  These include calls from the Reservation Controller class and the Trips Controller class.  
  After you deploy the ASP.NET Web API project to Azure, it cannot call the on-premises WCF service, so for now, the WCF Service calls are disabled. In Module 7, &quot;Windows Azure Service Bus&quot; in Course 20487, you will learn how a cloud application can connect to an on-premises service.

3. Use Visual Studio 2012 to publish the **BlueYonder.Companion.Host.Azure** project to the cloud service you created.

    a. Open the **Publish Windows Azure Application** dialog box for the cloud project.  
    b. Click the **Sign in to download credentials** link.  
    c. Save the publish settings file and return to Visual Studio 2012  
    d. Click **Import** , select the publish settings file you saved, and move to the next step of the publish wizard.

4. Select the **BlueYonderCompanionYourInitials** (_YourInitials_ contains your name&#39;s initials)Azure Cloud Service

    a. If required, create a new Azure Storage account named **byclyourinitials** (_yourinitials_ contains your name&#39;s initials, in lower-case) in a region closest to your location.  
    b. On the **Advanced Settings** tab, set the name of the new deployment to **Lab6** , and ensure that the **Append current date and time** check box is not selected.

  >**Note:** The abbreviation **bycl** stands for Blue Yonder Companion Labs. An abbreviation is used because storage account names are limited to 24 characters. The abbreviation is in lower-case because storage account names are in lower-case. Windows Azure Storage is covered in depth in Module 9, &quot;Windows Azure Storage&quot; in Course 20487.

5. Start the deployment process by clicking **Publish**. This might take several minutes to complete.

#### Task 4: Test the cloud service against the client application

1. Go back to the **20487B-SEA-DEV-C** virtual machine.
2. Replace the address used to communicate with the server:

    a. In the **BlueYonder.Companion.Shared** project, open the **Addresses** class and in the **BaseUri** property, replace the address of the emulator with the cloud service address you created earlier.  
    b. Use the form **https://blueyondercompanionYourInitials.cloudapp.net/** (replace _YourInitials_ with your initials).

3. Run the client app and search for flights to New York. Verify the client application is able to connect to the ASP.NET Web API Web application hosted in Windows Azure and retrieve the list of flights.

>**Results**: You will verify the application works locally in the Azure compute emulator, and then deploy it to Azure and verify it works there too.

### Exercise 3: Hosting the Flights Management Web Application in an Azure Web App

#### Scenario

In this exercise, you will deploy the flights manager web application to an Azure Web App. The first step will be to create an Azure Web App. After you create the Web App, you will download the publish settings file, which you will then use in Visual Studio 2012 to populate all the publish settings required by the publish process (such as destination server, user name, and password).

The main tasks for this exercise are as follows:

1. Create new Web App in Azure

2. Upload the Flights Management web application to the new Web App by using the Microsoft Azure portal

#### Task 1: Create new Web App in Azure

1. Go back to the **20487B-SEA-DEV-A** virtual machine, and open the Microsoft Azure portal ( **http://manage.windowsazure.com** ).

2. Create a new Web App named: **BlueYonderCompanionYourInitials** (_YourInitials_ contains your initials).

    a. Use **NEW** and then **QUICK CREATE** button.  
    b. Create a new **APP SERVICE PLAN** and select a **Region** that is closest to your location.  
    c. Click **CREATE WEB APP** to create the web site.

3. Open the new Web App&#39;s **Dashboard** page, and download the publish profile file.

  >**Note:** The publishing profile file includes the information required to publish a Web application to the Web App. This is an alternative publish method to downloading the subscription file, as shown in Lesson 2, &quot;Hosting Services in Windows Azure&quot;, Demo 1, &quot;Hosting in Windows Azure&quot; in Course 20487. The difference is that by importing the subscription file, you can publish to any of the Web Apps managed by your Azure subscription, whereas importing the publish profile file of a Web App will only allow you to publish to that specific Web App.

#### Task 2: Upload the Flights Management web application to the new Web App by using the Microsoft Azure portal

1. Open the **BlueYonder.Companion.FlightsManager** solution file from the **D:\AllFiles\Mod06\LabFiles\begin\BlueYonder.Server** folder in a new Visual Studio 2012 instance.

2. In the **BlueYonder.FlightsManager** project, open the **web.config** file, and edit the **&lt;appSettings&gt;** section to substitute the **{YourInitials}** placeholder with the initials you have used when you created the Azure cloud service earlier.
3. Publish the **BlueYonder.FlightsManager** project.

   - Use the publish profile file you downloaded in the previous task.
   - After the deployment completes, a browser will automatically open, showing the deployed site.

4. Verify that you can see flight schedules from **Paris** to **Rome** , indicating that the application was able to retrieve information from the web role.

>**Results**: After you publish the flights manager web application, you will open the web application in a browser and verify if it is working properly and is able to communicate with the web role you deployed in the previous exercise.

Perform the following steps to apply the **StartingImage** snapshot:

1. On the host computer, click **Start**, point to **Administrative Tools**, and then click **Hyper-V Manager**.
2. In Hyper-V Manager, click **20487B-SEA-DEV-A**, and in the **Snapshots** pane, right-click **StartingImage**, and then click **Apply**.
3. In the **Apply Snapshot** dialog box, click **Apply**.
4. Repeat Step 2 for all the virtual machines that you used in this lab. (excluding **MSL-TMG1**).

©2016 Microsoft Corporation. All rights reserved.

The text in this document is available under the  [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are  **not**  included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
