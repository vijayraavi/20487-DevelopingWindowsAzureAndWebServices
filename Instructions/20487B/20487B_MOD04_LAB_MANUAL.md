# Module 4 - Extending and Securing ASP.NET Web API Services

## Lab: Extending Travel Companion&#39;s ASP.NET Web API Services

#### Scenario

Blue Yonder Airlines wishes to add more features to their client application: searchable flight schedule, getting an RSS feed for flight schedules, and changing the way the client connects to the service, by using  a secured HTTPS channel. In this lab, you will implement the necessary changes to the ASP.NET Web API services you previously created. In addition, you will use these changes to apply validations and dependency injection to the services and make them more secured, manageable, and testable.

#### Objectives

After completing this lab, you will be able to:

- Secure the service&#39;s communication with HTTPS.
- Use System.ComponentModel.DataAnnotations and an ActionFilter to validate a model.
- Create a MediaTypeFormatter to support returning image content.
- Create an OData queryable service action.
- Use the dependency resolver to inject repository types.

#### Lab Setup

Estimated Time: **75 minutes**

Virtual Machine: **20487B-SEA-DEV-A** and **20487B-SEA-DEV-C**

User name: **Administrator** , **Admin**

Password: **Pa$$w0rd** , **Pa$$w0rd**

For this lab, you will use the available virtual machine environment. Before you begin this lab, you must complete the following steps:

1. On the host computer, click **Start** , point to **Administrative Tools** , and then click **Hyper-V Manager**.
2. In Hyper-V Manager, click **MSL-TMG1** , and in the **Actions** pane, click **Start**.
3. In Hyper-V Manager, click **20487B-SEA-DEV-A** , and in the **Actions** pane, click **Start**.
4. In the **Actions** pane, click **Connect**. Wait until the virtual machine starts.
5. Sign in by using the following credentials:

  - User name: **Administrator**
  - Password: **Pa$$w0rd**

6. Return to Hyper-V Manager, click **20487B-SEA-DEV-C** , and in the **Actions** pane, click **Start**.
7. In the **Actions** pane, click **Connect**. Wait until the virtual machine starts.
8. Sign in by using the following credentials:

  - User name: **Admin**
  - Password: **Pa$$w0rd**

### Exercise 1: Creating a Dependency Resolver for Repositories

#### Scenario

Controllers use repositories as data providers. In the current implementation, the controller is responsible to create the repository in the constructor. This is not a good practice because the controller knows the type of the repository that results in a coupling.

In the following exercise, you will decouple the controller and repository by using the dependency injection technique to inject the repository interface as a parameter in the controller constructor.

You will start by creating a dependency resolver class that is responsible for creating the repositories. You will then register the dependency resolver class in the **HttpConfiguration** to automatically create a repository when a controller is used. Finally, you will use Microsoft Fakes and create a stub for a repository, and then use it in a unit test project to test the location controller.

The main tasks for this exercise are as follows:

1. Change the FlightController constructor to support injection

2. Create a dependency resolver class

3. Register the dependency resolver class with HttpConfiguration

#### Task 1: Change the FlightController constructor to support injection

1. Open the BlueYonder.Companion.sln solution from the D:\AllFiles\Mod04\LabFiles\begin\BlueYonder.Companion folder.
2. In the **BlueYonder.Companion.Controllers** project, change the **LocationsController** class constructor to receive the locations repository:

   a. Change the **LocationsController** constructor method, so that it receives an **ILocationRepository** object.  
   b. Initialize the **Locations** member with the constructor parameter.

  >**Note:** The same pattern was already applied in the starter solution for the rest of the controller classes ( **TravelersController** , **FlightsController** , **ReservationsController** , and **TripsController** ).  
   Open those classes to review the constructor definition.

#### Task 2: Create a dependency resolver class

1. Open the **BlueYonderResolver** class from the **BlueYonder.Companion.Controllers** project, and in the **GetService** method, add the missing code for resolving the **LocationsController** class.

  - If the **serviceType** parameter is of the type **LocationsController** , create an instance of the **LocationsController** class with the required repository.

  >**Note:** The BlueYonderResolver class implements the IDependencyResolver interface.

#### Task 3: Register the dependency resolver class with HttpConfiguration

1. Open the **WebApiConfig.cs** file, located under the **App\_Start** folder in the **BlueYonder.Companion.Host** project, and set **BlueYonderResolver** as the new dependency resolver.

   - Use the **DependencyResolver** property of the **config** object to set the dependency resolver.

