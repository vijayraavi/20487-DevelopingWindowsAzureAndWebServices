# Module 9: Windows Azure Storage

## Lab: Microsoft Azure Storage

#### Scenario

Blue Yonder Airlines is planning to add a new feature to its Travel Companion app. This feature will allow users to upload photos from their trips so other people can view them when searching for destinations to visit. In addition, the feature will support two types of uploads, public upload that makes the photo available for everyone to see and private upload that permits only the owner of the photo to view it.

In this lab, you will add the upload service, and the service for viewing uploaded photos. You will also create shared access signatures for the end users&#39; blob container to allow them to view their private content.

#### Objectives

After completing this lab, you will be able to:

- Upload and download files to Azure blobs.
- Insert and query photo metadata with Azure tables.
- Generate a shared access key for blob resources.

#### Lab Setup

Estimated Time: **60 minutes**

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

9. Verify that you received the credentials to sign in to the Azure portal from you training provider; these credentials and the Azure account will be used throughout the labs of this course.

### Exercise 1: Storing Content in Azure Storage

#### Scenario

When a user uploads a photo from a specific trip, the ASP.NET Web API service should upload the photo to a special blob container for that trip. To support this feature, you will first create an Azure storage account, where these blobs will be stored, and then add the required code to the ASP.NET Web API service to connect to the storage account, create the required blob container, and upload the files to the container.

   >**Note:** Windows Store apps can write directly to Azure Storage. However, this will require you to place the storage access keys in the application&#39;s code, which is less secure. To avoid storing keys locally in the application, you will implement these features on the server side.

The main tasks for this exercise are as follows:

1. Create a storage account

2. Add a storage connection string to the cloud project

3. Create blob containers and upload files to them

4. Explore the asynchronous file upload action

#### Task 1: Create a storage account

1. Sign in to the virtual machine **20487B-SEA-DEV-A** as **Administrator** with the password **Pa$$w0rd** and run the **Setup.cmd** script from the **D:\AllFiles\Mod09\LabFiles\Setup** folder. When you are prompted for information, provide it according to the instructions. Write down the name of the cloud service that is shown in the script. You will use it later during the lab.

   >**Note:** You might see warnings in yellow indicating a mismatch in the versions and obsolete settings. These warnings might appear if there are newer versions of Azure PowerShell cmdlets. If these warnings are followed by a red error message, please inform the instructor, otherwise you can ignore them.

