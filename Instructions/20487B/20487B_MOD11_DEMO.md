# Module 11: Identity Management and Access Control

# Lesson 1: Claims-based Identity Concepts

### Demonstration: Using claims in an ASP.NET Website

#### Preparation Steps

For this demo, you will use the available virtual machine environment. Before you begin this demo, you must complete the following steps:
 1.	On the host computer, click **Start**, point to **Administrative Tools**, and then click **Hyper-V Manager**.
 2.	In Hyper-V Manager, click **MSL-TMG1**, and in the Action pane, click **Start**.
 3.	In Hyper-V Manager, click **20487B-SEA-DEV-A**, and in the Action pane, click **Start**.
 4.	In the **Action** pane, click **Connect**. Wait until the virtual machine starts.
 5.	Sign in using the following credentials:
    - User name: **Administrator**
    - Password: **Pa$$w0rd**
 
 6.	On the Start screen, click the **Visual Studio 2012** tile. 
 7.	In Visual Studio 2012, on the **Tools** menu, click **Extensions and Updates**. 
 8.	In the **Extensions and Updates** dialog box, on the navigation pane, expand the **Online** node, and then click the **Visual Studio Gallery** node. 
 9.	Press Ctrl+E, and then type **Identity**. In the center pane, click the **Identity and Access Tool** extension, and then click **Download**. 
 10.	When the **Download and Install** dialog box appears, click **Install. Wait for installation completion, click Close, and then close Visual Studio 2012.

#### Demonstration Steps

1. On the Start screen, click the **Visual Studio 2012** tile.
2. On the **File** menu, point to **New**, and then click **Project**. A **New Project** dialog box will open.
3. In the **New Project** dialog box, in the navigation pane, expand the **Installed** node, expand the **Templates** node, expand the **Visual C#** node, and then click the **Web** node.
4. In the **Name** text box, type **ClaimsApp**.
5. In the **Location** text box, type **D:\Allfiles\Mod11\DemoFiles\ClaimsApp**.
6. In the project templates list, click **ASP.NET MVC 4 Web Application**, and then click **OK**.
7. In the **New ASP.NET MVC 4 Project** dialog box, select the **Internet Application** template, and then click **OK**.
8. To start the application without debugging, press Ctrl+F5. A browser will open.
9. Examine the **Log in** link on the upper-right corner of the web page - the default functionality uses ASP.NET Membership as an identity store.
10. Close the browser and return to Visual Studio 2012.
11. In the **Solution Explorer** pane, right-click the project, and then click **Identity and Access**. The **Identity and Access** dialog box will open.
12. In the **Identity and Access** dialog box, on the **Providers** tab, select **Use the Local Development STS to test your application**.
13. Click the **Local Development STS** tab, and then show the students the test claims that will be used during development.
14. Click **OK** to save the changes.
15. In the **Solution Explorer** pane, under the **ClaimsApp** project, expand the **Controllers** folder, and then double-click **HomeController.cs**.
16. Add the following using directives.

  ```cs
        using System.Security.Claims;
        using System.Threading;
```
17. Locate the **Index** method, and then add the following code to display the claims in the HTML output.

  ```cs
        var identity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;
        ViewBag.Claims = identity.Claims;
```
18. Press Ctrl+S to save the file.
19. In the **Solution Explorer** pane, under the **ClaimsApp** project, expand the **Views** folder, expand the **Home** folder, and then double-click **Index.cshtml**.
20. Add the following using directive.

  ```cs
        @using System.Security.Claims;
```
21. Add the following code before the **&lt;h3&gt;** HTML element, to output the claim types and values.

  ```cs
        @foreach (Claim claim in @ViewBag.Claims)
        {
        <div><b> @claim.Type </b> @claim.Value<br/></div>
        }
```
22. Press Ctrl+S to save the file.
23. To run the application without debugging, press Crtl+F5.
24. Notice that the local STS is running, the application authenticated the user as **Terry**, and that the application also outputs the list of claims.

    >**Note:** ASP.NET leverages a process named passive federation that can only be used inside a browser-based environment. Passive federation is discussed in depth in Lesson 3, &quot;Configuring Services to Use Federated Identities&quot;, of this module.