2. Test the application and the **DependencyResolver** injection:

   a. Open the **LocationsController** class from the **BlueYonder.Companion.Controllers** project, and place a breakpoint in the constructor.  
   b. Run the **BlueYonder.Companion.Host** project in debug mode.  
   c. Navigate to the address http://localhost:9239/Locations.  
   d. Return to Visual Studio 2012 and verify that the code breaks on the breakpoint and that the constructor parameter is initialized (not null).

3. Open the **LocationControllerTest** test class from the **BlueYonder.Companion.Controllers.Tests** project, and examine the code in the **Initialize** method. The test initialization process uses the **StubILocationRepository** type,which was auto-generated with the **Fakes** framework. This stub repository mimics the real location repository. You use the fake repository to test the code, instead of using the real repository, which requires using a database for the test. When running unit tests, you should use fake objects to replace external components, in order to reduce the complexity of creating and executing the test.

  >**Note:** For additional information about Fakes, see: [http://go.microsoft.com/fwlink/?LinkID=298770&amp;clcid=0x409](http://go.microsoft.com/fwlink/?LinkID=298770&amp;clcid=0x409)

4. Test the application by using the **Fakes** mock framework and running the test project.

   - On the **Test** menu, point to **Run** , and the click **All Tests**.

>**Results** : You will be able to inject data repositories to the controllers instead of creating them explicitly inside the controllers. This will decouple the controllers from the implementation of the repositories.

### Exercise 2: Adding OData Capabilities to the Flight Schedule Service

#### Scenario

OData is a data access protocol that provides standard CRUD access of a data source via a website.

To add support for the OData protocol, you will install the NuGet package **Microsoft.AspNet.WebApi.OData** , and then decorate the methods that should support OData with the **[Queryable]** attribute.

The main tasks for this exercise are as follows:

1. Add a Queryable action to the flight schedule service

2. Handle the search event in the client application and query the flight schedule service by using OData filters

#### Task 1: Add a Queryable action to the flight schedule service

1. Use the Package Manager Console window to install the **4.0.30506** version of the **Microsoft.AspNet.WebApi.OData** NuGet package in the **BlueYonder.Companion.Controllers**

  >**Note:** The current version of the **Microsoft.AspNet.WebApi.OData** NuGet package has some incompatibilities with the version that was used while developing this course.  
Therefore, you are required to use the Package Manager Console to install the supported version of this package. ** **

2. In the **BlueYonder.Companion.Controllers** project, open the **LocationsController** class, and decorate the **Get** method verload, which has three parameters, with the **[Queryable]** attribute.
3. Change the method implementation to use the repository&#39;s **GetAll** method and return **IQueryable** instead of **IEnumerable**.
4. Remove the parameters from the method declaration. You will not need them anymore because the OData infrastructure will take care of the query filtering.

#### Task 2: Handle the search event in the client application and query the flight schedule service by using OData filters

1. Log on to the virtual machine **20487B-SEA-DEV-C** as **Admin** with the password **Pa$$w0rd**.
2. Open the BlueYonder.Companion.Client solution from the D:\AllFiles\Mod04\LabFiles\begin\BlueYonder.Companion.Client folder.
3. Open the **Addresses.cs** file from the **BlueYonder.Companion.Shared** project and change the **GetLocationsWithQueryUri** property to use OData querying instead of the standard query string.

   - Replace the current query string with the **$filter** option by using the expression **substringof(tolower(&#39;{0}&#39;),tolower(City))**.  
  
    The resulting code should resemble the following code.
  

       GetLocationsUri + &quot;?$filter=substringof(tolower(&#39;{0}&#39;),tolower(City))&quot;;

>**Results** : Your web application exposes the OData protocol that supports the **Get** request of the locations data.

### Exercise 3: Applying Validation Rules in the Booking Service

#### Scenario

To make your web application more robust, you need to add some validations to the incoming data.

You will apply a validation rule to your server to verify that all required fields of a model are sent from the client before handling the request.

You will decorate the **Travel** model with attributes that define the required fields, then you will derive from the **ActionFilter** class and implement the validation of the model. Finally, you will add the validation to the **Post** action.

The main tasks for this exercise are as follows:

1. Add Data Annotations to the Traveler Class

2. Implement the Action filter to validate the model

3. Apply the custom attribute to the PUT and POST actions in the booking service

#### Task 1: Add Data Annotations to the Traveler Class

1. Return to the **BlueYonder.Companion** solution in the **20487B-SEA-DEV-A** virtual machine. In the **BlueYonder.Entities** project, use data annotation attributes to validate the **Traveler** class&#39; properties by using the following rules:


   - **FirstName** , **LastName** , and **HomeAddress** should use the **[Required]** validation attribute.
   - **MobilePhone** should use the **[Phone]** validation attribute.
   - **Email** should use the **[EmailAddress]** validation attribute.

#### Task 2: Implement the Action filter to validate the model

1. In the **BlueYonder.Companion.Controllers** project, create a public class named **ModelValidationAttribute** that derives from **ActionFilterAttribute**. Add the new class to the project&#39;s **ActionFilters** folder.
2. In the new class, override the **OnActionExecuting** method and implement it as follows:

   a. Check if the model state is valid by using the **actionContext.ModelState.IsValid** property.  
   b. If the model state is not valid, call **actionContext.Request.CreateErrorResponse** to create an error response message.

    >**Note:** The **CreateErrorResponse** method is an extension method. To use it, add a **using** directive for the **System.Net.Http** namespace.

   c. For the error response, use the overload that expects an **HttpStatusCode** enum and an **HttpError** object.    
   d. Use **HttpStatusCode.BadRequest** , and initialize the **HttpError** object with **actionContext.ModelState** and the **true** Boolean value for the second constructor parameter.

#### Task 3: Apply the custom attribute to the PUT and POST actions in the booking service

1. In the **BlueYonder.Companion.Controllers** project, open the **TravelersController** class, and decorate the **Put** and **Post** methods with the **[ModelValidation]** attribute.
2. Build the solution.

>**Results** : Your web application will verify that the minimum necessary information is sent by the client before trying to handle it.

### Exercise 4: Securing the Communication between the Client and the Server

#### Scenario

To support a secured communication between the service and the client, you will need to define a certificate.

You will need to add an HTTPS binding to the service and specify the certificate to be used by the binding. Finally, you will need to configure the client to communicate with the service over the secured connection.

The main tasks for this exercise are as follows:

1. Add an HTTPS binding in IIS

2. Host an ASP. NET web API web application in IIS

3. Test the client application against the secure connection

#### Task 1: Add an HTTPS binding in IIS

1. Run the Setup.cmd file from D:\AllFiles\Mod04\LabFiles\Setup.

  >**Note:** The setup script creates a server certificate to be used for HTTPS communication.

2. Open IIS Manager. Open the Server Certificates feature from the SEA-DEV12-A (SEA-DEV12-A\Administrator) features list.
3. Verify that you can see a certificate issued to **SEA-DEV12-A**. This certificate was created by the script you ran in the previous task.
4. Select **Default Web Site** from the **Connections** pane, open the **Bindings** list, and add an HTTPS binding. Select the **SEA-DEV12-A** certificate that was created by the setup script.

  >**Note:** When you add an HTTPS binding to the web site bindings, all web applications in the web site will support HTTPS.

#### Task 2: Host an ASP. NET web API web application in IIS

1. Publish the service to IIS from Visual Studio 2012 by changing the **BlueYonder.Companion.Host** project properties.

    a. In the project&#39;s Properties window, click the **Web** tab, and change the server from IIS Express to the local IIS Web server.  
    b. Create the virtual directory and save the changes to the project.
  
2. Browse the web application by using HTTP to get the location data.

    a. Return to IIS Manager, refresh the **Default Web Site** , and then select the **BlueYonder.Companion.Host** application.  
    b. Click the **Browse \*:80** link to browse the web application.  
    c. Append the string **locations** to the text in the address bar to get the list of locations.  
    d. Explore the contents of the file. It contains the **Location** database content in the JSON format.

3. Change the scheme of the URL from HTTP to HTTPS to get the location data again, this time through a secured channel.

    a. Make sure you also change the computer name in the URL from **localhost** to **SEA-DEV12-A**.  
    b. Explore the contents of the file. It should contain the same locations as before.

  >**Note:** If you use **localhost** instead of the computer&#39;s name, the browser will display a certificate warning. This is because the certificate was issued to the **SEA-DEV12-A** domain, not the **localhost** domain.

#### Task 3: Test the client application against the secure connection

1. Return to the **BlueYonder.Companion.Client** solution in the **20487B-SEA-DEV-C** virtual machine.


   - Open the **Addresses.cs** file from the **BlueYonder.Companion.Shared** project and in the **BaseUri** property, change the URL from using HTTP to HTTPS.

2. Run the client app, search for **New** , and purchase a flight from _Seattle_ to _New York._ Provide an incorrect value in the email field and verify if you get a validation error message originating from the service.


   - Use any non-valid email address, such as **ABC** or **ABC@DEF**.

3. Correct the email address and purchase the trip. Verify that the trip was purchased successfully, and then close the client app.

>**Results** : The communication with your web application will be secured by using a certificate.