2. Open the Microsoft Azure portal ( **http://manage.windowsazure.com** ).
3. Create a new Azure storage account named **blueyonderlab09**** yourinitials** (_yourinitials_ is your initials in lowercase). Select the region closest to your location, and wait until the storage account is created.

   >**Note:** If you get a message saying the storage account creation failed because you reached your storage account limit, delete one of your existing storage accounts and retry the step. If you do not know how to delete a storage account, consult the instructor. 

4. Open the configuration for the newly created account, click **MANAGE ACCESS KEYS** , and copy the **PRIMARY ACCESS KEY**.

#### Task 2: Add a storage connection string to the cloud project

1. Open the **BlueYonder.Companion** solution file from the **D:\AllFiles\Mod09\LabFiles\begin\BlueYonder.Server** folder.

2. Add a storage account connection string to the **BlueYonder.Companion.Host** role in the **BlueYonder.Companion.Host.Azure** project. Use the following settings:

   | **Key** | **Value** |
   | --- | --- |
   | Name | BlueYonderStore |
   | Type | Connection String |
   | Value-&gt;connect using | Manually entered credentials |
   | Value-&gt;Account name | blueyonderlab09_yourinitials_(_yourinitials_ is your initials in lowercase) |
   | Value-&gt;Account keys | Press Ctrl+V to paste the primary access key |

   >**Note:** To create the connection string for the storage account, click the ellipsis in the **Value** box.

#### Task 3: Create blob containers and upload files to them

1. In the **BlueYonder.Companion.Storage** project, open the **AsyncStorageManager** class and add code to the constructor to retrieve the connection string from the role settings.
2. Use the **CloudConfigurationManager.GetSetting** static method to retrieve the storage connection string.

3. Use the **CloudStorageAccount.Parse** static method to create the **CloudStorageAccount** object.
4. Store the result in the **\_account** field.
5. In the **GetContainer** method, add code to get the container reference:
6. Use the **CreateCloudBlobClient** method of the **\_account** member, and then use the **GetContainerReference** method to get the container reference.
7. Create the container if it does not exist by using the **CreateIfNotExists** method of the container, and then return the container.
8. In the **GetBlob** method, add code to get the container and return a block blob reference.
9. Get the container by using the **GetContainer** method you implemented before.
10. Check against the **isPublic** field and if it is true, use the container&#39;s **SetPermissions** method to set the access type to **Blob**.
11. Return a reference to the blob by using the container&#39;s **GetBlockBlobReference**. Use the **fileName** parameter as the blob&#39;s name.
12. Explore the implementation of the **UploadStreamAsync** method. The method uses the previous methods to retrieve a reference to the new blob, and then uploads the stream to it.

#### Task 4: Explore the asynchronous file upload action

1. Open the **FilesController** class from the **BlueYonder.Companion.Controllers** project and locate the **UploadFile** method.

    - The method calls the **UploadStreamAsync** method asynchronously, and after the upload completes, returns the response.

2. Explore the **Public** and **Private** methods of the **FilesController** class.

    - The methods use the **UploadFile** method with a Boolean value that indicates whether the blob container is public or private.

   >**Note:** The client app calls these service actions to upload files as either public or private files. Public files can be viewed by any user, whereas private files can only be viewed by the user who uploaded them.

  
   >**Results** : After you complete the exercise, your code will support storing files in a blob storage, either in a private container or a public container. You will be able to test your changes at the end of the lab.

### Exercise 2: Storing Content in Azure Table Storage

#### Scenario

In the previous exercise, you uploaded photos to a blob container of a specific trip. By creating containers for each trip, you can enable users to easily find the photos from a specific trip. However, the photo upload feature also has to enable people to view photos by location, not just by trip. To support this feature, for every uploaded photo, a record should be stored in Table storage, specifying the location where the photo was taken. In this exercise, you will create the file entity and its containing table, and the code for storing entities in the table and querying the table for photos matching a specific location.

The main tasks for this exercise are as follows:

1. Write the files metadata to the Table storage

2. Query the Table storage

#### Task 1: Write the files metadata to the Table storage

1. In the **BlueYonder.Companion.Storage** project, open the **FileEntity** class and derive the class from the **TableServiceEntity** abstract class.

2. In the **BlueYonder.Companion.Storage** project, open the **AsyncStorageManager** class, and implement the **GetTableContext** method.
3. Use the **CreateCloudTableClient** method of the \_ **account** member to create a **CloudTableClient** object.
4. Use the table client object to retrieve the table by calling the **GetTableReference** method. Use the table name stored in the **MetadataTable** static field of the class.
5. Create the table if it does not exist by using the **CreateIfNotExists** method of the **CloudTable** class.
6. Use the **CloudTableClient.**** GetTableServiceContext** method to return a new table service context.

   >**Note:** You should make sure the table exists before you return a context for it, otherwise the code will fail when running queries on the table. If you have already created the table, you can skip calling the **GetTableReference** and **CreateIfNotExists** methods.

7. Add the **FileEntity** object to the table&#39;s context in the **SaveMetadataAsync** method.
8. Use the **TableServiceContext.AddObject** method and provide it with the target table name and object to add. Use the table name stored in the **MetadataTable** static field of the class.
9. Add the code after the **// TODO: Lab 9 Exercise 2: Task 1.3: use a TableServiceContext to add the object** comment.
10. In the **BlueYonder.Companion.Controllers** project, open the **FilesController** class, locate the **CreateFileEntity** method, and set the **RowKey** and **PartitionKey** properties of the new **FileEntity** object:
11. Set the two properties after the **entity** variable is initialized.
12. Set the entity&#39;s partition key to the **locationID** parameter. Convert the **locationID** from **int** to **string**.
13. Set the entity&#39;s row key to a URI-encoded value of the entity&#39;s **Uri** property. Use the **HttpUtility.UrlEncode** static method to encode the URI.

   >**Note:** The **RowKey** property is set to the file&#39;s URL, because it has a unique value. The URL is encoded because the forward slash (/) character is not valid in row keys. The **PartitionKey** property is set to the **locationID** property, because the partition key groups all the files from a single location in the same partition. By using the location&#39;s ID as the partition key, you can query the table and get all the files uploaded to a specific location. ** **

14. Explore the code in the **Metadata** method. The method creates the **FileEntity** object and saves it to the table.

   >**Note:** The client app calls this service action after it uploads the new file to Blob storage. Storing the list of files in Table storage enables the client app to use queries to find specific images, either by trip or location.

#### Task 2: Query the Table storage

1. In the **BlueYonder.Companion.Storage** project, open the **AsyncStorageManager** class, and implement the **GetLocationMetadata** method.
2. Get the table service context by using the **GetTableContext** method you implemented in the previous task.

3. Use the context&#39;s **CreateQuery&lt;T&gt;** generic method to create a data source for a LINQ query. Use the table name stored in the **MetadataTable** static field of the class.
4. Create a LINQ query that searches for files with a partition key identical to the **locationId** parameter.

   >**Note:** The location ID was used as the entity&#39;s partition key.

5. In the **GetFilesMetadata** method, uncomment the **where** clause.

   >**Note:** The method queries the table for each row key and returns the matching **FileEntity** object by using the **yield return** statement

6. In the **BlueYonder.Companion.Controllers** project, open the **FilesController** class, and review the implementation of the **LocationMetadata** method.

   >**Note:** The method calls the **GetLocationMetadata** method from the **AsyncStorageManager** class, and converts the **FileEntity** objects that are marked as public to **FileDto** objects. The client app calls this service action to get a list of all public files related to a specific location.

7. In the **ToFileDto** method, uncomment the initialization of the **LocationId** property.
8. In the **FilesController** class, explore the code in the **TripMetadata** method.

   >**Note:** The method retrieves the list of files in the trip&#39;s public blob container, and then uses the **GetFilesMetadata** method of the **AsyncStorageManager** class to get the **FileEntity** object for each of the files. The client app calls this service action to get a list of all the files related to a specific trip. Currently, the code retrieves only public files. In the next exercise, you will add the code to retrieve both public and private files.

  
   >**Results** : After you complete the exercise, your code will support storing information about uploaded photos in Table storage. You will be able to test your changes at the end of the lab.

### Exercise 3: Creating Shared Access Signatures for Blobs

#### Scenario

In the first exercise, you stored all the uploaded photos in a publicly accessible blob container. To support privately stored photos, which can only be viewed by the user who uploaded them, you will implement a second upload action that uploads photos to a private container. This container can be accessed only by using a shared access signature.

After completing the code, you will deploy the newly created ASP.NET Web API service to Azure, so it can be tested.

The main tasks for this exercise are as follows:

1. Change the public photos query to return private photos

2. Upload public and private files to Azure Storage

3. View the content of the blob containers and the table

#### Task 1: Change the public photos query to return private photos

1. In the **BlueYonder.Companion.Storage** project, open the **AsyncStorageManager** class, and start implementing the **CreateSharedAccessSignature** method by creating a new **SharecAccessBlobPolicy** object.

    - Set the policy&#39;s permission to **Read** and the expiration time to one hour from the current time. To calculate future time, use the **DateTime.UtcNow.AddHours** method.

2. In the **CreateSharedAccessSignature** method, add code tocreate a new **BlobContainerPermissions** object, add the policy to it, and apply the permissions to the blob container.
3. Add the policy to the **SharedAccessPolicies** collection of the **BlobContainerPermissions** object. Name the policy **blueyonder**.
4. Get the container by calling the **GetContainer** method, and apply the policy to the container by calling the container&#39;s **SetPermission** method.
5. Use the container&#39;s **GetSharedAccessSignature** to return a shared access signature string for the new policy. Pass the policy you created earlier to the method as a parameter.

   >**Note:** The shared access key signature is a URL query string that you append to blob URLs. Without the query string, you cannot access private blobs.

6. In the **BlueYonder.Companion.Controllers** project, open the **FilesController** class, and update the **TripMetadata** method to retrieve a list of private trip files in addition to the public trip files.
7. To get private files, duplicate the call to the **GetFileUris** and set the Boolean parameter to **false**. Store the result in a variable named **privateUris**.
8. Use the **Union** extension method to combine the private and public collections to a single collection. Store the collection in a variable named **allUris**.
9. Change the code so that the **allKeys** collection will use the **allUris** collection instead of just the **publicUris** collection.
10. Locate the **ToFileDto** method and explore its code. If the requested file is private, you create a shared access key for the blob&#39;s container, and then set the **Uri** property of the file to a URL containing the shared access key.
11. Use Visual Studio 2012 to publish the **BlueYonder.Companion.Host.Azure** project. If you did not import your Azure subscription information yet, download your Azure credentials, and import the downloaded publish settings file from the **Publish Windows Azure Application** dialog box.
12. Select the cloud service that matches the cloud service name you wrote down at the beginning of the lab, while running the setup script.
13. Finish the deployment process by clicking **Publish**.

#### Task 2: Upload public and private files to Azure Storage

1. Sign in to the virtual machine **20487B-SEA-DEV-C** as **Admin** with the password **Pa$$w0rd**.

2. Open the **BlueYonder.Companion.Client.sln** solution from **D:\AllFiles\Mod09\LabFiles\begin\BlueYonder.Companion.Client\** in a new Visual Studio 2012 instance.
3. In the **Addresses** class of the **BlueYonder.Companion.Shared** project, set the **BaseUri** property to the Azure cloud service name you wrote down at the beginning of this lab.
4. Run the client app, search for **New** , and purchase a flight from **Seattle** to **New York** _._
5. Select the current trip from **Seattle** to **New York** , and then select **Media** on the app bar.
6. On the **Media** page, use the app bar to add the **StatueOfLiberty.jpg** file from the **D:\Allfiles\Mod09\LabFiles\Assets** Use the app bar to upload the file to the public storage.
7. On the **Media** page, use the app bar to add the **EmpireStateBuilding.jpg** file from the **D:\Allfiles\Mod09\LabFiles\Assets** Use the app bar to upload the file to the private storage.
8. Return to the **Current Trip** page and then go to the **Media** page again. Wait for a few seconds until the photos are downloaded from Azure storage, and verify that both the private and public photos appear.
9. Return to the **Blue Yonder Companion** page (the main page). Under **New York at a Glance** , verify that the photo of the Statue of Liberty, which you uploaded to the public container, appears.

#### Task 3: View the content of the blob containers and the table

1. Return to the Visual Studio 2012 instance on the **20487B-SEA-DEV-A** virtual machine, and then connect to the Azure Storage account you created in the first exercise by using Server Explorer.

   - Add a new Azure storage account to Server Explorer.

2. In Server Explorer, expand the **Blobs** node and inspect the two folders that were created, one for private photos and one for public photos.
3. Open the public blob container, copy the photo&#39;s URL and paste it in a browser&#39;s address bar. Verify that the photo appears.
4. Open the private blob container, copy the photo&#39;s URL and go to the copied address.

    - Private photos cannot be accessed by a direct URL; therefore, an HTTP 404 (The webpage cannot be found) page appears.

   >**Note:** The client app is able to show the private photo because it uses a URL that contains a shared access permission key.

5. In Server Explorer, open the contents of the **FilesMetadata** table. The table contains metadata for both public and private photos.

   >**Results** : After you complete the exercise, you will be able to use the client app to upload photos to the private and public blob containers. You will also be able to view the content of Blob storage and Table storage by using Visual Studio 2012.