# Lesson 2: Using the Windows Azure Access Control Service

### Demonstration: Configuring ACS Using the Azure portal

#### Preparation Steps

For this demo, you will use the available virtual machine environment. Before starting this demo, you need to install the Identity and Access Tool extension in Visual Studio 2012:
 1.	On the host computer, click **Start**, point to **Administrative Tools**, and then click **Hyper-V Manager**.
 2.	In Hyper-V Manager, click **MSL-TMG1**, and in the Action pane, click **Start**.
 3.	In Hyper-V Manager, click **20487B-SEA-DEV-A**, and in the Action pane, click **Start**.
 4.	In the **Action** pane, click **Connect**. Wait until the virtual machine starts.
 5.	Sign in using the following credentials:
    - User name: **Administrator**
    - Password: **Pa$$w0rd**
 6.	In this demonstration you will need to use the **Identity and Access Tool** extension you installed in Visual Studio 2012 in Demo 1, "Using claims in an ASP.NET Website", of Lesson 1, "Claims-based Identity Concepts". If you have not installed this extension, please follow the preparation steps in that demo.

#### Demonstration Steps

1. On the Start screen, click the **Internet Explorer** tile.
2. Go to **http://manage.windowsazure.com**.
3. If a page appears, prompting you to provide your email address, type your email address, and then click **Continue**. Wait for the **Sign In** page to appear, type your email address and password, and then click **Sign In**.

    >**Note:** During the sign-in process, if a page appears prompting you to choose from a list of previously used accounts, select the account that you previously used, and then continue to type your credentials.

4. If the **Windows Azure Tour** dialog appears, click **Close** (the **X** button).
5. Click **NEW** , click **APP SERVICES**, click **ACTIVE DIRECTORY**, click **ACCESS CONTROL**, and then click **QUICK CREATE**.
6. Enter the following values:

    - Namespace: **BlueYonderServerDemo11_YourInitials_** (_YourInitials_ will contain your initials).
    - Region: Select the region closest to your location.

7. Click **CREATE**, and wait for a successful creation.
8. Click **ACTIVE DIRECTORY** in the navigation pane, click the **ACCESS CONTROL NAMESAPCES** tab, click the newly created namespace, and then click **MANAGE** at the bottom of the page. The Access Control Service portal will open.
9. In the Access Control Service portal, in the pane on the left, click the **Identity Providers** link under the **Trust relationships** section.
10. In the **Identity Providers** page, click **Add**.
11. In the **Add Identity Provider** page, select **Yahoo** under **Add a preconfigured identity provider**, click **Next**, and then click **Save**.
12. In the Access Control Service portal, in the pane on the left, click the **Relying party applications** link under the **Trust relationships** section.
13. In the **Relying Party Applications** page, click **Add**.
14. In the **Add Relying Party Application** page, type the following values:

    - Name: **BlueYonderServerDemo**
    - Realm: **http://localhost/WebApplication/**
    - Return URL: **http://localhost/WebApplication/**
    - Verify that the **Create new rule group** check box is selected.

