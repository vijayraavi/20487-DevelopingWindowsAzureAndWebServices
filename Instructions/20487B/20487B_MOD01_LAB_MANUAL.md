# Module 1: Overview of Service and Cloud Technologies

# Lab: Exploring the Work Environment

#### Scenario

In this lab you will explore several frameworks and platforms, such as Entity Framework, ASP.NET Web API, and Azure, that are used for creating distributed applications.

#### Objectives

After completing this lab, you will be able to:

- Create an entity data model by using the data model wizard.
- Create an ASP.NET Web API service.
- Create an Azure SQL database.
- Deploy a web application to an Azure website.

#### Lab Setup

Estimated Time: **30 minutes**

Virtual Machine: **20487B-SEA-DEV-A**

User name: **Administrator**

Password: **Pa$$w0rd**

For this lab, you will use the available virtual machine environment. Before you begin this lab, you must complete the following steps:

1. On the host computer, click **Start** , point to **Administrative Tools** , and then click **Hyper-V Manager**.

2. In Hyper-V Manager, click **MSL-TMG1** , and then in the **Actions** pane, click **Start**.
3. In Hyper-V Manager, click **20487B-SEA-DEV-A** , and then in the **Actions** pane, click **Start**.
4. In the **Actions** pane, click **Connect**. Wait until the virtual machine starts.
5. Sign in by using the following credentials:

   - User name: **Administrator**
   - Password: **Pa$$w0rd**

6. Verify that you received credentials to sign in to the Azure portal from you training provider; these credentials and the Azure account will be used throughout the labs of this course.

### Exercise 1: Creating an Azure SQL Database

#### Scenario

In this exercise, you will use Azure portal and create a new Azure SQL Database server. You will connect to the new SQL Database server from SQL Server Management Studio and create a BlueYonder database from the .bacpac file.

The main tasks for this exercise are as follows:

1. Create a new Azure SQL Database server

2. Manage the Azure SQL Database server from SQL Server Management Studio

#### Task 1: Create a new Azure SQL Database server

1. Open Internet Explorer and go to the Azure portal at **https://manage.windowsazure.com**.
2. On the **SQL DATABASES** page, from the **SERVERS** tab, add an Azure SQL Database server. Use the sign-in name **SQLAdmin** and password **Pa$$w0rd**, select a region that is closest to you, and then click **Complete**. Wait till the server appears in the list of servers and its status changes to **Ready**. Write down the name of the newly created server.

#### Task 2: Manage the Azure SQL Database server from SQL Server Management Studio

1. On the **Configure** tab of the newly created SQL database server, add a rule to allow access from any IP address (0.0.0.0 - 255.255.255.255), and save the new rule.

   >**Note:** We recommend that you allow only your computer&#39;s IP address, or your organization&#39;s IP address range to access the database server. However, in this course, you will use this database server for the upcoming labs, and your IP address might change in the meanwhile; therefore, you are required to allow access from all IP addresses.

2. Open Microsoft SQL Server Management Studio 2012 and connect to the new server. Use the server name  **SQLServerName.database.windows.net**, and the sign-in name and the password that you used in the previous task (Replace **SQLServerName** with the server name that you wrote down in the previous task).
3. In Object Explorer, right-click the **Databases** node, and then click **Import Data Tier Application**.
4. Import the **BlueYonder.bacpac** file located in the **D:\AllFiles\Mod01\LabFiles\Assets** folder.
5. Verify that the **BlueYonder** database is created.

   
   >**Results** : After completing this exercise, you should have created an Azure SQL database in your Azure account.

### Exercise 2: Creating an Entity Data Model

#### Scenario

In this exercise, you will create a new Class Library project and use Entity Framework in it, to connect to the Azure SQL database. You will use the Entity Framework database-first development approach to generate classes for the **BlueYonder** database tables.

The main tasks for this exercise are as follows:

1. Create an Entity Framework Model

#### Task 1: Create an Entity Framework Model

1. Open Visual Studio 2012 and create a new **Class Library** project. Name the project **BlueYonder.Model**.

2. Add an ADO.NET Entity Data Model to the project.

    a. Connect to the **SQLServerName.database.windows.net** server with the sign-in name and the password that you used in the previous task (replace **SQLServerName** with the server name you have written down in the previous exercise), and then select the **BlueYonder** database.

    b. Make sure to select the option to include the database password in the connection string.
    c. Import the **Locations** and **Travelers** tables.
    d. Save the EDMX file after it opens, and then close it.

   
   >**Results** : After completing this exercise, you should have created Entity Framework wrappers for the **BlueYonder** database.

