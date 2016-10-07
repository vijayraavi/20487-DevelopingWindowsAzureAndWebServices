# Module 11: Identity Management and Access Control

# Lab: Identity Management and Access Control

#### Scenario

One of the features that customers have requested for the Travel Companion app is the ability to access a list of their booked flights from multiple devices. After investigating the current behavior of the app, you found that it currently stores the booking information for each device and that each booking has the device ID that was used when booking. Therefore, it was not possible to view the bookings from other devices.

To address this issue, you decided that users should be able to log on to the app, so that the booking is saved for a user and not for a device. Users would then be able to log on to the app from other devices and still see their future and past flights.

To reduce the amount of work required to manage user identities and passwords, you decided that the authentication process will be accomplished by using known identity providers, such as Windows Live ID, with the help of Microsoft Azure ACS. In this lab, you will configure ACS to support user authentication with Windows Live ID, and configure the ASP.NET Web API service and client app to support this authentication process.

#### Objectives

After completing this lab, you will be able to:

- Configure Windows Azure ACS.
- Configure an ASP.NET Web API service to trust the ACS.
- Configure a Windows Store app to use federated authentication.

#### Lab Setup

Estimated Time: **60 Minutes**.

Virtual Machine: **20487B-SEA-DEV-A**, **20487B-SEA-DEV-C**

User name: **Administrator**, **Admin**

Password: **Pa$$w0rd**, **Pa$$w0rd**

For this demo, you will use the available virtual machine environment. Before you begin this demo, you must complete the following steps:

1. On the host computer, click **Start**, point to **Administrative Tools**, and then click **Hyper-V Manager**.
2. In Hyper-V Manager, click **MSL-TMG1**, and in the **Action** pane, click **Start**.
3. In Hyper-V Manager, click **20487B-SEA-DEV-A**, and in the **Action** pane, click **Start**.
4. In the Action pane, click **Connect**. Wait until the virtual machine starts.
5. Sign in using the following credentials:

    - User name: **Administrator**
    - Password: **Pa$$w0rd**

6. Return to Hyper-V Manager, click **20487B-SEA-DEV-C**, and in the **Action** pane, click **Start**.
7. In the Action pane, click **Connect**. Wait until the virtual machine starts.
8. Sign in using the following credentials:

    - User name: **Admin**
    - Password: **Pa$$w0rd**

9. Verify that you received credentials to sign in to the Azure portal from you training provider, these credentials and the Azure account will be used throughout the labs of this course.

In this lab, you will install NuGet packages. It is possible that some NuGet packages will have newer versions than those used when developing this course. If your code does not compile, and you identify the cause to be a breaking change in a NuGet package, you should uninstall the NuGet package and instead, install the old version by using Visual Studio&#39;s Package Manager Console window:

1. In Visual Studio, on the **Tools** menu, point to **Library Package Manager**, and then click **Package Manager Console**.
2. In **Package Manager Console**, enter the following command and then press Enter.  
**install-package PackageName -version PackageVersion -ProjectName ProjectName**  
(The project name is the name of the Visual Studio project that is written in the step where you were instructed to add the NuGet package).

3. Wait until Package Manager Console finishes downloading and adding the package.

The following table details the compatible versions of the packages used in the lab:

| Package name | Package version |
| --- | --- |
| Wif.Swt | 0.0.1.4 |

### Exercise 1: Configuring Microsoft Azure ACS

#### Scenario

Before you configure the ASP.NET Web API services for claims-based identities and authentication, you need to create your STS and configure it for your Web application. In this exercise, you will create an ACS namespace and in it, configure an RP for your application.

The main tasks for this exercise are as follows:

1. Create a new ACS namespace.

2. Configure the Relying Party application.

3. Create a rule group.

#### Task 1: Create a new ACS namespace