15. At the bottom of the page, click **Save**.
16. In the Access Control Service portal, in the pane on the left, click the **Rule groups** link under the **Trust relationships** section.
17. In the **Rule Groups** page, under the **Rule Groups** section, click **Default Rule Group for BlueYonderServerDemo**.
18. In the **Edit Rule Group** page, click the **Generate** link above the **Rules** section.
19. Verify that all the available identity providers are selected, and then click **Generate**.
20. In the **Edit Rule Group** page, click **Save**.
21. In the Access Control Service portal, in the pane on the left, click the **Application integration** link under the **Development** section.
22. In the **Application Integration** page, under **Endpoint Reference** section, locate the **WS-Federation Metadata** address, and then copy the address to the clipboard.
23. On the Start screen, click the **Visual Studio 2012** tile.
24. On the **File** menu, point to **Open** , and then click **Project/Solution**.
25. Type **D:\AllFiles\Mod11\DemoFiles\ConfigureTheACS\begin\ConfigureTheACS.sln** in the **File name** text box, and then click **Open**.
26. In the **Solution Explorer** pane, right-click the **WebApplication** project, and then click **Identity and Access**.
27. In the **Identity and Access** dialog box, on the **Providers** tab, select the **Use a business identity provider (e.g. Windows Azure Active Directory ADFSv2)** option. A new section is added at the bottom of the dialog box.
28. In the **Enter the path to the STS metadata document** text box, paste the **WS-Federation Metadata** address you copied to the clipboard earlier.
29. Click **OK** to save the changes.
30. In the **Solution Explorer** pane, in the **WebApplication** project, double-click **Web.config**.
31. Examine the configuration that was written to the **Web.config** file, in the **&lt;system.identityModel&gt;**, and the **&lt;system.web&gt;** configuration sections.
32. To start the application without debugging, press Ctrl+F5.
33. You will be redirected to ACS and an identity provider selection page will be presented.
34. Choose an identity provider, sign in, and then you will be redirected to the application&#39;s default page.
35. Close the browser window.

# Lesson 3: Configuring Services to Use Federated Identities

### Demonstration: Configuring ACS for Service Bus Endpoints

#### Preparation Steps

For this demo, you will use the available virtual machine environment. Before you begin this demo, you must complete the following steps:
 1.	On the host computer, click **Start**, point to **Administrative Tools**, and then click **Hyper-V Manager**.
 2.	In Hyper-V Manager, click **MSL-TMG1**, and in the Action pane, click **Start**.
 3.	In Hyper-V Manager, click **20487B-SEA-DEV-A**, and in the Action pane, click **Start**.
 4.	In the **Action** pane, click **Connect**. Wait until the virtual machine starts.
 5.	Sign in using the following credentials:
    - User name: **Administrator**
    - Password: **Pa$$w0rd**

#### Demonstration Steps

1. Go to **D:\AllFiles\Mod11\DemoFiles\ACSForServiceBus\Setup**.
2. Double-click the **Setup.cmd** file. When prompted for information, provide it according to the instructions.

   >**Note:** You might see warnings in yellow indicating a mismatch in the versions and obsolete settings. These warnings might appear if there are newer versions of Azure PowerShell cmdlets. If these warnings are followed by a red error message, please inform the instructor, otherwise you can ignore them.

3. Write down the name of the service bus namespace that is shown in the script. You will use it later on during the demo.
4. On the Start screen, click the **Internet Explorer** tile.
5. G to **http://manage.windowsazure.com**.
6. If a page appears, prompting you to type your email address, type your email address, and then click **Continue**.
7. Wait for the **Sign In** page to appear, type your email address and password, and then click **Sign In**.

   >**Note:** During the sign-in process, if a page appears prompting you to choose from a list of previously used accounts, select the account that you previously used, and then continue to type your credentials.

8. If the **Windows Azure Tour** dialog appears, click **Close** (the **X** button).
9. Click **SERVICE BUS** in the navigation pane, and then click the namespace that showed in the script.
10. Click the **QUEUES** tab, and then click **CREATE A NEW QUEUE**.
11. Click **QUICK CREATE**, type **BlueYonderQueue** in the **QUEUE NAME** text box, and then click **CREATE A NEW QUEUE**. Wait for the queue to be created.
12. Go to the address **https://namespace-sb.accesscontrol.windows.net** (namespace is the service bus namespace that displayed when running the script) to open the Service Bus ACS portal.

   >**Note:** The browser should sign you in automatically to the portal. If you are redirected to sign in page, type your email address and password, and then click **Sign in**.

13. In the **Access Control Service** portal, in the pane on the left side, click the **Relying party applications** link under the **Trust relationships** section.
14. In the Relying Party Application page, click Add.
15. In the **Add Relying Party Application** page, type the following values:

    - Name: BlueYonderQueue
    - Realm: **http://_namespace_.servicebus.windows.net/blueyonderqueue** (_namespace_ is the service bus namespace that displayed when running the script)
    - Token lifetime: **1200**