### Exercise 3: Managing the Entity Framework Model with an ASP.NET Web API Project

#### Scenario

Create a new ASP.NET MVC 4 web application that exposes the Web API for CRUD operations on the BlueYonder database, by using the Entity Framework model.

The main tasks for this exercise are as follows:

1. Create an ASP.NET Web API Project

2. Add a Web API Controller with CRUD Actions by using the Add Controller Wizard

#### Task 1: Create an ASP.NET Web API Project

- Add a new **ASP.NET MVC 4 Web Application** project named **BlueYonder.MVC** using the Web API template.

#### Task 2: Add a Web API Controller with CRUD Actions by using the Add Controller Wizard

1. In the **BlueYonder.MVC** project, add a reference to the **BlueYonder.Model** project.

2. Copy the connection string from the **BlueYonder.Model** project to the **BlueYonder.MVC** project (from the **App.config** file to the **Web.config** file).
3. Build the solution, open Server Explorer, and then refresh the **Data Connections**.
4. In Solution Explorer, in the **BlueYonder.MVC** project, right-click the **Controllers** folder, and then add a new controller named **LocationsController** , by using the following steps:

5. Create the new controller by using the **API controller with read/write actions, using Entity Framework** template.

6. Select the **Location** model class.
7. Select the **BlueYonderEntities** data context class.

   >**Note:** You now have a Web API controller for the Location model.

8. Run the **BlueYonder.MVC** web application and in the web browser, append the **api/locations** string to the URL to download the list of locations. Open the downloaded file and verify that you see a JSON formatted list of locations.

   
   >**Results** : After completing this exercise, you should have created a web app that exposes the Web API for CRUD operations on the BlueYonder database.

### Exercise 4: Deploying a Web App to Azure

#### Scenario

In this exercise, you will create an Azure web app to host the ASP.NET MVC 4 web application.

The main tasks for this exercise are as follows:

1. Create a New Azure Web App

2. Deploy the web application to the Azure web app

3. Delete the Azure SQL Database Server

#### Task 1: Create a New Azure Web App

1. Open Azure portal at **https://manage.windowsazure.com**.

2. On the **WEB APPS** page, click **NEW** , and then click **QUICK CREATE** to create an Azure web app. Name the web app **BlueYonderWebSite_YourInitials_** (Replace **_YourInitials_** with your initials), create a new App Service plan, and then select the region that is closest to your location.
3. After you create the web app, wait until its status changes to **Running**.
4. On the web app&#39;s **DASHBOARD** page, click the **Download the publish profile** link to download the web app&#39;s publish profile file.

#### Task 2: Deploy the web application to the Azure web app

1. In Visual Studio 2012, right-click the **BlueYonder.MVC** project in Solution Explorer, and then click **Publish**.

2. Import the profile settings file that you downloaded in the previous task, and use the **Publish Web** wizard to deploy the web application to the Azure web app that you created. Wait for the deployment to complete, and for a browser window to open.

   >**Note:** At this point, you can click **Next** at every step of the wizard, and then click **Publish** to start the publishing process. Later in the course, you will learn how the deployment process works and how to configure it.

3. In the **Internet Explorer** window, append the **api/locations** string to the URL and download the list of locations.
4. Open the downloaded file and verify that you see the list of locations in JSON format.

#### Task 3: Delete the Azure SQL Database Server

1. Open Internet Explorer and go to the Azure portal at **https://manage.windowsazure.com**.

2. Open the **SQL DATABASES** page, and on the **SERVERS** tab, click the **STATUS** column of the server you created in the first exercise, and then click **DELETE**. Follow the instructions in the **Delete Server Confirmation** dialog box to delete both the database and the server.

   >**Note:** Azure free subscriptions have a resource limitation and a restriction on the total working hours. To avoid exceeding those limitations, you must delete the Azure SQL Database resources.

   
  
  >**Results** : After completing this exercise, you should have ensured that all your products are hosted on the Microsoft Azure cloud by using SQL Databases and Azure Web Apps.

Perform the following steps to apply the **StartingImage** snapshot:

1. On the host computer, click **Start**, point to **Administrative Tools**, and then click **Hyper-V Manager**.
2. In Hyper-V Manager, click **20487B-SEA-DEV-A**, and in the **Snapshots** pane, right-click **StartingImage**, and then click **Apply**.
3. In the **Apply Snapshot** dialog box, click **Apply**.
4. Repeat Step 2 for all the virtual machines that you used in this lab. (excluding **MSL-TMG1**).

©2016 Microsoft Corporation. All rights reserved.

The text in this document is available under the  [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are  **not**  included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