1. In the **20487B-SEA-DEV-A** virtual machine, run the **setup.cmd** script from **D:\AllFiles\Mod11\LabFiles\Setup**. Provide the information according to the instructions and write down the name of the cloud service the script outputs.

   >**Note:** You might see warnings in yellow indicating a mismatch in the versions and obsolete settings. These warnings might appear if there are newer versions of Azure PowerShell cmdlets. If these warnings are followed by a red error message, please inform the instructor, otherwise you can ignore them.

2. Open the Azure portal at (**http://manage.windowsazure.com**).
3. Create a new ACS namespace named **BlueYonderCompanion_YourInitials_** (_YourInitials_ will contain your initials). When you create the namespace, select the region closest to your location.

#### Task 2: Configure the Relying Party application

1. Open the ACS portal for the new namespace.

    - To view the list of ACS namespaces, click **ACTIVE DIRECTORY** in the navigation pane, and then  click the **ACCESS CONTROL NAMESAPCES** tab.

2. Create a new **Relying Party** with the following information:

    - Name: **BlueYonderCloud**
    - Realm: **urn:blueyonder.cloud**
    - Return URL: **https://CloudServiceName.cloudapp.net/federationcallback** (_CloudServiceName_ is the name of the cloud service you wrote down in the beginning of the lab while running the setup script)
    - Token format: **SWT**
    - Token signing key: Generate a new token.

#### Task 3: Create a rule group

- Generate a rule group for the **BlueYonderCloud** RP. The rule group should propagate all the incoming claims from the identity providers to the RP.

    - To generate a rule group, in the ACS portal, open the **Rule Groups** page, select the default rule group for **BlueYonderCloud**, and then click the **Generate** link.
    - Make sure you generate the rules for the Windows Live ID identity provider.

>**Results**: After completing this exercise, you should have successfully created a new ACS namespace and configured an RP for the ASP.NET Web API services. Also you should have tested the RP configuration.

### Exercise 2: Integrating ACS with the ASP.NET Web API Project

#### Scenario

Now that you have created the ACS namespace, the next step is to configure your ASP.NET Web API services to support authentication and authorization of claims-based identities. ASP.NET Web API does not provide out-of-the-box solution for claims-based identities. Therefore, you will use third-party NuGet packages to add that support.

In this exercise, you will implement SWT token authentication with the **Thinktecture.IdentityModel** NuGet package, and SWT claims extraction with the **Wif.Swt** NuGet package. In addition, you will configure several of the API controllers to authorize the user.

The main tasks for this exercise are as follows:

1. Add the Thinktecture.IdentityModel NuGet package.

2. Add token validation to ASP.NET Web API.

3. Add a federation callback controller.

4. Update the Routing with the New Authentication Configuration.

5. Decorate the ASP.NET Web API controllers for authorization.

#### Task 1: Add the Thinktecture.IdentityModel NuGet package

1. Open the **D:\AllFiles\Mod11\LabFiles\begin\BlueYonder.Server\BlueYonder.Companion.sln** solution file in **Visual Studio 2012**.
2. Install version 2.2.1 of the **ThinkTecture.IdentityModel** NuGet package in the **BlueYonder.Companion.Host** project.

    - To install a specific version of a NuGet package, first open the **Packager Manager Console** window (on the **View** menu, under **Other Windows** ).
    - In **Package Manager Console**, type the following command, and then press Enter:

  ```cs
          install-package ThinkTecture.IdentityModel -version 2.2.1 -ProjectName BlueYonder.Companion.Host
```
   >**Note:** The last known version of the **ThinkTecture.IdentityModel** NuGet package that supports the SWT token is 2.2.1. Therefore, you need to use the Package Manager Console to install this NuGet package, rather than using the **Manage NuGet Packages** dialog box.

#### Task 2: Add token validation to ASP.NET Web API

1. In the **BlueYonder.Companion.Host.Azure** project, open the properties of the **BlueYonder.Companion.Host** web role, and add a string setting to store the issuer (STS) name of the token.

    - Name the new setting **ACS.IssuerName**, and set its value to **https://BlueYonderCompanion****_YourInitials_****.accesscontrol.windows.net/** (_YourInitials_ will contain your initials).
    - Make sure there are no spaces at the end of the string.

2. Add another string setting to the web role to store the realm of the relying party.

    - Name the new setting **ACS.Realm**, and set its value to **urn:blueyonder.cloud**.

3. Add another string setting to the web role to store the token signing key of the relying party you used. Name the new setting **ACS.SigningKey**.

    - To find the token siging key, in the ACS portal, open the **Certificates and Keys** page, click **BlueYonderCloud**, and then click **Show Key**.

4. Create a new folder named **Authentication** in the **BlueYonder.Companion.Host** project.
5. Create a new class named **AuthenticationConfig** in the new folder you created.
6. In the **AuthenticationConfig** class, create a static method named **CreateConfiguration**, which returns an object of type **AuthenticationConfiguration**, and start implementing the method.

    - Create a local variable named **config**, of type **AuthenticationConfiguration**.
    - Initialize the new variable and return its value from the method;

7. Continue implementing the method by retrieving the **ACS.IssuerName**, **ACS.Realm**, and **ACS.SigningKey** settings you created in the web role settings.

    - Use the **Microsoft.Azure.CloudConfigurationManager** class to access the role&#39;s settings.
    - Use the **Trim** method on each of the retrieved strings to remove any whitespace which might have been added to them.
    - Make sure you add the code before the **return** statement.

8. Continue implementing the method by adding a new SWT token to the configuration.

    - Call the config&#39;s **AddSimpleWebToken** method to add a definition for the SWT token.
    - Set the method&#39;s **issuer** and **signingKey** parameters to the strings you retrieved from the role&#39;s settings.
    - Set the method&#39;s **audience** parameter to the **ACS.Realm** string you retrieved from the role&#39;s settings.

      >**Note:** Realm is the unique identifier of your RP. Audience refers to the realm of the RP that redirected the client to the STS. In most cases the realm and audience are the same, because you are redirected back to the application you came from. There are scenarios where the RP that got the token is not the same RP that requested the token.

    - Set the method&#39;s **options** parameter to the following code.

  ```cs
          AuthenticationOptions.ForAuthorizationHeader("OAuth"));
```
   >**Note:** The **AuthenticationOptions.ForAuthorizationHeader** method sets the value of the HTTP Authorization header that is added to unauthorized responses. The **OAuth** value specifies that OAuth authentication should be used.

   - Make sure you add the simple web token configuration before the **return** statements.

9. Set the default authentication scheme to **OAuth** , and enable the use of session tokens.

    - Set the config&#39;s **DefaultAuthenticationScheme** property to **OAuth**. This setting will define the authentication scheme returned for requests without any HTTP Authorization header.
    - Set the config&#39;s **EnableSessionToken** property to **true** to support client requests for session tokens. Clients can use session token instead of including the SWT token in each request. Session tokens are usually stored in cookies.
    - Make sure you set the two properties before the **return** statement.

#### Task 3: Add a federation callback controller

1. Install the **Simple Web Token Support for Windows Identity Foundation** NuGet package in the **BlueYonder.Companion.Host** project.

    - You can locate the NuGet package by searching for **WIF.SWT**.

2. Add a new class named **FederationCallbackController** to the **BlueYonder.Companion.Host** project. Set the class to derive from **ApiController**, and set the access level of the class to **public**.
3. In the **FederationCallbackController**, add a method to handle POST requests which hold the authentication token.

    - Name the method **Post**, and set its return type to **HttpResponseMessage**.
    - Use the **Request.CreateResponse** method to create a **Redirect** response message.
    - Use the response&#39;s **Headers** collection to add the **Location** HTTP header to the response
    - Set the **Location** header to the string format **FederationCallback/end?acsToken={0}**
    - Replace the **{0}** placeholder with the token passed in the request. Retrieve the token by using the **ClaimsPrincipalExtensions.BootstrapToken** extension method.
    - The **BootstrapToken** method accepts an **IPrincipal** parameter. Use the **HttpContext.Current.User** to get the current **IPrincipal**.

   >**Note:** The client application extracts the token from the response and uses it to authenticate against the service in future requests.  
The special redirect to **FederationCallback/end** indicates to the client that the authentication process has completed successfully. This flow is part of the passive federation process.

4. In the **BlueYonder.Companion.Host** project, open the **Web.config** file, and then in the **&lt;appSettings&gt;** section set the **SwtSigningKey** application setting to the relying party token signing key you generated in the first exercise.

    - You can either locate the signing key in the Relying party configuration in the ACS portal, or copy it from the role&#39;s settings, from the **ACS.SigningKey** setting.

5. In the **Web.config** file, locate the **&lt;microsoft.identityModel&gt;** section and in the **&lt;audienceUris&gt;** element, replace the _[yourrealm]_ placeholder with **urn:blueyonder.cloud**.

   >**Note:** WIF 4.5 uses the **&lt;system.identityModel&gt;** section. However, the WIF.SWT NuGet package you installed still uses WIF 4, which uses the **&lt;microsoft.identityModel&gt;** section.

6. Locate the **&lt;trustedIssuers&gt;** element, and then replace the _[youracsnamespace]_ placeholder with your ACS namespace. Type the namespace in lowercase letters.
7. Add the following federated authentication configuration to the **&lt;service&gt;** element under the **&lt;microsoft.identityModel&gt;** section.

  ```cs
       <federatedAuthentication>
         <wsFederation passiveRedirectEnabled="false" issuer="urn:unused" realm="urn:unused" requireHttps="false" />
       </federatedAuthentication>
```
8. Make sure that the **Microsoft.IdentityModel** assembly in the **BlueYonder.Companion.Host** project is copied locally when deployed.

    - In the **Solution Explorer** pane, under the **BlueYonder.Companion.Host** project, expand the **References** node.
    - Click the **Microsoft.IdentityModel** reference, and then in the **Properties** window, change **Copy Local** to **True**.

    >**Note:** WIF 4 is not installed by default in Azure VMs. Therefore, you need to make sure that the assembly is included in the deployed package.

#### Task 4: Update the Routing with the New Authentication Configuration

1. In the **BlueYonder.Companion.Host** project, open the **WebApiConfig.cs** file from the **App_Start** folder, and then add a route for the **FederationCallback** controller in the **Register** method. Use the following parameters when calling the **MapHttpRoute** method.

| Parameter | Value |
| --- | --- |
| name | callback |
| routeTemplate | FederationCallback |
| defaults | a new anonymous type, with a **Controller** property set to the string **FederationCallback** |

  - Make sure you add the route before any other call to the **MapHttpRoute** method.

   >**Note:** The order of routes is important; you must add the federation callback route before adding the default route (**{controller}/{id}**), which handles all the other calls to the controllers. If you add the default route first, it will be used even when you use a URL that ends with **FederationCallback**.

2. In the **Register** method, use the **AuthenticationConfig.CreateConfiguration** static method you created before to get an **AuthenticationConfiguration** object.

    - Call the method before mapping the HTTP routes.
    - Store the result in a local variable.

3. Add the **AuthenticationHandler** message handler to the **TravelerReservationsApi**, **ReservationsApi**, and **DefaultApi** routes.

    - In each of the routes, add the **handler** parameter.
    - Set the parameter to a new **AuthenticationHandler** object, and then in the constructor, pass the variable you created in the previous step, in the ASP.NET Web API global configuration object.
    - The **config** parameter of the **Register** method contains the global configuration object.

   >**Note:** The authentication handler is not used for the first two routes you just added, because the requests to the **FederationCallback** controller are sent before the client is authenticated. The authentication handler is not used for the location&#39;s weather route because the **GetWeather** action is public and does not require any authentication.

#### Task 5: Decorate the ASP.NET Web API controllers for authorization

- In the **BlueYonder.Companion.Controllers** project, open the **ReservationsController**, **TravelersController** and the **TripsController** classes, and then decorate them with the [**Authorize**] attribute.

>**Results**: After completing this exercise, you should have successfully configured your ASP.NET Web API services to use claims-based identities, authenticate users, and authorize users. Also you should have tested this configuration.

### Exercise 3: Deploying the Web Application to Azure and Configuring the Client App

#### Scenario

Now that the ASP.NET Web API services are configured for claims-based identities, the last step is to configure the client-side code. ASP.NET Web API services requires the use of active federation, which requires the client to first authenticate against an identity provider, before sending the authentication token to the ASP.NET Web API service.

In this exercise, you will deploy your ASP.NET Web API services to Azure, and then configure the client to the location of your ACS and cloud service. The client-side code for active federation is already written in the client app. In this exercise, you will also examine the client-side code to understand the process of active federation.

The main tasks for this exercise are as follows:

1. Deploy the Web Application to Windows Azure.

2. Configure the client app for authentication.

3. Examine the Client Code That Manages the Authentication Process.

#### Task 1: Deploy the Web Application to Windows Azure

1. Publish the **BlueYonder.Companion.Host.Azure** project.

    - If you did not import your Azure subscription information yet, download your Azure credentials, and then import the downloaded publish settings file in the **Publish Windows Azure Application** dialog box

2. Select the cloud service that matches the cloud service name you wrote down in the beginning of the lab, after running the setup script.
3. Finish the deployment process by clicking **Publish**.

#### Task 2: Configure the client app for authentication

1. In the **20487B-SEA-DEV-C** virtual machine, open the **D:\AllFiles\Mod11\LabFiles\begin\BlueYonder.Companion.Client\BlueYonder.Companion.Client.sln** solution file in **Visual Studio 2012**.
2. In the **BlueYonder.Companion.Shared** project, open the **Addresses** class, and then in the **BaseUri** property replace the _{CloudService}_ placeholder with the Azure Cloud Service name you wrote down at the beginning of this lab.
3. In the **BlueYonder.Companion.Client** project, expand the **Helpers** folder, and then open the **DataManager** class. Set the ACS namespace constant to the namespace you created in Exercise 1, &quot;Configuring Windows Azure ACS&quot;.

   >**Note:** The client is already configured to use the _blueyonder.cloud_ realm.

#### Task 3: Examine the Client Code That Manages the Authentication Process

1. In the **DataManager** class, locate the **GetLiveIdUri** method, and examine its code. Observe how the method retrieves the address of the identity provider logon page.
2. Locate the **AuthenticateAsync** method, and examine its code. Observe how the method uses the **WebAuthenticationBroker** class to handle the authentication process.
3. Locate the **GetSessionToken** method and examine its code. Observe how the method uses the SWT it received from the federation callback to start a secure session with the ASP.NET Web API service.
4. Locate the **CreateHttpClient** method, and examine its code. Observe how the method creates a new HTTP request object with the HTTP **Authorization** header.
5. Run the client app and verify the client app requests a Microsoft Account (formerly Windows Live ID) identity. Provide your Microsoft Account credentials and verify you see the main windows of the app.
6. Display the app bar, sign out from the client app, and then close the app.

>**Results**: After completing this exercise, you should be able to successfully run the client app, and sign in by using your Microsoft Account credentials.

Perform the following steps to apply the **StartingImage** snapshot:

1. On the host computer, click **Start**, point to **Administrative Tools**, and then click **Hyper-V Manager**.
2. In Hyper-V Manager, click **20487B-SEA-DEV-A**, and in the **Snapshots** pane, right-click **StartingImage**, and then click **Apply**.
3. In the **Apply Snapshot** dialog box, click **Apply**.
4. Repeat Step 2 for all the virtual machines that you used in this lab. (excluding **MSL-TMG1**).

©2016 Microsoft Corporation. All rights reserved.

The text in this document is available under the  [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are  **not**  included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
