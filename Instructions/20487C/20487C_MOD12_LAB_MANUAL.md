# Module 12: Scaling Services

# Lab: Scalability

#### Scenario

The final task that you need to perform in the Blue Yonder Airlines application is to reduce the ASP.NET Web API back-end service database load by storing the latest reservations data that was fetched from the database in a distributed cache. In this lab, you will add a distributed caching mechanism to the ASP.NET Web API service.

#### Objectives

After completing this lab, you will be able to:

- Use Azure Redis Cache with Web applications.

#### Lab Setup

Estimated Time: **20 Minutes**.

1. Verify that you received credentials to sign in to the Azure portal from you training provider, these credentials and the Azure account will be used throughout the labs of this course.

### Exercise 1: Use Azure Redis Cache

#### Scenario

To reduce the number of requests sent to the database from the ASP.NET Web API back-end service, you will add a distributed cache. When the system administrator requests a list of the latest reservations, the result of querying the database is stored in the distributed cache. Subsequent requests retrieve the data from the cache, unless a new reservation was added (which invalidates the cache entry).

In this exercise, you will create an Azure Redis Cache, and configure the Reservations controller to check for an existing cached result before returning the list of latest reservations.

The main tasks for this exercise are as follows:

1. Create an Azure Redis Cache.

1. Add code to cache the list of latest reservations.

1. Add code to fetch the list of latest reservations.

1. Add code to invalidate the list of latest reservations.

1. Test using Windows PowerShell.

#### Task 1: Create an Azure Redis Cache

1. Open the Azure Portal, and create a new Azure Redis Cache named `blueyondercache-YOURINITIALS`. Use the **Basic C0** pricing tier.

1. In the newly created Azure Redis Cache, navigate to **Access Keys** and copy the primary connection string to the clipboard.

1. Open the **BlueYonder.Companion.sln** solution file from the **[repositoryroot]\Allfiles\20487C\Mod12\LabFiles\begin\BlueYonder.Companion** folder.

1. In the **BlueYonder.Reservations** project, open the **Web.config** file and replace the connection string named **Redis** with the value copied in the previous step.

#### Task 2: Add code to cache the list of latest reservations

1. In the **BlueYonder.Reservations** project, open the **ReservationsController** class and locate the **Get** method. In the method, locate the comment _// TODO: Lab 12, Exercise 1, Task 1.2 : Store the query results in Redis_, and after it add code to serialize the reservations to a string and store the result in Redis using the `RedisProvider.Cache` object under the `RESERVATIONS_KEY` key.

#### Task 3: Add code to fetch the list of latest reservations

1. Still in the **Get** method, locate the comment _// TODO: Lab 12, Exercise 1, Task 1.3 : Fetch the query results from Redis_, and after it add code to fetch the serialized reservations from Redis and deserialize them to `List<ReservationDTO>` if the string was found in the cache.

#### Task 4: Add code to invalidate the list of latest reservations

1. Locate the **Post** method. In the method, locate the comment _// TODO: Lab 12, Exercise 1, Task 1.4 : Delete the cached results from Redis_, and after it add code to delete the `RESERVATIONS_KEY` key from Redis.

#### Task 5: Test using Windows PowerShell

1. Build and run the **BlueYonder.Reservations** project.

1. In a web browser, navigate to _http://localhost:1791/api/reservations_ (replace the port number with the appropriate value in your case). A list of reservations is displayed in JSON format.

1. Open Windows PowerShell, and use the **Invoke-WebRequest** cmdlet to make a POST request to _http://localhost:1791/api/reservations?traveler=David_. This should return HTTP Created (status code 201).

1. In the web browser, refresh the /api/reservations page again, and make sure you see the new reservation.

  >**Results**: After completing this exercise, you should have successfully created an Azure Redis Cache, and used it to cache reservation information obtained from the database.

©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the  [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are  **not**  included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