16. Deselect **Windows Live ID** under the **Identity providers** section.
17. Verify that **Create new rule group** is selected, and then select **Default Rule Group for ServiceBus**, to retain access rights to the _owner_ identity.
18. Click **Save** at the bottom of the page.
19. In the Access Control Service portal, in the pane on the left side, click the **Service identities** link under the **Service settings** section.
20. On the **Service Identities** page, click **Add**.
21. On the **Add Service Identity** page, type **QueueClient** in the **Name** text box.
22. Verify that **Symmetric Key** is selected in the **Type** drop down list.
23. Click **Generate** next to the **Key** text box.
24. Select the content of the **Key** text box, and then press Ctrl+C to copy the password to the clipboard.
25. Click **Save** at the bottom of the page. The **Edit Service Identity** page opens.
26. In the **Edit Service Identity** page, above the **Credentials** section, click **Add**.
27. In the **Add Credential** page, select **Password** in the **Type** drop down list.
28. In the **Password** text box, press Ctrl+V to paste the generated key you copied earlier.
29. Click **Save** at the bottom of the page.
30. In the Access Control Service portal, in the pane on the left, click the **Rule groups** link under the **Trust relationships** section.
31. On the **Rule Groups** page, under the **Rule Groups** section, click the **Default Rule Group for BlueYonderQueue** link.
32. On the **Edit Rule Group** page, above **Rules** section, click the **Add** link. The **Add Claim Rule** page opens.
33. Under **Input claim issuer**, choose the **Access Control Service** option.
34. Under **Input claim type**, select **Select type**.
35. Under **Input claim value**, select **Enter value**, and then type **QueueClient** in the text box on the right.
36. Under **Output claim type**, select **Enter type**, and then type **net.windows.servicebus.action** in the text box on the right.
37. Under **Output claim value**, select **Enter value**, and then type **Send** in the text box on the right.
38. Click **Save** at the bottom of the page.
39. On the **Edit Rule Group** page, click **Save**.
40. Keep the browser open, and then on the Start screen, click the **Visual Studio 2012** tile.
41. On the **File** menu, point to **Open**, and then click **Project/Solution**.
42. Type **D:\AllFiles\Mod11\DemoFiles\ACSForServiceBus\begin\ServiceBusQueue\ServiceBusQueue.sln** in the **File name** text box, and then click **Open**.
43. In the **Solution Explorer** pane, expand the project, and then double-click **Program.cs**.
44. Examine the code in the **Main** method. The application will attempt to do the following:

    - Send messages to the queue by using the **QueueClient** identity.
    - Listen to the queue by using the **owner** identity.
    - Listen to the queue by using the **QueueClient** identity.

45. The application will fail on listening to the queue with the **QueueClient** identity, because you only configured it to allow sending messages.
46. In the **Program.cs** file, locate the static string named **ServiceBusNamespace**, and then replace the value **{ServiceBusNamespace}** with the service bus namespace that displayed when running the script.
47. Return to the browser window, and then in the pane on the left side, click the **Service identities** link under the **Service settings** section.
48. On the Service Identities page, click owner.
49. On the **Edit Service Identity** page, click **Password** at the bottom of the page.
50. On the **Edit Credential** page, click **Show Password**, select the text in the **Password** text box, and then press Ctrl+C to copy the password to the clipboard.
51. Return to Visual Studio 2012, locate the static string named **ServiceIdentityKey1**, select the value **{Password}**, and then press Ctrl+V to replace the value with the copied password.
52. Perform the same steps to copy the password of the **QueueClient** service identity, and then paste it over the **{Password}** value of the **ServiceIdentityKey2** static string.
53. Press Ctrl+S to save the file.
54. Press Ctrl+F5 to run the application without debugging.
55. Verify the application can send messages to the queue, and read messages from the queue.
56. Verify the attempt to read messages from the queue fails when accessed with the **QueueClient** identity.
57. Close the console window.

©2016 Microsoft Corporation. All rights reserved.

The text in this document is available under the  [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are  **not**  included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
